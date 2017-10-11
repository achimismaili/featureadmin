using Akka.Actor;
using Akka.DI.AutoFac;
using Autofac;
using FeatureAdmin.Backends.Actors;
using FeatureAdmin.Backends.Services;
using FeatureAdmin.Core.Services;

namespace FeatureAdmin
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
            ActorSystem = ActorSystem.Create("FeatureAdminActorSystem",
                @"akka {
                stdout - loglevel = DEBUG
                    loglevel = DEBUG
                    log - config - on - start = on
                    actor {
                            debug {
                                receive = on
                                autoreceive = on
                                lifecycle = on
                                event-stream = on
                                unhandled = on
                                  }
                          }"  
                );


            //// Setup Autofac
            ContainerBuilder builder = new ContainerBuilder();
    builder.RegisterType<DemoDataService>().As<IDataService>();
            builder.RegisterType<LocationManagerActor>();
            builder.RegisterType<LocationActor>();

            IContainer container = builder.Build();

    var propsResolver = new AutoFacDependencyResolver(container, ActorSystem);

}
    }
}
