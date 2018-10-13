using FeatureAdmin.Core.Models;
using Microsoft.SharePoint;
using System;

namespace FeatureAdmin.Backends.Sp2010.Common
{
    public static class SpFeatureAction
    {

        public static string WebFeatureAction(FeatureDefinition feature, Location location, Func<SPFeatureCollection, Guid, bool, SPFeature> featureAction, bool elevatedPrivileges, bool force, out ActivatedFeature resultingFeature)
        {
            resultingFeature = null;

            SPWeb spWeb = null;

            try
            {
                spWeb = SpLocationHelper.GetWeb(location, elevatedPrivileges);
                SPFeatureCollection featureCollection = SpFeatureHelper.GetFeatureCollection(spWeb, elevatedPrivileges);

                SPFeature spResultingFeature;

                spResultingFeature = featureAction(featureCollection, feature.Id, force);

                if (spResultingFeature != null)
                {
                    resultingFeature = spResultingFeature.ToActivatedFeature(location);
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
            finally
            {
                if (spWeb != null)
                {
                    spWeb.Dispose();
                }
            }

            return string.Empty;
        }

        public static string SiteFeatureAction(
            FeatureDefinition feature, 
            Location location, 
            Func<SPFeatureCollection, Guid, bool, SPFeature> featureAction, 
            bool elevatedPrivileges, bool force, 
            out ActivatedFeature resultingFeature)
        {
            resultingFeature = null;

            SPSite spSite = null;

            try
            {
                spSite = SpLocationHelper.GetSite(location);

                SPFeatureCollection featureCollection = SpFeatureHelper.GetFeatureCollection(spSite, elevatedPrivileges);

                var spResultingFeature = featureAction(featureCollection, feature.Id, force);

                if (spResultingFeature != null)
                {
                    resultingFeature = spResultingFeature.ToActivatedFeature(location);
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
            finally
            {
                if (spSite != null)
                {
                    spSite.Dispose();
                }
            }

            return string.Empty;
        }

        public static string WebAppFeatureAction(
            FeatureDefinition feature,
            Location location,
            Func<SPFeatureCollection, Guid, bool, SPFeature> featureAction,
            bool force,
            out ActivatedFeature resultingFeature)
        {
            resultingFeature = null;

            try
            {
                var wa = SpLocationHelper.GetWebApplication(location.Id);

                var spResultingFeature = featureAction(wa.Features, feature.Id, force);

                if (spResultingFeature != null)
                {
                    resultingFeature = spResultingFeature.ToActivatedFeature(location);
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

            return string.Empty;
        }

        public static string FarmFeatureAction(
            FeatureDefinition feature,
            Location location,
            Func<SPFeatureCollection, Guid, bool, SPFeature> featureAction,
            bool force,
            out ActivatedFeature resultingFeature){
            resultingFeature = null;

            try
            {
                var farm = SpLocationHelper.GetFarm();

                var spResultingFeature = featureAction(farm.Features, feature.Id, force);

                if (spResultingFeature != null)
                {
                    resultingFeature = spResultingFeature.ToActivatedFeature(location);
                }
            }
            catch (Exception ex)
            {

                return ex.Message;
            }

            return string.Empty;
        }

    }
}
