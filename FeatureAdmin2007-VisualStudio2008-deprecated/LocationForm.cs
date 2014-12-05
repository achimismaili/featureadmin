using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Microsoft.SharePoint;
using Be.Timvw.Framework.Collections.Generic;
using Be.Timvw.Framework.ComponentModel;

namespace FeatureAdmin
{
    public partial class LocationForm : Form
    {
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
            public string LocationUrl
            {
                get
                {
                    if (Location == null)
                        return "";
                    else
                        return Location.Url;
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
        public LocationForm(FeatureLocationSet featLocs)
        {
            InitializeComponent();

            this._FeatureLocations = featLocs;

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
            GridColMgr.AddTextColumn(grid, "LocationUrl");
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
            LocationDetailsView.Columns.Add("Property2", 100);
            LocationDetailsView.Columns.Add("Value", 200);
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
            FeatureLocation floc = LocationGrid.CurrentRow.DataBoundItem as FeatureLocation;
            DisplayLocationDetails(floc);
        }
        private void DisplayLocationDetails(FeatureLocation floc)
        {
            LocationDetailsView.Items.Clear();
            if (floc == null) { return; }
            AddLocationProperty("Feature Name", "?");
            AddLocationProperty("Feature Id", "?");
            AddLocationProperty("Scope", floc.Location.ScopeAbbrev);
            AddLocationProperty("Location Name", floc.Location.Name);
            AddLocationProperty("Location URL", floc.Location.Url);
            AddLocationProperty("Location Id", floc.Location.Id.ToString());
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
    }
}
