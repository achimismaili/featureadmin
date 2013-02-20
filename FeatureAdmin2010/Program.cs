using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint;

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
                    + " (dbReader is not sufficient, dbOwner is recommended)"
                    + " or SharePoint " + spver.SharePointVersion
                    + " is not installed on this machine. "
                    + " FeatureAdmin will close now.";
                MessageBox.Show(msg);
                    return;
                }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}