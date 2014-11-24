using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
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
    }
}
