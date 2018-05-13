using Akka.Actor;
using FeatureAdmin.Core.Messages.Tasks;
using FeatureAdmin.Core.Models;
using OrigoDB.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FeatureAdmin.Repository
{
    /// <summary>
    /// The feature repository manages all locations, feautre definitions and activated features
    /// in a in memory database and also talks to SharePoint via the SharePoint Backends
    /// </summary>
    public class FeatureRepository : IFeatureRepository
    {
        public FeatureModel store;
        private Akka.Actor.IActorRef taskManagerActorRef;
        public FeatureRepository()
        {
            var config = new EngineConfiguration();
            config.PersistenceMode = PersistenceMode.ManualSnapshots;
            store = Db.For<FeatureModel>(config);
        }

        public void AddFeatureDefinitions(IEnumerable<FeatureDefinition> featureDefinitions)
        {
            store.AddFeatureDefinitions(featureDefinitions);
        }

        /// <summary>
        /// Initial load and also reload of all feature definitions, locations and activated features
        /// </summary>
        public void Clear()
        {
            store.Clear();
        }

      

        public IEnumerable<FeatureDefinition> SearchFeatureDefinitions(string searchString)
        {
            return store.SearchFeatureDefinitions();
        }
    }
}
