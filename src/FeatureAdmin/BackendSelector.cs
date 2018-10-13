using FeatureAdmin.Core.Models.Enums;
using FeatureAdmin.Core.Services;

namespace FeatureAdmin
{
    public class BackendSelector
    {
        public Backend EvaluateBackend()
        {

            //// evaluate SP 2007
            //if (CheckIfSharePointVersionIsInstalled(12))
            //{
            //    return Backend.SP2007;
            //}

            //// evaluate SP 2010
            //if (CheckIfSharePointVersionIsInstalled(14))
            //{
            //    return Backend.SP2010;
            //}

            // evaluate SP 2013
            if (CheckIfSharePointVersionIsInstalled(15))
            {
                return Backend.SP2013;
            }

            // evaluate SP 2016
            if (CheckIfSharePointVersionIsInstalled(16))
            {
                return Backend.SP2013;
                // commented out until implemented
                // return Backend.SP2016;
            }
            
            // evaluate SP 2019
            if (CheckIfSharePointVersionIsInstalled(17))
            {
                return Backend.SP2013;
                // commented out until implemented
                // return Backend.SP2019;
            }

            throw new System.ApplicationException("No supported SharePoint Version found, please check on https://www.featureadmin.com for updates, or download the demo.");
        }

        private bool SubKeyExist(string Subkey)
        {
            // Check if a Subkey exist
            var myKey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(Subkey);
            if (myKey == null)
                return false;
            else
                return true;
        }

        private bool CheckIfSharePointVersionIsInstalled(int version) 
        {
            // string SharePointDllPath = string.Format(
                // @"C:\Program Files\Common Files\Microsoft Shared\Web Server Extensions\{0}\ISAPI\Microsoft.SharePoint.dll",
                // version
                // );

            var SharePointRegistryPath = string.Format(@"SOFTWARE\Microsoft\Office\{0}.0\BinPath", version);

            return SubKeyExist(SharePointRegistryPath);

            // return System.IO.File.Exists(SharePointDllPath);

            // checking if farm exists, takes too long, therefore, commented out

            //if (!System.IO.File.Exists(SharePointDllPath))
            //{

            //    return false;
            //}

            //try
            //{
            //    T dataService = new T();

            //    var farm = dataService.LoadFarm();

            //    return farm != null;

            //}
            //catch (Exception ex)
            //{

            //    System.Diagnostics.Debug.WriteLine("Error when locating farm: {0}", ex.Message);
            //    return false;

            //}
        }
    }
}
