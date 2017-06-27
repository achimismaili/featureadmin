using FeatureAdmin.SharePointFarmService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;

namespace FeatureAdmin.SharePointFarmService
{
    public class Farm : BaseSharePointContainer, IFeatureDefinitionService
    {
        /// <summary>
        /// get an activated feature from this container
        /// </summary>
        /// <param name="id">feature id</param>
        /// <returns>returns an activated feature</returns>
        /// <remarks>featurecollection is not filled in constructor, because definitions might be used
        /// more frequently and then not always activated features need to be retrieved</remarks>
        public new SPFeature GetActivatedFeature(Guid id)
        {
            if (featureCollection == null)
            {
                featureCollection = SPWebService.ContentService.Features; 
            }

            return base.GetActivatedFeature(id);
        }

        /// <summary>
        /// get all activated features from this container
        /// </summary>
        /// <param name="id">feature id</param>
        /// <returns>returns an activated feature</returns>
        /// <remarks>featurecollection is not filled in constructor, because definitions might be used
        /// more frequently and then not always activated features need to be retrieved</remarks>

        public new SPFeatureCollection GetAllActivatedFeatures()
        {
            if (featureCollection == null)
            {
                featureCollection = SPWebService.ContentService.Features;
            }

            return base.GetAllActivatedFeatures();
        }

        public SPFeatureDefinitionCollection GetAllFeatureDefinitions()
        {
            return SPFarm.Local.FeatureDefinitions;
        }

        public SPFeatureDefinition GetFeatureDefinition(Guid Id)
        {
            return GetAllFeatureDefinitions().FirstOrDefault(f => f.Id == Id);
        }

    }
}
