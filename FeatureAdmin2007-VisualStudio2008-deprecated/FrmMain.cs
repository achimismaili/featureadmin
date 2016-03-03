using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using CursorUtil;
using Be.Timvw.Framework.Collections.Generic;
using Be.Timvw.Framework.ComponentModel;

namespace FeatureAdmin
{
    public partial class FrmMain : Form
    {

        #region members

        // warning, when no feature was selected
        public const string NOFEATURESELECTED = "No feature selected. Please select at least 1 feature.";

        // defines, how the format of the log time
        public static string DATETIMEFORMAT = "yyyy/MM/dd HH:mm:ss";
        // prefix for log entries: Environment.NewLine + DateTime.Now.ToString(DATETIMEFORMAT) + " - "; 

        private FeatureDatabase m_featureDb = new FeatureDatabase();
        private Location m_CurrentWebAppLocation;
        private Location m_CurrentSiteLocation;
        private Location m_CurrentWebLocation;
        private ContextMenuStrip m_featureDefGridContextMenu;
        private Feature m_featureDefGridContextFeature;

        #endregion


        /// <summary>Initialize Main Window</summary>
        public FrmMain()
        {
            InitializeComponent();

            SetTitle();
            // web app list is prefilled
            loadWebAppList();

            removeBtnEnabled(false);
            EnableActionButtonsAsAppropriate();

            ConfigureFeatureDefGrid();

            this.Show();
            ReloadAllFeatureDefinitions();
            EnableActionButtonsAsAppropriate();
        }

        #region FeatureDefinition Methods

        private void SetTitle()
        {
            Version version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            this.Text = string.Format("FeatureAdmin for SharePoint {0} - v{1}",
                spver.SharePointVersion, version);
        }

        private void ConfigureFeatureDefGrid()
        {
            DataGridView grid = gridFeatureDefinitions;
            grid.AutoGenerateColumns = false;
            GridColMgr.AddTextColumn(grid, "ScopeAbbrev", "Scope", 60);
            GridColMgr.AddTextColumn(grid, "Name");
#if (SP2013)
            GridColMgr.AddTextColumn(grid, "CompatibilityLevel", "Compat", 60);
#endif
            GridColMgr.AddTextColumn(grid, "Id", 50);
            GridColMgr.AddTextColumn(grid, "Activations", 60);
            GridColMgr.AddTextColumn(grid, "Faulty", 60);

            // Set most columns sortable
            foreach (DataGridViewColumn column in grid.Columns)
            {
                if (column.DataPropertyName != "Activations")
                {
                    column.SortMode = DataGridViewColumnSortMode.Automatic;
                }
            }

            CreateFeatureDefContextMenu();

            // Color faulty rows
            grid.DataBindingComplete += new DataGridViewBindingCompleteEventHandler(gridFeatureDefinitions_DataBindingComplete);
        }

        void gridFeatureDefinitions_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            MarkFaultyFeatureDefs();
        }

        /// <summary>Used to populate the list of Farm Feature Definitions</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLoadFDefs_Click(object sender, EventArgs e)
        {
            ReloadAllFeatureDefinitions();
            EnableActionButtonsAsAppropriate();
        }

        private void ReloadAllFeatureDefinitions()
        {
            using (WaitCursor wait = new WaitCursor())
            {
                this.gridFeatureDefinitions.DataSource = null;

                ReloadAllActivationData(); // reloads defintions & activation data

                SortableBindingList<Feature> features = m_featureDb.GetAllFeatures();
                //features.Sort();

                //clbFeatureDefinitions.
                this.gridFeatureDefinitions.DataSource = features;
                    //new SortableBindingList<Feature>(features);

                logDateMsg("Feature Definition list updated.");
            }
        }

        private void MarkFaultyFeatureDefs()
        {
            foreach (DataGridViewRow row in gridFeatureDefinitions.Rows)
            {
                Feature feature = row.DataBoundItem as Feature;
                if (feature.IsFaulty)
                {
                    row.DefaultCellStyle.ForeColor = Color.DarkRed;
                }
            }
        }

        /// <summary>
        /// Get list of all feature definitions currently selected in the
        ///  Feature Definition list
        /// </summary>
        private List<Feature> GetSelectedFeatureDefinitions()
        {
            List<Feature> features = new List<Feature>();
            foreach (DataGridViewRow row in this.gridFeatureDefinitions.SelectedRows)
            {
                Feature feature = row.DataBoundItem as Feature;
                features.Add(feature);
            }
            return features;
        }

