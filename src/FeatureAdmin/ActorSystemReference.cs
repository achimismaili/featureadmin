using Akka.Actor;
using Akka.DI.AutoFac;
using Autofac;
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
            // see also https://doc.akka.io/docs/akka/2.5/general/configuration.html#custom-application-conf
            ActorSystem = ActorSystem.Create("FeatureAdminActorSystem",
                @"akka {
                stdout - loglevel = WARNING
                    loglevel = WARNING
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

//#if (SP2013)
//            builder.RegisterType<Backends.Sp2013.Services.SpDataService>().As<IDataService>();
//#else
//            builder.RegisterType<Backends.Demo.Services.DemoDataService>().As<IDataService>();
//#endif
            builder.RegisterType<Actors.LocationActor>();
            builder.RegisterType<Actors.FeatureDefinitionActor>();
            IContainer container = builder.Build();

    var propsResolver = new AutoFacDependencyResolver(container, ActorSystem);

}
    }
}
