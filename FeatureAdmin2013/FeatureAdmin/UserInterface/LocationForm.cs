using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Microsoft.SharePoint;
using FeatureAdmin.Models;
using FeatureAdmin.Common;
using System.Linq;

namespace FeatureAdmin.UserInterface
{
    public partial class LocationForm : Form
    {
        private List<FeatureLocation> _selectedFeatureLocations = new List<FeatureLocation>();
        private FrmMain _parentForm;
        private class FeatureLocation
        {
            public Feature Feature;
            public Location Location;
            public string FeatureName
            {
                get
                {
                    return Feature.Name;
                }
            }
            public string LocationFullUrl
            {
                get
                {
                    if (Location == null)
                        return "";
                    else
                        return Location.FullUrl;
                }
            }
            public string LocationName
            {
                get
                {
                    if (Location == null)
                        return "";
                    else
                        return Location.Name;
                }
            }
        }
        private FeatureLocationSet _FeatureLocations = null;
        public LocationForm(FeatureLocationSet featLocs, FrmMain parentForm)
        {
            InitializeComponent();

            this._FeatureLocations = featLocs;
            this._parentForm = parentForm;

            PopulateFeatureHeader();
            ConfigureLocationGrid();
            ConfigureLocationDetails();
            PopulateLocationGrid();
        }
        private void PopulateFeatureHeader()
        {
            FeaturePanelCaption.Text = string.Format(
                "{0} Activation(s) of {1} Feature(s)",
                _FeatureLocations.GetTotalLocationCount(),
                _FeatureLocations.Count
                );

        }
        private void ConfigureLocationGrid()
        {
            DataGridView grid = this.LocationGrid;
            grid.AutoGenerateColumns = false;
            if (_FeatureLocations.Count > 1)
            {
                GridColMgr.AddTextColumn(grid, "FeatureName");
            }
            GridColMgr.AddTextColumn(grid, "LocationFullUrl", 300);
            GridColMgr.AddTextColumn(grid, "LocationName");

            // Set all columns sortable
            foreach (DataGridViewColumn column in grid.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.Automatic;
            }
        }
        private void ConfigureLocationDetails()
        {
            // Width of -2 indicates auto-size.
            LocationDetailsView.Columns.Add("Property", -2, HorizontalAlignment.Left);
            LocationDetailsView.Columns.Add("Value", 300);
        }
        private void PopulateLocationGrid()
        {
            // Create list of FeatureLocations
            List<FeatureLocation> list = new List<FeatureLocation>();
            foreach (KeyValuePair<Feature, List<Location>> pair in _FeatureLocations)
            {
                Feature feature = pair.Key;
                List<Location> locations = pair.Value;
                foreach (Location location in locations)
                {
                    FeatureLocation floc = new FeatureLocation();
                    floc.Feature = feature;
                    floc.Location = location;
                    list.Add(floc);
                }
            }
            this.LocationGrid.DataSource = new SortableBindingList<FeatureLocation>(list);
        }
        private void LocationGrid_SelectionChanged(object sender, EventArgs e)
        {
           
            if (LocationGrid.SelectedRows.Count > 0)
            {
                _selectedFeatureLocations.Clear();
                foreach (DataGridViewRow row in LocationGrid.SelectedRows)
                {
                    _selectedFeatureLocations.Add(row.DataBoundItem as FeatureLocation);
                }
            }
            if (LocationGrid.CurrentRow != null)
            {
                FeatureLocation floc = LocationGrid.CurrentRow.DataBoundItem as FeatureLocation;
                DisplayLocationDetails(floc);
            }
        }
        private void DisplayLocationDetails(FeatureLocation floc)
        {
            DeactivateButton.Text = string.Format(
                "Deactivate {0} selected activation(s)",
                LocationGrid.SelectedRows.Count);
            LocationDetailsView.Items.Clear();
            if (floc == null) { return; }
            AddLocationProperty("Feature Name", floc.Feature.Name);
            AddLocationProperty("Feature Id", floc.Feature.Id.ToString());
            AddLocationProperty("Scope", floc.Location.ScopeAbbrev);
            AddLocationProperty("Location Name", floc.Location.Name);
            AddLocationProperty("Location URL", floc.Location.FullUrl);
            AddLocationProperty("Location Id", floc.Location.Id.ToString());
            if (!string.IsNullOrEmpty(floc.Location.Template.Name))
            {
                AddLocationProperty("Template Name", floc.Location.Template.Name);
                AddLocationProperty("Template Title", floc.Location.Template.Title);
            }
            switch (floc.Location.Scope)
            {
                case SPFeatureScope.Site:
                    AddSiteDetails(floc.Location);
                    break;
                case SPFeatureScope.Web:
                    AddWebDetails(floc.Location);
                    break;
            }
        }
        private void AddSiteDetails(Location location)
        {
        }
        private void AddWebDetails(Location location)
        {
        }
        private void AddLocationProperty(string name, string value)
        {
            ListViewItem item = new ListViewItem();
            item.Text = name;
            item.SubItems.Add(value);
            LocationDetailsView.Items.Add(item);
        }

        private void DeactivateButton_Click(object sender, EventArgs e)
        {
            if ((_selectedFeatureLocations.Count == 0))
            {
                MessageBox.Show(Constants.Text.NOFEATURESELECTED);
                // logDateMsg(Constants.Text.NOFEATURESELECTED);
                return;
            }


            // make sure, only one type of feature scopes is selected
            var countWeb = 0;
            var countSiCo = 0;
            var countWebApp = 0;
            var countFarm = 0;

            foreach (FeatureLocation fl in _selectedFeatureLocations)
            {
                switch (fl.Feature.Scope)
                {
                    case SPFeatureScope.Web:
                        countWeb++;
                        break;
                    case SPFeatureScope.Site:
                        countSiCo++;
                        break;
                    case SPFeatureScope.WebApplication:
                        countWebApp++;
                        break;
                    case SPFeatureScope.Farm:
                        countFarm++;
                        break;
                    default:
                        // invalid? tbd error logging
                        break;
                }
            }

            // when multiplicating with 0, it results in 0, so when every combination results in 0, only 1 can be not zero

            if (countFarm * countWebApp == 0
                && countFarm * countSiCo == 0
                && countFarm * countWeb == 0
                && countWebApp * countSiCo == 0
                && countWebApp * countWeb == 0
                && countSiCo * countWeb == 0
                && countFarm + countWebApp + countSiCo + countWeb > 0 // sanity check ...
                )
            {
                foreach (Location l in _selectedFeatureLocations.Select(fl => fl.Location).Distinct())
                {
                    _parentForm.PromptAndActivateSelectedFeaturesAcrossSpecifiedScope(
                       _selectedFeatureLocations
                            .Where(fl => fl.Location.FullUrl.Equals(l.FullUrl, StringComparison.InvariantCultureIgnoreCase))
                            .Select(fl => fl.Feature).ToList(),
                       SPFeatureScope.WebApplication,
                       l,
                       FeatureActivator.Action.Deactivating
                       );
                }
            }
            else
            {
                MessageBox.Show("Canceled because of mixed or invalid scopes in selected features");
                // logDateMsg(Constants.Text.NOFEATURESELECTED);
                return;
            }



        }
    }
}
