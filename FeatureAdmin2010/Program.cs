using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint;
using System.Security.Principal;

// Codeplex Project http://FeatureAdmin.codeplex.com
// Started by Achim Ismaili in September 2009 - email: achim@ismaili.de

// interesting URL about feature upgrading
// http://sharepointtipoftheday.blogspot.com/2009/06/solution-feature-upgrading-and.html

namespace FeatureAdmin
{
    static class Program
    {
        // run feature admin with elevated privileges
        public static bool adminMode = false;

        /// <summary>The main entry point for the application.</summary>
        [STAThread]
        static void Main()
        {
            
           
            if (SPFarm.Local == null )
            {

                //WindowsIdentity identity = WindowsIdentity.GetCurrent();
                //WindowsPrincipal principal = new WindowsPrincipal(identity);
                //bool isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);

            
                    MessageBox.Show("Cannot find farm. Either this account has not enough access to the SharePoint config db (dbReader is not sufficient, dbOwner is recommended)" +
                        " or SharePoint 2010 is not installed on this machine. FeatureAdmin will close now.");
                    return;
                }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }


    }
}