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
        private List<Location> _Locations = null;
        private Feature _Feature = null;
        public LocationForm(Feature feature, List<Location> locations)
        {
            InitializeComponent();

            this._Locations = locations;
            this._Feature = feature;

            PopulateFeatureHeader();
            ConfigureLocationGrid();
            ConfigureLocationDetails();
            PopulateLocationGrid();
        }
        private void PopulateFeatureHeader()
        {
            FeatureNameLabel.Text = _Feature.Name;
            FeatureIdLabel.Text = _Feature.Id.ToString();
            FeatureScopeLabel.Text = _Feature.Scope.ToString();
        }
        private void ConfigureLocationGrid()
        {
            DataGridView grid = this.LocationGrid;
            grid.AutoGenerateColumns = false;
            AddTextColumn(grid, "Url");
            AddTextColumn(grid, "Name");
            AddTextColumn(grid, "Id");

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
        private void AddTextColumn(DataGridView grid, string name)
        {
            AddTextColumn(grid, name, name);
        }
        private void AddTextColumn(DataGridView grid, string name, string title)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn()
            {
                Name = name,
                DataPropertyName = name,
                HeaderText = title
            };
            grid.Columns.Add(column);
        }
        private void PopulateLocationGrid()
        {
            this.LocationGrid.DataSource = new SortableBindingList<Location>(_Locations);
        }
        private void LocationGrid_SelectionChanged(object sender, EventArgs e)
        {
            Location location = LocationGrid.CurrentRow.DataBoundItem as Location;
            DisplayLocationDetails(location);
        }
        private void DisplayLocationDetails(Location location)
        {
            LocationDetailsView.Items.Clear();
            if (location == null) { return; }
            AddLocationProperty("Name", location.Name);
            AddLocationProperty("URL", location.Url);
            AddLocationProperty("Id", location.Id.ToString());
            switch (location.Scope)
            {
                case SPFeatureScope.Farm:
                    AddFarmDetails(location);
                    break;
                case SPFeatureScope.WebApplication:
                    AddWebApplicationDetails(location);
                    break;
                case SPFeatureScope.Site:
                    AddSiteDetails(location);
                    break;
                case SPFeatureScope.Web:
                    AddWebDetails(location);
                    break;
            }
        }
        private void AddFarmDetails(Location location)
        {
        }
        private void AddWebApplicationDetails(Location location)
        {
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
