using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FeatureAdmin
{
    /// <summary>
    /// This window is only needed in a special case:
    /// When s.o. wants to remove a Feature Definition, but the Feature Definition has an error
    /// e.g. the manifest file is no longer available.
    /// If the feature scope is still clear, it is clear, which feature collections in the farm should be scanned,
    /// to try to deactivate the feature first, before uninstalling. In case even the scope is no longer clear,
    /// This window is needed, to decide, if the user wants a specific scope to be checked (e.g. he knows, what scope
    /// the feature was before), or if all feature collections in the farm with all scopes should be checked.
    /// The tool then still knows, when it found the right scope and no longer checks feature collections with 
    /// other scopes.
    /// </summary>
    public partial class RemoveInvalidFeature : Form
    {
        private Guid featureID;


        private FrmMain parentWindow;
        private SPFeatureScope scopeWindowScope;

        public SPFeatureScope ScopeWindowScope
        {
            get { return scopeWindowScope; }
            set { scopeWindowScope = value; }
        }

        /// <summary>Initialize Scope select window</summary>
        /// <param name="ParentWindow">keep the parent window object e.g. for logging there</param>
        /// <param name="FeatureID">keep the feature ID to remove the feature</param>
        public RemoveInvalidFeature(FrmMain ParentWindow, Guid FeatureID)
        {
            featureID = FeatureID;
            parentWindow = ParentWindow;
            InitializeComponent();

        }

        /// <summary>cancel and close the window</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScopeCancel_Click(object sender, EventArgs e)
        {
            string msgString = "Action canceled.";
            parentWindow.logTxt(DateTime.Now.ToString(FrmMain.DATETIMEFORMAT) + " - " + msgString + Environment.NewLine);
            this.Close();
            this.Dispose();
        }

        /// <summary>search for the feature and deactivate it in the specified scoped feature collections only</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScopeSelected_Click(object sender, EventArgs e)
        {
            string msgString = string.Empty;
            int featurefound = 0;

            this.Hide();
            try
            {
                if (radioScopeWeb.Checked)
                {
                    scopeWindowScope = SPFeatureScope.Web;
                }
                else
                {
                    if (radioScopeSite.Checked)
                    {
                        scopeWindowScope = SPFeatureScope.Site;
                    }
                    else
                    {
                        if (radioScopeWebApp.Checked)
                        {
                            scopeWindowScope = SPFeatureScope.WebApplication;
                        }
                        else
                        {
                            if (radioScopeFarm.Checked)
                            {
                                scopeWindowScope = SPFeatureScope.Farm;
                            }
                            else
                                msgString = "Error in scope selection! Task canceled";
                            MessageBox.Show(msgString);
                            ((FrmMain)parentWindow).logTxt(DateTime.Now.ToString(FrmMain.DATETIMEFORMAT) + " - " + msgString + Environment.NewLine);

                            return;
                        }
                    }
                }
                featurefound = parentWindow.RemoveFeaturesWithinFarm(featureID, scopeWindowScope);
                if (featurefound == 0)
                {
                    msgString = "Feature not found in Scope:'" + scopeWindowScope.ToString() + "'. Do you want to try something else?";
                    if (MessageBox.Show(msgString, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                    {
                        this.Show();
                    }
                    else
                    {

                    }
                }
                else
                {
                    msgString = "Success! Feature was found and deactivated " + featurefound + " times in Scope:'" + scopeWindowScope.ToString() + "'!";
                    MessageBox.Show(msgString);
                    parentWindow.logTxt(DateTime.Now.ToString(FrmMain.DATETIMEFORMAT) + " - " + msgString + Environment.NewLine);

                    SPFarm.Local.FeatureDefinitions.Remove(featureID, true);
                    parentWindow.logTxt(DateTime.Now.ToString(FrmMain.DATETIMEFORMAT) + " - FeatureDefinition was uninstalled." + Environment.NewLine);

                }
            }
            catch
            {
            }
            finally
            {
                        this.Close();
                        this.Dispose();
            }

        }

        /// <summary>search for the feature and deactivate it in the all feature collections of the farm (Scope Web, Site, WebApplication and Farm)</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnScopeUnknown_Click(object sender, EventArgs e)
        {
            string msgString = "All scopes will be checked for this feature. This might take a while. Continue?";
            if (MessageBox.Show(msgString, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                this.Hide();
                try
                {
                    // search all webs
                    if (!tryToRemove(featureID, SPFeatureScope.Web))
                    {
                        // search all Sites     
                        if (!tryToRemove(featureID, SPFeatureScope.Site))
                        {


                            // search all Web Applications     
                            if (!tryToRemove(featureID, SPFeatureScope.WebApplication))
                            {
                                // search in Farm Scope     
                                tryToRemove(featureID, SPFeatureScope.Farm);
                            }
                        }
                    }

                    SPFarm.Local.FeatureDefinitions.Remove(featureID, true);
                    parentWindow.logTxt(DateTime.Now.ToString(FrmMain.DATETIMEFORMAT) + " - FeatureDefinition was uninstalled." + Environment.NewLine);
                    this.Close();
                    this.Dispose();
                }

                catch
                {
                    parentWindow.logTxt(DateTime.Now.ToString(FrmMain.DATETIMEFORMAT) + " - An error occured iterating through the features ..." + Environment.NewLine);

                }
                finally
                {
                    this.Close();
                    this.Dispose();
                }
            }
        }

        /// <summary>
        /// try to remove the feature from the farm in all feature collections with this scope
        /// this only needs to run, when a feature definition should be uninstalled, and it has an error,
        /// so that it is not clear, which scope the feature definition was defined for.
        /// </summary>
        /// <param name="featureID">feature guid of the feature to be removed</param>
        /// <param name="scope">scope of all the feature collections in the farm to be scanned</param>
        /// <returns>was the feature found, if yes, it had this scope and other scopes don't need to be scanned</returns>
        private bool tryToRemove(Guid featureID, SPFeatureScope scope)
        {
            string msgString = string.Empty;
            int featurefound;

            featurefound = parentWindow.RemoveFeaturesWithinFarm(featureID, scope);
            /// search all webs
            if (featurefound == 0)
            {
                msgString = "Search through all Scopes:'" + scope.ToString() + "' done..., feature not found.";
                parentWindow.logTxt(DateTime.Now.ToString(FrmMain.DATETIMEFORMAT) + " - " + msgString + Environment.NewLine);
                return false;
            }
            else
            {
                msgString = "Success! Feature was found and deactivated " + featurefound + " times in Scope:'" + scope.ToString() + "'!";
                MessageBox.Show(msgString);
                parentWindow.logTxt(DateTime.Now.ToString(FrmMain.DATETIMEFORMAT) + " - " + msgString + Environment.NewLine);
                return true;
            }


        }

    }
}