        /// <summary>Uninstall the selected Feature definition</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUninstFeatureDef_Click(object sender, EventArgs e)
        {
            List<Feature> selectedFeatures = GetSelectedFeatureDefinitions();
            if (selectedFeatures.Count == 1)
            {
                Feature feature = selectedFeatures[0];

                if (MessageBox.Show("This will forcefully uninstall the " + selectedFeatures.Count +
                    " selected feature definition(s) from the Farm. Continue ?",
                    "Warning",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (MessageBox.Show("Before uninstalling a feature, it should be deactivated everywhere in the farm. " +
                        "Should the Feature be removed from everywhere in the farm before it gets uninstalled?",
                        "Please Select",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // iterate through the farm to remove the feature
                        if (feature.Scope == SPFeatureScope.ScopeInvalid)
                        {
                            RemoveInvalidFeature formScopeUnclear = new RemoveInvalidFeature(this, feature.Id);
                            formScopeUnclear.Show();
                            return;

                        }
                        else
                        {
                            removeFeaturesWithinFarm(feature.Id, feature.Scope);
                        }

                    }
                    using (WaitCursor wait = new WaitCursor())
                    {
                        UninstallSelectedFeatureDefinitions(selectedFeatures);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select exactly 1 feature.");
            }
        }

        #endregion

        #region Feature removal (SiteCollection and SPWeb)

        /// <summary>triggers removeSPWebFeaturesFromCurrentWeb</summary>
        private void btnRemoveFromWeb_Click(object sender, EventArgs e)
        {
            if (clbSPSiteFeatures.CheckedItems.Count > 0)
            {
                MessageBox.Show("Please uncheck all SiteCollection scoped features. Action canceled.",
                    "No SiteCollection scoped Features must be checked");
                return;
            }
            removeSPWebFeaturesFromCurrentWeb();
        }

        /// <summary>Removes selected features from the current web only</summary>
        private void removeSPWebFeaturesFromCurrentWeb()
        {
            if (clbSPWebFeatures.CheckedItems.Count == 0)
            {
                MessageBox.Show(NOFEATURESELECTED);
                logDateMsg(NOFEATURESELECTED);
                return;
            }
            if (IsEmpty(m_CurrentWebLocation))
            {
                MessageBox.Show("No web currently selected");
                return;
            }
            if (clbSPSiteFeatures.CheckedItems.Count > 0) { throw new Exception("Mixed mode unsupported"); }

            string msgString = string.Format(
                "This will force deactivate the {0} selected feature(s) from the selected Site(SPWeb): {1}"
                + "\n Continue ?",
                clbSPWebFeatures.CheckedItems.Count,
                LocationManager.SafeDescribeLocation(m_CurrentWebLocation)
                );
            if (MessageBox.Show(msgString, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            using (WaitCursor wait = new WaitCursor())
            {
                using (SPSite site = OpenCurrentSite())
                {
                    using (SPWeb web = OpenCurrentWeb(site))
                    {
                        List<Feature> webFeatures = GetSelectedWebFeatures();
                        ForceRemoveFeaturesFromLocation(m_CurrentWebAppLocation, web.Features, webFeatures);
                    }
                }
            }

            msgString = "Done. Please refresh the feature list, when all features are removed!";
            logDateMsg(msgString);
        }

        /// <summary>Removes selected features from the current site collection only</summary>
        private void removeSPSiteFeaturesFromCurrentSite()
        {
            if (clbSPSiteFeatures.CheckedItems.Count == 0)
            {
                MessageBox.Show(NOFEATURESELECTED);
                logDateMsg(NOFEATURESELECTED);
                return;
            }
            if (clbSPWebFeatures.CheckedItems.Count > 0) { throw new Exception("Mixed mode unsupported"); }
            if (IsEmpty(m_CurrentSiteLocation))
            {
                MessageBox.Show("No site collection currently selected");
                return;
            }

            string msgString;
            msgString = "This will force remove/deactivate the selected Feature(s) from the selected Site Collection only. Continue ?";
            if (MessageBox.Show(msgString, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            using (WaitCursor wait = new WaitCursor())
            {
                using (SPSite site = OpenCurrentSite())
                {
                    List<Feature> scFeatures = GetSelectedSiteCollectionFeatures();
                    Location scLocation = LocationManager.CreateLocation(site);
                    ForceRemoveFeaturesFromLocation(null, site.Features, scFeatures);
                }
            }

            msgString = "Done. Please refresh the feature list, when all features are removed!";
            logDateMsg(msgString);
        }

        private SPSite OpenCurrentSite()
        {
            if (IsEmpty(m_CurrentSiteLocation)) { return null; }
            SPWebApplication webapp = GetCurrentWebApplication();
            if (webapp == null) { return null; }
            try
            {
                return webapp.Sites[m_CurrentSiteLocation.Url];
            }
            catch (Exception exc)
            {
                logException(exc, "Exception accessing current site collection");
                return null;
            }
        }

        private SPWeb OpenCurrentWeb(SPSite site)
        {
            if (IsEmpty(m_CurrentWebLocation)) { return null; }
            return site.OpenWeb(m_CurrentWebLocation.Id);
        }

        /// <summary>Removes selected features from the current SiteCollection only</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveFromSiteCollection_Click(object sender, EventArgs e)
        {
            if ((clbSPSiteFeatures.CheckedItems.Count == 0) && (clbSPWebFeatures.CheckedItems.Count == 0))
            {
                MessageBox.Show(NOFEATURESELECTED);
                logDateMsg(NOFEATURESELECTED);
            }

            string msgString = string.Empty;

            if (clbSPWebFeatures.CheckedItems.Count == 0)
            {
                // Only site collection features
                // normal removal of SiteColl Features from one site collection
                removeSPSiteFeaturesFromCurrentSite();
                return;
            }

            int featuresRemoved = 0;
            if (clbSPSiteFeatures.CheckedItems.Count != 0)
            {
                string msg = "Cannot remove site features and web features simultaneously";
                MessageBox.Show(msg);
                return;
            }

            // only remove SPWeb features from a site collection
            msgString = "This will force remove/deactivate the selected Site (SPWeb) scoped Feature(s) from all sites within the selected SiteCollections. Continue ?";
            if (MessageBox.Show(msgString, "Warning - Multi Site Deletion!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            using (WaitCursor wait = new WaitCursor())
            {
                using (SPSite site = OpenCurrentSite())
                {
                    if (site == null) { return; }
                    // the web features need a special treatment
                    foreach (Feature checkedFeature in clbSPWebFeatures.CheckedItems)
                    {
                        featuresRemoved += removeWebFeaturesWithinSiteCollection(site, checkedFeature.Id);
                    }
                }
            }
            removeReady(featuresRemoved);
        }
        /// <summary>Removes selected features from the current Web Application only</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRemoveFromWebApp_Click(object sender, EventArgs e)
        {
            int featuresSelected = clbSPSiteFeatures.CheckedItems.Count + clbSPWebFeatures.CheckedItems.Count;
            if (featuresSelected == 0)
            {
                MessageBox.Show(NOFEATURESELECTED);
                logDateMsg(NOFEATURESELECTED);
                return;
            }

            int featuresRemoved = 0;

            string title = m_CurrentWebAppLocation.Name;
            string msgString = string.Empty;
            msgString = "The " + featuresSelected + " selected Feature(s) " +
                "will be removed/deactivated from the complete web application: " + title + ". Continue?";
            if (MessageBox.Show(msgString, "Warning - Multi Site Deletion!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }

            using (WaitCursor wait = new WaitCursor())
            {
                SPWebApplication webApp = GetCurrentWebApplication();
                List<Feature> scFeatures = GetSelectedSiteCollectionFeatures();
                List<Feature> webFeatures = GetSelectedWebFeatures();
                TraverseForceRemoveFeaturesFromWebApplication(webApp, scFeatures, webFeatures);
            }
            removeReady(featuresRemoved);
        }

        // TODO: All this traverse remove code should be factored into its own class
        private void TraverseForceRemoveFeaturesFromFarm(List<Feature> scFeatures, List<Feature> webFeatures)
        {
            foreach (SPWebApplication webapp in WebAppEnumerator.GetAllWebApps())
            {
                TraverseForceRemoveFeaturesFromWebApplication(webapp, scFeatures, webFeatures);
            }
        }

        // TODO: All this traverse remove code should be factored into its own class
        private void TraverseForceRemoveFeaturesFromWebApplication(SPWebApplication webapp, List<Feature> scFeatures, List<Feature> webFeatures)
        {
            foreach (SPSite site in webapp.Sites)
            {
                try
                {
                    TraverseForceRemoveFeaturesFromSiteCollection(site, scFeatures, webFeatures);
                }
                finally
                {
                    if (site != null)
                    {
                        site.Dispose();
                    }
                }
            }
        }

        private void TraverseForceRemoveFeaturesFromSiteCollection(SPSite site, List<Feature> scFeatures, List<Feature> webFeatures)
        {
            if (scFeatures != null && scFeatures.Count > 0)
            {
                Location scLoc = LocationManager.CreateLocation(site);
                ForceRemoveFeaturesFromLocation(scLoc, site.Features, scFeatures);
            }
            if (webFeatures != null && webFeatures.Count > 0)
            {
                foreach (SPWeb web in site.AllWebs)
                {
                    try
                    {
                        ForceRemoveFeaturesFromWeb(web, webFeatures);
                    }
                    finally
                    {
                        if (web != null)
                        {
                            web.Dispose();
                        }
                    }
                }
            }
        }

        private void ForceRemoveFeaturesFromWeb(SPWeb web, List<Feature> webFeatures)
        {
            if (webFeatures != null && webFeatures.Count > 0)
            {
                Location webLoc = LocationManager.CreateLocation(web);
                ForceRemoveFeaturesFromLocation(webLoc, web.Features, webFeatures);
            }
        }

        private List<Feature> GetSelectedSiteCollectionFeatures()
        {
            List<Feature> list = new List<Feature>();
            foreach (Feature feature in clbSPSiteFeatures.CheckedItems)
            {
                list.Add(feature);
            }
            return list;
        }
        private List<Feature> GetSelectedWebFeatures()
        {
            List<Feature> list = new List<Feature>();
            foreach (Feature feature in clbSPWebFeatures.CheckedItems)
            {
                list.Add(feature);
            }
            return list;
        }
        /// <summary>Removes selected features from the whole Farm</summary>
        private void btnRemoveFromFarm_Click(object sender, EventArgs e)
        {
            int featuresSelected = clbSPSiteFeatures.CheckedItems.Count + clbSPWebFeatures.CheckedItems.Count;
            if (featuresSelected == 0)
            {
                MessageBox.Show(NOFEATURESELECTED);
                logDateMsg(NOFEATURESELECTED);
                return;
            }

            int featuresRemoved = 0;

            string msgString = string.Empty;
            msgString = "The " + featuresSelected + " selected Feature(s) " +
                "will be removed/deactivated in the complete Farm! Continue?";
            if (MessageBox.Show(msgString, "Warning - Multi Site Deletion!", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
            {
                return;
            }
            using (WaitCursor wait = new WaitCursor())
            {
                List<Feature> scFeatures = GetSelectedSiteCollectionFeatures();
                List<Feature> webFeatures = GetSelectedWebFeatures();
                TraverseForceRemoveFeaturesFromFarm(scFeatures, webFeatures);
            }
            removeReady(featuresRemoved);
        }

        #endregion

        #region Feature Activation + Deactivation

        private void activateSelectedFeaturesAcrossSpecifiedScope(SPFeatureScope activationScope, Location currentLocation, FeatureActivator.Action action)
        {
            List<Feature> selectedFeatures = GetSelectedFeatureDefinitions();
            FeatureSet featureSet = new FeatureSet(selectedFeatures);
            string verbnow = "?", verbpast = "?";
            switch (action)
            {
                case FeatureActivator.Action.Activating:
                    verbnow = "activate"; verbpast = "activated";
                    break;
                case FeatureActivator.Action.Deactivating:
                    verbnow = "deactivate"; verbpast = "deactivated";
                    break;
                default:
                    throw new Exception("Invalid action in activateSelectedFeaturesAcrossSpecifiedScope");
            }

            if (selectedFeatures.Count > 10)
            {
                InfoBox(string.Format(
                    "Too many features ({0}) selected; max 10 may be {1} at time",
                    selectedFeatures.Count,
                    verbpast
                    ));
                return;
            }

            if (featureSet.LowestScope < activationScope)
            {
                InfoBox(string.Format(
                    "{0} Feature(s) cannot be {1} at level {2}",
                    featureSet.LowestScope,
                    verbpast,
                    activationScope
                    ));
                return;
            }

            string currentLocName = "?";
            if (activationScope == SPFeatureScope.Farm)
            {
                currentLocName = "Local Farm";
            }
            else
            {
                currentLocName = currentLocation.Name + " -- " + currentLocation.Url;
            }
            string msg;
            msg = string.Format(
                "{0}  the selected {1} features: \n"
                + "{2}"
                + "in the selected {3}: \n\t{4}",
                CultureInfo.CurrentCulture.TextInfo.ToTitleCase(verbnow),
                selectedFeatures.Count,
                GetFeatureSummaries(selectedFeatures, "\t{Name} ({Scope})\n"),
                activationScope,
                currentLocName
                );
            if (!ConfirmBox(msg))
            {
                return;
            }
            FeatureActivator activator = new FeatureActivator(m_featureDb, action, featureSet);
            activator.ExceptionLoggingListeners += new FeatureActivator.ExceptionLoggerHandler(activator_ExceptionLoggingListeners);

            switch (activationScope)
            {
                case SPFeatureScope.Farm:
                    {
                        activator.TraverseActivateFeaturesInFarm();
                    }
                    break;

                case SPFeatureScope.WebApplication:
                    {
                        SPWebApplication webapp = GetCurrentWebApplication();
                        activator.TraverseActivateFeaturesInWebApplication(webapp);
                    }
                    break;

                case SPFeatureScope.Site:
                    {
                        using (SPSite site = OpenCurrentSite())
                        {
                            if (site == null) { return; }
                            try
                            {
                                activator.TraverseActivateFeaturesInSiteCollection(site);
                            }
                            finally
                            {
                                site.Dispose();
                            }
                        }
                    }
                    break;

                case SPFeatureScope.Web:
                    {
                        using (SPSite site = OpenCurrentSite())
                        {
                            if (site == null) { return; }
                            try
                            {
                                using (SPWeb web = site.OpenWeb())
                                {
                                    if (web == null) { return; }
                                    try
                                    {
                                        activator.ActivateFeaturesInWeb(web);
                                    }
                                    finally
                                    {
                                        site.Dispose();
                                    }
                                }
                            }
                            finally
                            {
                                site.Dispose();
                            }
                        }
                    }
                    break;
                default:
                    {
                        msg = "Unknown scope: " + activationScope.ToString();
                        ErrorBox(msg);
                    }
                    break;
            }
            msg = string.Format(
                "{0} Feature(s) {1}.",
                activator.Activations,
                verbpast);
            MessageBox.Show(msg);
            logDateMsg(msg);
        }

        private static string GetFeatureSummaries(List<Feature> features, string format)
        {
            StringBuilder featureNames = new StringBuilder();
            foreach (Feature feature in features)
            {
                string text = format
                    .Replace("{Name}", feature.Name)
                    .Replace("{Scope}", feature.Scope.ToString())
                    .Replace("{Id}", feature.Id.ToString())
                    ;
                featureNames.Append(text);
            }
            return featureNames.ToString();
        }

        void activator_ExceptionLoggingListeners(Exception exc, string msg)
        {
            logException(exc, msg);
        }

        #endregion

        #region Helper Methods

        private void logFeatureSelected()
        {
            if (clbSPSiteFeatures.CheckedItems.Count + clbSPWebFeatures.CheckedItems.Count > 0)
            {

                // enable all remove buttons
                removeBtnEnabled(true);

                logDateMsg("Feature selection changed:");
                if (clbSPSiteFeatures.CheckedItems.Count > 0)
                {
                    foreach (Feature checkedFeature in clbSPSiteFeatures.CheckedItems)
                    {
                        logMsg(checkedFeature.ToString() + ", Scope: Site");
                    }
                }

                if (clbSPWebFeatures.CheckedItems.Count > 0)
                {
                    foreach (Feature checkedFeature in clbSPWebFeatures.CheckedItems)
                    {
                        logMsg(checkedFeature.ToString() + ", Scope: Web");
                    }
                }
            }
            else
            {
                // disable all remove buttons
                removeBtnEnabled(false);
            }
        }

        /// <summary>
        /// Forcefully delete specified features from specified collection
        /// (Could be SPFarm.Features, or SPWebApplication.Features, or etc.)
        /// </summary>
        private int ForceRemoveFeaturesFromLocation(Location location, SPFeatureCollection spfeatureSet, List<Feature> featuresToRemove)
        {
            int removedFeatures = 0;
            foreach (Feature feature in featuresToRemove)
            {
                ForceRemoveFeatureFromLocation(location, spfeatureSet, feature.Id);
                removedFeatures++;
            }
            return removedFeatures;
        }

        /// <summary>forcefully removes a feature from a featurecollection</summary>
        /// <param name="id">Feature ID</param>
        public void ForceRemoveFeatureFromLocation(Location location, SPFeatureCollection spfeatureSet, Guid featureId)
        {
            try
            {
                spfeatureSet.Remove(featureId, true);
            }
            catch (Exception exc)
            {
                logException(exc, string.Format(
                    "Trying to remove feature {0} from {1}",
                    featureId, LocationManager.SafeDescribeLocation(location)));
            }
        }

        /// <summary>remove all Web scoped features from a SiteCollection</summary>
        /// <param name="site"></param>
        /// <param name="featureID"></param>
        /// <returns>number of deleted features</returns>
        private int removeWebFeaturesWithinSiteCollection(SPSite site, Guid featureID)
        {
            int removedFeatures = 0;
            int scannedThrough = 0;
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                foreach (SPWeb web in site.AllWebs)
                {
                    try
                    {
                        //forcefully remove the feature
                        if (web.Features[featureID].DefinitionId != null)
                        {
                            bool force = true;
                            web.Features.Remove(featureID, force);
                            removedFeatures++;
                            logDateMsg(
                                string.Format("Success removing feature {0} from {1}",
                                featureID,
                                LocationManager.SafeDescribeObject(web)));

                        }
                    }
                    catch (Exception exc)
                    {
                        logException(exc,
                            string.Format("Exception removing feature {0} from {1}",
                            featureID,
                            LocationManager.SafeDescribeObject(web)));
                    }
                    finally
                    {
                        scannedThrough++;
                        if (web != null)
                        {
                            web.Dispose();
                        }
                    }
                }
                string msgString = removedFeatures + " Web Scoped Features removed in the SiteCollection " + site.Url.ToString() + ". " + scannedThrough + " sites/subsites were scanned.";
                logDateMsg("  SiteColl - " + msgString);
            });
            return removedFeatures;
        }

        /// <summary>remove all features within a web application, if feature is web scoped, different method is called</summary>
        /// <param name="webApp"></param>
        /// <param name="featureID"></param>
        /// <param name="trueForSPWeb"></param>
        /// <returns>number of deleted features</returns>
        private int removeFeaturesWithinWebApp(SPWebApplication webApp, Guid featureID, SPFeatureScope featureScope)
        {
            int removedFeatures = 0;
            int scannedThrough = 0;
            string msgString;

            msgString = "Removing Feature '" + featureID.ToString() + "' from Web Application: '" + webApp.Name.ToString() + "'.";
            logDateMsg(" WebApp - " + msgString);

            SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    if (featureScope == SPFeatureScope.WebApplication)
                    {
                        try
                        {
                            webApp.Features.Remove(featureID, true);
                            removedFeatures++;
                            logDateMsg(
                                string.Format("Success removing feature {0} from {1}",
                                featureID,
                                LocationManager.SafeDescribeObject(webApp)));
                        }
                        catch (Exception exc)
                        {
                            logException(exc,
                                string.Format("Exception removing feature {0} from {1}",
                                featureID,
                                LocationManager.SafeDescribeObject(webApp)));
                        }
                    }
                    else
                    {

                        foreach (SPSite site in webApp.Sites)
                        {
                            using (site)
                            {
                                if (featureScope == SPFeatureScope.Web)
                                {
                                    removedFeatures += removeWebFeaturesWithinSiteCollection(site, featureID);
                                }
                                else
                                {
                                    //forcefully remove the feature
                                    site.Features.Remove(featureID, true);
                                    removedFeatures += 1;

                                }
                                scannedThrough++;
                            }
                        }
                    }

                });
            msgString = removedFeatures + " Features removed in the Web Application. " + scannedThrough + " SiteCollections were scanned.";
            logDateMsg(" WebApp - " + msgString);

            return removedFeatures;
        }

        /// <summary>removes the defined feature within a complete farm</summary>
        /// <param name="featureID"></param>
        /// <param name="trueForSPWeb"></param>
        /// <returns>number of deleted features</returns>
        public int removeFeaturesWithinFarm(Guid featureID, SPFeatureScope featureScope)
        {
            int removedFeatures = 0;
            int scannedThrough = 0;
            string msgString;

            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                msgString = "Removing Feature '" + featureID.ToString() + ", Scope: " + featureScope.ToString() + "' from the Farm.";
                logDateMsg("Farm - " + msgString);
                if (featureScope == SPFeatureScope.Farm)
                {
                    try
                    {
                        SPWebService.ContentService.Features.Remove(featureID, true);
                        removedFeatures++;
                        logDateMsg(
                            string.Format("Success removing feature {0} from {1}",
                            featureID,
                            LocationManager.SafeDescribeObject(SPFarm.Local)));
                    }
                    catch (Exception exc)
                    {
                        logException(exc,
                            string.Format("Exception removing feature {0} from farm",
                            featureID,
                            LocationManager.SafeDescribeObject(SPFarm.Local)));

                        logDateMsg("Farm - The Farm Scoped feature '" + featureID.ToString() + "' was not found. ");
                    }
                }
                else
                {

                    // all the content WebApplications 
                    SPWebApplicationCollection webApplicationCollection = SPWebService.ContentService.WebApplications;

                    //  administrative WebApplications 
                    // SPWebApplicationCollection webApplicationCollection = SPWebService.AdministrationService.WebApplications;

                    foreach (SPWebApplication webApplication in webApplicationCollection)
                    {

                        removedFeatures += removeFeaturesWithinWebApp(webApplication, featureID, featureScope);
                        scannedThrough++;
                    }
                }
                msgString = removedFeatures + " Features removed in the Farm. " + scannedThrough + " Web Applications were scanned.";
                logDateMsg("Farm - " + msgString);
            });
            return removedFeatures;
        }


        /// <summary>Uninstall a collection of Farm Feature Definitions forcefully</summary>
        /// <param name="manager"></param>
        /// <param name="checkedListItems"></param>
        private void UninstallSelectedFeatureDefinitions(List<Feature> features)
        {
            foreach (Feature feature in features)
            {
                try
                {
                    FeatureUninstaller.ForceUninstallFeatureDefinition(
                        feature.Id, feature.CompatibilityLevel);
                }
                catch (Exception exc)
                {
                    logException(exc, string.Format(
                        "Exception uninstalling feature defintion {0}",
                        feature.Id));
                }
            }
        }

        private void ForceUninstallFeatureDefinition(Feature feature)
        {
        }

        /// <summary>enables or disables all buttons for feature removal</summary>
        /// <param name="enabled">true = enabled, false = disabled</param>
        private void removeBtnEnabled(bool enabled)
        {
            btnRemoveFromWeb.Enabled = enabled;
            btnRemoveFromSiteCollection.Enabled = enabled;
            btnRemoveFromWebApp.Enabled = enabled;
            btnRemoveFromFarm.Enabled = enabled;
        }

        /// <summary>enables or disables all buttons for feature definition administration</summary>
        /// <param name="enabled">true = enabled, false = disabled</param>
        private void EnableActionButtonsAsAppropriate()
        {
            bool bDb = m_featureDb.IsLoaded();
            SPFeatureScope lowestScope = GetLowestSelectedScope();

            bool bWeb = !IsEmpty(m_CurrentWebLocation) && lowestScope >= SPFeatureScope.Web;
            bool bSite = !IsEmpty(m_CurrentSiteLocation) && lowestScope >= SPFeatureScope.Site;
            bool bWebApp = !IsEmpty(m_CurrentWebAppLocation) && lowestScope >= SPFeatureScope.WebApplication;

            btnUninstFDef.Enabled = bDb && (gridFeatureDefinitions.SelectedRows.Count <= 10);

            btnActivateSPWeb.Enabled = bDb && bWeb;
            btnDeactivateSPWeb.Enabled = bDb && bWeb;
            btnActivateSPSite.Enabled = bDb && bSite;
            btnDeactivateSPSite.Enabled = bDb && bSite;
            btnActivateSPWebApp.Enabled = bDb && bWebApp;
            btnDeactivateSPWebApp.Enabled = bDb && bWebApp;
            btnActivateSPFarm.Enabled = bDb;
        }

        private SPFeatureScope GetLowestSelectedScope()
        {
            SPFeatureScope minScope = SPFeatureScope.ScopeInvalid;
            foreach (DataGridViewRow row in gridFeatureDefinitions.SelectedRows)
            {
                Feature feature = row.DataBoundItem as Feature;
                if (minScope == SPFeatureScope.ScopeInvalid || feature.Scope < minScope)
                {
                    minScope = feature.Scope;
                }
            }
            return minScope;
        }

        private void removeReady(int featuresRemoved)
        {
            string msgString;
            msgString = featuresRemoved.ToString() + " Features were removed. Please 'Reload Web Applications'!";
            MessageBox.Show(msgString);
            logDateMsg(msgString);
        }

        /// <summary>searches for faulty features and provides the option to remove them</summary>
        /// <param name="features">SPFeatureCollection, the container for the features</param>
        /// <param name="scope">is needed, in case a feature is found, so that it can be deleted</param>
        /// <returns></returns>
        private bool findFaultyFeatureInCollection(SPFeatureCollection features, SPFeatureScope scope)
        {
            return false;
            /*
            bool faultyFound = false;
            if (features == null)
            {
                logDateMsg("ERROR: Feature Collection was null!");
                return false;
            }
            if (features.Count == 0)
            {
                logDateMsg("ERROR: Feature Collection was empty!");
                return false;
            }

            // string DBName = string.Empty; // tbd: retrieve the database name of the featureCollection
            string featuresName = features.ToString();

            try
            {
                foreach (SPFeature feature in features)
                {
                    FeatureChecker checker = new FeatureChecker();
                    FeatureChecker.Status status = checker.CheckFeature(feature);
                    if (status.Faulty)
                    {
                        faultyFound = true;
                        string location = LocationInfo.SafeDescribeObject(feature.Parent);

                        string msgString = "Faulty Feature found! Id=" + feature.DefinitionId.ToString();
#if SP2013
                        msgString += " Activation=" + feature.TimeActivated.ToString("yyyy-MM-dd");
#endif
                        string solutionInfo = GetFeatureSolutionInfo(feature);
                        if (!string.IsNullOrEmpty(solutionInfo))
                        {
                            msgString += solutionInfo;
                        }
                        msgString += Environment.NewLine
                            + "Found in " + location + "." + Environment.NewLine
                            + " Should it be removed from the farm?";
                        logDateMsg(msgString);
                        string caption = "Found Faulty Feature";
                        DialogResult response = MessageBox.Show(msgString, caption, 
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                        if (response == DialogResult.Yes)
                        {
                            removeFeaturesWithinFarm(feature.DefinitionId, scope);
                        }
                        if (response == DialogResult.Cancel)
                        {
                            return faultyFound;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is SqlException)
                {
                    string msgstring = string.Format("Cannot access a feature collection of scope '{0}'! Not enough access rights for a content DB on SQL Server! dbOwner rights are recommended. Please read the following error message:\n\n'{1}'", scope.ToString(), ex.ToString());
                    string MessageCaption = string.Format("FeatureCollection in a Content DB not accessible");
                    if(MessageBox.Show(msgstring, MessageCaption,MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    {
                        return faultyFound;
                    }
                }
                else
                {
                    MessageBox.Show(ex.ToString(), "An error has occured!", MessageBoxButtons.OK);
                }
                return faultyFound;
            }
            return faultyFound;
             * */
        }
        private string GetFeatureSolutionInfo(SPFeature feature)
        {
            string text = "";
            try
            {
                if (feature.Definition != null
                    && feature.Definition.SolutionId != Guid.Empty)
                {
                    text = string.Format("SolutionId={0}", feature.Definition.SolutionId);
                    SPSolution solution = SPFarm.Local.Solutions[feature.Definition.SolutionId];
                    if (solution != null)
                    {
                        try {
                            text += string.Format(", SolutionName='{0}'", solution.Name);
                        } catch { }
                        try {
                            text += string.Format(", SolutionDisplayName='{0}'", solution.DisplayName);
                        } catch { }
                        try {
                            text += string.Format(", SolutionDeploymentState='{0}'", solution.DeploymentState);
                        } catch { }
                    }
                }
            }
            catch
            {
            }
            return text;
        }

        #endregion
        #region Error & Logging Methods

        protected void ReportError(string msg)
        {
            ReportError(msg, "Error");
        }
        protected void ReportError(string msg, string caption)
        {
            // TODO - be nice to have an option to suppress message boxes
            MessageBox.Show(msg, caption);
        }

        protected string FormatSiteException(SPSite site, Exception exc, string msg)
        {
            msg += " on site " + site.ServerRelativeUrl + " (ContentDB: " + site.ContentDatabase.Name + ")";
            if (IsSimpleAccessDenied(exc))
            {
                msg += " (web application user policy with Full Control and dbOwner rights on contentdb recommended for this account)";
            }
            return msg;
        }

        protected void logException(Exception exc, string msg)
        {
            logDateMsg(msg + " -- " + DescribeException(exc));
        }

        protected bool IsSimpleAccessDenied(Exception exc)
        {
            return (exc is System.UnauthorizedAccessException && exc.InnerException == null);
        }

        protected string DescribeException(Exception exc)
        {
            if (IsSimpleAccessDenied(exc))
            {
                return "Access is Denied";
            }
            StringBuilder txt = new StringBuilder();
            while (exc != null)
            {
                if (txt.Length > 0) txt.Append(" =++= ");
                txt.Append(exc.Message);
                exc = exc.InnerException;
            }
            return txt.ToString();
        }

        /// <summary>
        /// Log current date+time, plus message, plus line return
        /// </summary>
        protected void logDateMsg(string msg)
        {
            logMsg(DateTime.Now.ToString(DATETIMEFORMAT) + " - " + msg);
        }
        /// <summary>
        /// Log message plus line return
        /// </summary>
        protected void logMsg(string msg)
        {
            logTxt(msg + Environment.NewLine);
        }

        /// <summary>adds log string to the logfile</summary>
        public void logTxt(string logtext)
        {
            this.txtResult.AppendText(logtext);
        }
        protected void ClearLog()
        {
            this.txtResult.Clear();
        }

        #endregion

        #region Feature lists, WebApp, SiteCollection and SPWeb list set up

        /// <summary>trigger load of web application list</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnListWebApplications_Click(object sender, EventArgs e)
        {
            loadWebAppList();
        }

        /// <summary>populate the web application list</summary>
        private void loadWebAppList()
        {
            using (WaitCursor wait = new WaitCursor())
            {
                listWebApplications.Items.Clear();
                listSiteCollections.Items.Clear();
                listSites.Items.Clear();
                clbSPSiteFeatures.Items.Clear();
                clbSPWebFeatures.Items.Clear();
                removeBtnEnabled(false);

                if (SPWebService.ContentService != null)
                {
                    foreach (SPWebApplication webApp in SPWebService.ContentService.WebApplications)
                    {
                        Location location = LocationManager.CreateLocation(webApp);
                        listWebApplications.Items.Add(location);
                    }
                }
                else
                {
                    MessageBox.Show("No Content web application found. Are you Farm-Administrator? Is the database accessible?");
                }

                if (listWebApplications.Items.Count > 0)
                {
                    listSiteCollections.Enabled = true;
                    // If there is only one, select it
                    if (listWebApplications.Items.Count == 0)
                    {
                        listWebApplications.SelectedIndex = 0;
                    }
                }
                else
                {
                    listSiteCollections.Enabled = false;
                }
            }
        }

        private SPWebApplication GetCurrentWebApplication()
        {
            Guid id = m_CurrentWebAppLocation.Id;
            if (id == Guid.Empty)
            {
                return null;
            }
            else if (id == SPAdministrationWebApplication.Local.Id)
            {
                return SPAdministrationWebApplication.Local;
            }
            else
            {
                return SPWebService.ContentService.WebApplications[id];
            }
        }

        /// <summary>Update SiteCollections list when a user changes the selection in Web Application list
        /// </summary>
        private void listWebApplications_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadCurrentSiteCollections();
            EnableActionButtonsAsAppropriate();
        }

        private void ReloadCurrentSiteCollections()
        {
            using (WaitCursor wait = new WaitCursor())
            {
                try
                {
                    ClearCurrentSiteCollectionData();
                    ClearCurrentWebData();
                    removeBtnEnabled(false);

                    m_CurrentWebAppLocation = listWebApplications.SelectedItem as Location;
                    if (m_CurrentWebAppLocation == null) { return; }
                    SPWebApplication webApp = GetCurrentWebApplication();
                    foreach (SPSite site in webApp.Sites)
                    {
                        Location siteLocation = LocationManager.CreateLocation(site);
                        listSiteCollections.Items.Add(siteLocation);
                    }
                    // select first site collection if there is only one
                    if (listSiteCollections.Items.Count == 1)
                    {
                        listSiteCollections.SelectedIndex = 0;
                    }
                }
                catch (Exception exc)
                {
                    logException(exc, "Exception enumerating site collections");
                }
            }
        }

        /// <summary>UI method to update the SPWeb list when a user changes the selection in site collection list
        /// </summary>
        private void listSiteCollections_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (WaitCursor wait = new WaitCursor())
            {
                m_CurrentSiteLocation = listSiteCollections.SelectedItem as Location;
                ClearCurrentWebData();
                ReloadCurrentSiteCollectionFeatures();
                ReloadSubWebList();
            }
            EnableActionButtonsAsAppropriate();
        }

        private void ClearCurrentSiteCollectionData()
        {
            FeatureAdmin.Location.Clear(m_CurrentSiteLocation);
            listSiteCollections.Items.Clear();
            clbSPSiteFeatures.Items.Clear();
        }

        private void ClearCurrentWebData()
        {
            FeatureAdmin.Location.Clear(m_CurrentWebLocation);
            listSites.Items.Clear();
            clbSPWebFeatures.Items.Clear();
        }

        private void ReloadCurrentSiteCollectionFeatures()
        {
            clbSPSiteFeatures.Items.Clear();
            if (IsEmpty(m_CurrentSiteLocation)) { return; }
            List<Feature> features = m_featureDb.GetFeaturesOfLocation(m_CurrentSiteLocation);
            features.Sort();
            clbSPSiteFeatures.Items.AddRange(features.ToArray());
        }

        private void ReloadCurrentWebFeatures()
        {
            clbSPWebFeatures.Items.Clear();
            if (m_CurrentWebLocation == null) { return; }
            List<Feature> features = m_featureDb.GetFeaturesOfLocation(m_CurrentWebLocation);
            features.Sort();
            clbSPWebFeatures.Items.AddRange(features.ToArray());
        }

        private static bool IsEmpty(Location location)
        {
            return FeatureAdmin.Location.IsLocationEmpty(location);
        }

        private void ReloadSubWebList()
        {
            try
            {
                removeBtnEnabled(false);

                m_CurrentSiteLocation = listSiteCollections.SelectedItem as Location;
                if (m_CurrentSiteLocation == null) { return; }
                using (SPSite site = OpenCurrentSite())
                {
                    foreach (SPWeb web in site.AllWebs)
                    {
                        try
                        {
                            Location webLocation = LocationManager.CreateLocation(web);
                            listSites.Items.Add(webLocation);
                        }
                        finally
                        {
                            site.Dispose();
                        }
                    }
                }
                // select first site collection if there is only one
                if (listSites.Items.Count == 1)
                {
                    listSites.SelectedIndex = 0;
                }
            }
            catch (Exception exc)
            {
                logException(exc, "Exception enumerating subwebs");
            }
        }

        /// <summary>UI method to load the SiteCollection Features and Site Features
        /// Handles the SelectedIndexChanged event of the listSites control.
        /// </summary>
        private void listSites_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (WaitCursor wait = new WaitCursor())
            {
                m_CurrentWebLocation = listSites.SelectedItem as Location;
                ReloadCurrentWebFeatures();
            }
            EnableActionButtonsAsAppropriate();
        }

        private void clbSPSiteFeatures_SelectedIndexChanged(object sender, EventArgs e)
        {
            logFeatureSelected();
        }

        private void clbSPWebFeatures_SelectedIndexChanged(object sender, EventArgs e)
        {
            logFeatureSelected();
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            ClearLog();
        }

        private void gridFeatureDefinitions_SelectionChanged(object sender, EventArgs e)
        {
            EnableActionButtonsAsAppropriate();
        }

        private void btnActivateSPWeb_Click(object sender, EventArgs e)
        {
            if (IsEmpty(m_CurrentWebLocation))
            {
                InfoBox("No site (SPWeb) selected");
                return;
            }
            activateSelectedFeaturesAcrossSpecifiedScope(
                SPFeatureScope.Web,
                m_CurrentWebLocation,
                FeatureActivator.Action.Activating);
        }

        private void btnDeactivateSPWeb_Click(object sender, EventArgs e)
        {
            if (IsEmpty(m_CurrentWebLocation))
            {
                InfoBox("No site (SPWeb) selected");
                return;
            }
            activateSelectedFeaturesAcrossSpecifiedScope(
                SPFeatureScope.Web,
                m_CurrentWebLocation,
                FeatureActivator.Action.Deactivating);
        }

        private void btnActivateSPSite_Click(object sender, EventArgs e)
        {
            if (IsEmpty(m_CurrentSiteLocation))
            {
                InfoBox("No site collection selected");
                return;
            }
            activateSelectedFeaturesAcrossSpecifiedScope(
                SPFeatureScope.Site,
                m_CurrentSiteLocation,
                FeatureActivator.Action.Activating);
        }

        private void btnDeactivateSPSite_Click(object sender, EventArgs e)
        {
            if (IsEmpty(m_CurrentSiteLocation))
            {
                InfoBox("No site collection selected");
                return;
            }
            activateSelectedFeaturesAcrossSpecifiedScope(
                SPFeatureScope.Site,
                m_CurrentSiteLocation,
                FeatureActivator.Action.Deactivating);
        }

        private void btnActivateSPWebApp_Click(object sender, EventArgs e)
        {
            if (IsEmpty(m_CurrentWebAppLocation))
            {
                InfoBox("No web application selected");
                return;
            }
            activateSelectedFeaturesAcrossSpecifiedScope(
                SPFeatureScope.WebApplication,
                m_CurrentWebAppLocation,
                FeatureActivator.Action.Activating);
        }

        private void btnDeactivateSPWebApp_Click(object sender, EventArgs e)
        {
            if (IsEmpty(m_CurrentWebAppLocation))
            {
                InfoBox("No web application selected");
                return;
            }
            activateSelectedFeaturesAcrossSpecifiedScope(
                SPFeatureScope.WebApplication,
                m_CurrentWebAppLocation,
                FeatureActivator.Action.Deactivating);
        }

        private void btnActivateSPFarm_Click(object sender, EventArgs e)
        {
            activateSelectedFeaturesAcrossSpecifiedScope(
                SPFeatureScope.Farm,
                null, // Location
                FeatureActivator.Action.Activating);
        }

        private void btnDeactivateSPFarm_Click(object sender, EventArgs e)
        {
            activateSelectedFeaturesAcrossSpecifiedScope(
                SPFeatureScope.Farm,
                null, // Location
                FeatureActivator.Action.Deactivating);
        }

        private List<Location> GetFeatureLocations(Guid featureId)
        {
            if (!m_featureDb.IsLoaded())
            {
                ReloadAllActivationData();
            }
            return m_featureDb.GetLocationsOfFeature(featureId);
        }

        private void ReloadAllActivationData()
        {
            using (WaitCursor wait = new WaitCursor())
            {
                ActivationFinder finder = new ActivationFinder();
                // No Found callback b/c we process final list
                finder.ExceptionListeners += new ActivationFinder.ExceptionHandler(logException);

                // Call routine to actually find & report activations
                m_featureDb.LoadAllData(finder.FindAllActivationsOfAllFeatures());
                m_featureDb.MarkFaulty(finder.GetFaultyFeatureIdList());
                lblFeatureDefinitions.Text = string.Format(
                    "All {0} Features installed in the Farm",
                    m_featureDb.GetAllFeaturesCount());
            }
        }


        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        #endregion
        #region MessageBoxes
        private static void ErrorBox(string text)
        {
            ErrorBox(text, "Error");
        }
        private static void ErrorBox(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        private static bool ConfirmBox(string text)
        {
            return ConfirmBox(text, "Confirm");
        }
        private static bool ConfirmBox(string text, string caption)
        {
            DialogResult rtn = MessageBox.Show(
                text, caption,
                MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            return (rtn == DialogResult.Yes);
        }
        private static void InfoBox(string text)
        {
            InfoBox(text, "");
        }
        private static void InfoBox(string text, string caption)
        {
            MessageBox.Show(text, caption, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        private void gridFeatureDefinitions_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView grid = sender as DataGridView;
            if (grid.Columns[e.ColumnIndex].DataPropertyName == "Activations")
            {
                Feature feature = grid.Rows[e.RowIndex].DataBoundItem as Feature;
                FeatureLocationSet set = new FeatureLocationSet();
                ReviewActivationsOfFeature(feature);
            }
        }

        private void ReviewActivationsOfFeature(Feature feature)
        {
            FeatureLocationSet set = new FeatureLocationSet();
            set[feature] = GetFeatureLocations(feature.Id);
            ReviewActivationsOfFeatures(set);
        }
        private void ReviewActivationsOfFeatures(FeatureLocationSet featLocs)
        {
            if (featLocs.Count == 0 || featLocs.GetTotalLocationCount() == 0)
            {
                MessageBox.Show("No activations found");
            }
            else
            {
                LocationForm form = new LocationForm(featLocs);
                form.ShowDialog();
            }
        }

        private void gridFeatureDefinitions_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            m_featureDefGridContextFeature = null;
            if (e.RowIndex < 0 || e.ColumnIndex < 0) { return; } // Ignore if out of data area
            if (e.Button == MouseButtons.Right)
            {
                // Find feature def on which user right-clicked
                DataGridView grid = sender as DataGridView;
                DataGridViewRow row = grid.Rows[e.RowIndex];
                m_featureDefGridContextFeature = row.DataBoundItem as Feature;
                if (GetIntValue(m_featureDefGridContextFeature.Activations, 0) == 0)
                {
                    // no context menu if feature has no activations
                    // because the context menu gets stuck up if the no activations
                    // message box appears
                    return;
                }
                if (m_featureDefGridContextFeature.Id == Guid.Empty)
                {
                    // no context menu if no valid id
                    return;
                }
                gridFeatureDefinitions.ContextMenuStrip = m_featureDefGridContextMenu;
                UpdateGridContextMenu();
            }
        }

        private void CreateFeatureDefContextMenu()
        {
            // Construct context menu
            ContextMenuStrip ctxtmenu = new ContextMenuStrip();
            ToolStripMenuItem header = new ToolStripMenuItem("Feature: ?");
            header.Name = "Header";
            header.ForeColor = Color.DarkBlue;
            ctxtmenu.Items.Add(header);
            ToolStripMenuItem menuViewActivations = new ToolStripMenuItem("View activations");
            menuViewActivations.Name = "Activations";
            menuViewActivations.MouseDown += gridFeatureDefinitions_ViewActivationsClick;
            ctxtmenu.Items.AddRange(new ToolStripItem[] { menuViewActivations });
            m_featureDefGridContextMenu = ctxtmenu;
        }

        private void UpdateGridContextMenu()
        {
            Feature feature = m_featureDefGridContextFeature;
            ContextMenuStrip ctxtmenu = m_featureDefGridContextMenu;
            ToolStripItem header = ctxtmenu.Items.Find("Header", true)[0];
            header.Text = string.Format("Feature: {0}", GetFeatureNameOrId(feature));
            ToolStripItem activations = ctxtmenu.Items.Find("Activations", true)[0];
            activations.Text = string.Format("View {0} activations", GetIntValue(feature.Activations, 0));
        }

        private static string GetFeatureNameOrId(Feature feature)
        {
            if (!string.IsNullOrEmpty(feature.Name))
            {
                return feature.Name;
            }
            else
            {
                return feature.Id.ToString();
            }
        }

        private void gridFeatureDefinitions_ViewActivationsClick(object sender, EventArgs e)
        {
            ReviewActivationsOfFeature(m_featureDefGridContextFeature);
        }

        private static int GetIntValue(int? value, int defval)
        {
            return value.HasValue ? value.Value : defval;
        }

        private void btnViewActivations_Click(object sender, EventArgs e)
        {
            if (gridFeatureDefinitions.SelectedRows.Count < 1)
            {
                InfoBox("No features selected to review activations");
                return;
            }
            FeatureLocationSet set = new FeatureLocationSet();
            foreach (Feature feature in GetSelectedFeatureDefinitions())
            {
                set.Add(feature, GetFeatureLocations(feature.Id));
            }
            ReviewActivationsOfFeatures(set);
        }

        private void gridFeatureDefinitions_MouseDown(object sender, MouseEventArgs e)
        {
            gridFeatureDefinitions.ContextMenuStrip = null;
            m_featureDefGridContextFeature = null;
        }

    }
}
