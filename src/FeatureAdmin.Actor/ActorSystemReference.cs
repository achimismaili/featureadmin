using Akka.Actor;
using Autofac;
using FeatureAdmin.Actor.Actors;
using FeatureAdmin.Backends.Services;
using FeatureAdmin.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Actor
{
    public static class ActorSystemReference
    {
        public static ActorSystem ActorSystem { get; private set; }

        static ActorSystemReference()
        {
            CreateActorSystem();
        }

        private static void CreateActorSystem()
        {
            ActorSystem = ActorSystem.Create("FeatureAdminActorSystem");


            //// Setup Autofac
            //ContainerBuilder builder = new ContainerBuilder();
            //builder.RegisterType<DemoDataService>().As<IDataService>();
            //builder.RegisterType<LoadActor>();

            //IContainer container = builder.Build();
        }
    }
}
