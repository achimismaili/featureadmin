using FeatureAdmin.Core.Models.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace FeatureAdmin.Core.Models.Tasks
{
    public class AdminTaskItems : AdminTask
    {
        private double quotaPreparation = 5 / 100;

        private double quotaScopeFarm = 5 / 100;

        private double quotaScopeFarmFeatures = 5 / 100;

        private double quotaScopeWebApps = 10 / 100;

        private double quotaScopeSites
        {
            get
            {
                return
                    1 - quotaPreparation
                    - quotaScopeFarm
                    - quotaScopeFarmFeatures
                    - quotaScopeWebApps;
            }
        }


        public AdminTaskItems(string title, int prepStepsTotal) : base(title)
        {
            PreparationStepsTotal = prepStepsTotal;
            FarmsTotal = 1;
            FeaturesTotal = 1;
        }

        /// <summary>
        /// increments preparation steps processed
        /// </summary>
        /// <param name="preparationSteps">number of processed preparation steps</param>
        /// <returns>true, if all preparation steps have been processed</returns>
        public bool TrackPreparationsProcessed(int preparationSteps)
        {
            return TrackItemsProcessed(preparationSteps, ref PreparationStepsProcessed, ref PreparationStepsTotal, quotaPreparation);

        }

        /// <summary>
        /// tracks / increments items processed
        /// </summary>
        /// <param name="itemsProcessedNew">processed items to increment</param>
        /// <param name="itemsProcessedReference">variable for tracking processed items</param>
        /// <param name="itemsTotalReference">variable for total items</param>
        /// <param name="quota">quota for this type of items regarding total progress</param>
        /// <returns>true, if all items have been processed (--> if processed reached total)</returns>
        private bool TrackItemsProcessed(int itemsProcessedNew, ref int itemsProcessedReference,
            ref int itemsTotalReference, double quota)
        {
            itemsProcessedReference += itemsProcessedNew;

            if (itemsProcessedReference > itemsTotalReference)
            {
                SetProgress(quota);
            }
            else
            {
                SetProgress(quota * itemsProcessedReference / itemsTotalReference);
            }


            return itemsProcessedReference >= itemsTotalReference;
        }

        public bool TrackFeatureDefinitionsProcessed(int featuresProcessed)
        {
            return TrackItemsProcessed(featuresProcessed, ref FeaturesTotal, ref FeaturesProcessed, quotaScopeFarmFeatures);
        }


        public bool TrackLocationsProcessed([NotNull] IEnumerable<Location> locations)
        {
            bool finished = false;
            foreach (Location l in locations)
            {
                var finishedThis = TrackLocationProcessed(l);

                finished = finishedThis ? true : finished;
            }

            return finished;
        }
        public bool TrackLocationProcessed([NotNull] Location location)
        {
            switch (location.Scope)
            {
                case Scope.Web:
                    return TrackItemsProcessed(1, ref WebsTotal, ref WebsProcessed, quotaScopeSites);
                case Scope.Site:
                    UpdateExpectedItems(0, 0, location.ChildCount, 0, 0, 0);
                    return TrackItemsProcessed(1, ref SitesTotal, ref SitesProcessed, quotaScopeSites);
                case Scope.WebApplication:
                    UpdateExpectedItems(0, 0, 0, location.ChildCount, 0, 0);
                    return TrackItemsProcessed(1, ref WebAppsTotal, ref WebAppsProcessed, quotaScopeWebApps);
                case Scope.Farm:
                    UpdateExpectedItems(0, 0, 0, 0, location.ChildCount, 0);
                    return TrackItemsProcessed(1, ref FarmsTotal, ref FarmsProcessed, quotaScopeFarm);
                case Scope.ScopeInvalid:
                default:
                    // do not track non valid scopes
                    return false;
            }
        }

        public int PreparationStepsTotal;
        public int PreparationStepsProcessed;

        public int FarmsProcessed;
        public int FeaturesProcessed;

        public int WebAppsTotal;
        public int WebsTotal;
        public int SitesTotal;

        public int FarmsTotal;
        public int FeaturesTotal;

        public int ItemsTotal
        {
            get
            {
                return FeaturesTotal + FarmsTotal + WebAppsTotal + SitesTotal + WebsTotal;
            }
        }
        public int ItemsProcessed
        {
            get
            {
                return
                    FeaturesProcessed +
                    FarmsProcessed +
                    WebAppsProcessed +
                    SitesProcessed +
                    WebsProcessed;
            }
        }

        public int SitesProcessed;
        public int WebAppsProcessed;
        public int WebsProcessed;


        private void UpdateExpectedItems(int prepSteps, int features, int webs, int sites, int webApps, int farms)
        {
            PreparationStepsTotal += prepSteps;
            FeaturesTotal += features;
            WebsTotal += webs;
            SitesTotal += sites;
            WebAppsTotal += webApps;
            FarmsTotal += farms;
        }


        private void UpdateProcessedItems(int features, int webs, int sites, int webApps, int farms)
        {
            FeaturesProcessed += features;
            WebsProcessed += webs;
            SitesProcessed += sites;
            WebAppsProcessed += webApps;
            FarmsProcessed += farms;
        }
    }
}
