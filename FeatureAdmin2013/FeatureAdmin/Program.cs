using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint;
using Serilog;

// Codeplex Project http://FeatureAdmin.codeplex.com
// Started by Achim Ismaili in September 2009 - email: achim@ismaili.de

// interesting URL about feature upgrading
// http://sharepointtipoftheday.blogspot.com/2009/06/solution-feature-upgrading-and.html

namespace FeatureAdmin
{
    static class Program
    {
        /// <summary>The main entry point for the application.</summary>
        [STAThread]
        static void Main()
        {

            if (SPFarm.Local == null)
            {
                string msg = "Cannot find local SharePoint Farm. "
                    + "Either this account has not enough access to the SharePoint config db"
                    + " (dbReader is not sufficient, dbOwner is recommended),"
                    + " you may not be in windows local admin group, Farm-Admin group or "
                    + " you may require additional rights like SPShellAdmin, ... \n"
                    + " or SharePoint " + Common.Constants.SharePointVersion
                    + " is not installed on this machine. "
                    + " FeatureAdmin will close now.";
                MessageBox.Show(msg);
                return;
            }
            // We could also check SPFarm.Local.CurrentUserIsAdministrator(true)
            // but it seems that non-farm admins are already trapped by SPFarm.Local == null

            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new UserInterface.FrmMain());
        }
    }
}
