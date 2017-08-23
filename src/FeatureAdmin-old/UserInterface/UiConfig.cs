using FeatureAdmin.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FeatureAdmin.UserInterface
{
    /// <summary>
    /// configuration class for the UserInterface
    /// </summary>
    public static class UiConfig
    {
        public static void SetRowRedAndBold(DataGridViewRowCollection rows)
        {
            foreach (DataGridViewRow row in rows)
            {
                Feature feature = row.DataBoundItem as Feature;
                if (feature.IsFaulty)
                {
                    row.DefaultCellStyle.ForeColor = Color.DarkRed;
                    row.DefaultCellStyle.Font = new Font(SystemFonts.DefaultFont, FontStyle.Bold); ;
                }
            }
        }

        public static  ContextMenuStrip CreateFeatureDefinitionContextMenu(MouseEventHandler mouseEventHandler)
        {
            // Construct context menu
            ContextMenuStrip ctxtMenu = new ContextMenuStrip();
            ToolStripMenuItem header = new ToolStripMenuItem("Feature: ?");
            header.Name = "Header";
            header.ForeColor = Color.DarkBlue;
            ctxtMenu.Items.Add(header);

            ToolStripMenuItem menuViewActivations = new ToolStripMenuItem("View activations");
            menuViewActivations.Name = "Activations";
            menuViewActivations.MouseDown += mouseEventHandler;
            ctxtMenu.Items.AddRange(new ToolStripItem[] { menuViewActivations });

            return ctxtMenu;
        }

    }
}
