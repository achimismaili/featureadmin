using System;
using System.Windows.Forms;
using Microsoft.SharePoint;

namespace FeatureAdmin
{
    public static class GridColMgr
    {
        public static void AddTextColumn(DataGridView grid, string name)
        {
            AddTextColumn(grid, name, name);
        }
        public static void AddTextColumn(DataGridView grid, string name, string title)
        {
            AddTextColumn(grid, name, title, -1);
        }
        public static void AddTextColumn(DataGridView grid, string name, int width)
        {
            AddTextColumn(grid, name, name, width);
        }
        public static void AddTextColumn(DataGridView grid, string name, string title, int width)
        {
            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn()
            {
                Name = name,
                DataPropertyName = name,
                HeaderText = title
            };
            if (width >= 0)
            {
                column.Width = width;
            }
            grid.Columns.Add(column);
        }
    }
}
