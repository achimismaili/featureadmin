using FeatureAdmin.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.CheckedListBox;

namespace FeatureAdmin.Common
{
    public static class GridConverter
    {
        public static List<FeatureDefinition> SelectedRowsToFeatureDefinition(DataGridViewSelectedRowCollection selectedRows)
        {
            if (selectedRows == null || selectedRows.Count == 1)
            {
                Log.Warning("No feature definitions selected!");
                return null;
            }

            var convertedRows = new List<FeatureDefinition>();

            foreach (DataGridViewRow row in selectedRows)
            {
                FeatureDefinition fd = row.DataBoundItem as FeatureDefinition;
                convertedRows.Add(fd);
            }

            return convertedRows;
        }

        internal static List<FeatureParent> SelectedRowsToFeatureParent(DataGridViewSelectedRowCollection selectedRows)
        {
            if (selectedRows == null || selectedRows.Count == 1)
            {
                Log.Warning("No feature parents selected!");
                return null;
            }

            var convertedRows = new List<FeatureParent>();

            foreach (DataGridViewRow row in selectedRows)
            {
                FeatureParent fd = row.DataBoundItem as FeatureParent;
                convertedRows.Add(fd);
            }

            return convertedRows;
        }

        internal static List<ActivatedFeature> SelectedRowsToActivatedFeature(DataGridViewSelectedRowCollection selectedRows)
        {
            if (selectedRows == null || selectedRows.Count == 1)
            {
                Log.Warning("No activated features selected!");
                return null;
            }

            var convertedRows = new List<ActivatedFeature>();

            foreach (DataGridViewRow row in selectedRows)
            {
                ActivatedFeature fd = row.DataBoundItem as ActivatedFeature;
                convertedRows.Add(fd);
            }

            return convertedRows;
        }

        internal static List<ActivatedFeature> SelectedRowsToActivatedFeature(CheckedItemCollection selectedRows)
        {
            if (selectedRows == null || selectedRows.Count == 1)
            {
                Log.Warning("No activated features selected!");
                return null;
            }

            var convertedRows = new List<ActivatedFeature>();

            foreach (DataGridViewRow row in selectedRows)
            {
                ActivatedFeature fd = row.DataBoundItem as ActivatedFeature;
                convertedRows.Add(fd);
            }

            return convertedRows;
        }
    }
}
