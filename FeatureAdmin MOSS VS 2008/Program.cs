using System;
using System.Collections.Generic;
using System.Windows.Forms;

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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FrmMain());
        }
    }
}