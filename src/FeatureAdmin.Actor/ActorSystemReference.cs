using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeatureAdmin.Actor
{
    static class ActorSystemReference
    {
        public static ActorSystem ActorSystem { get; private set; }

        static ActorSystemReference()
        {
            CreateActorSystem();
        }

        private static void CreateActorSystem()
        {
            ActorSystem = ActorSystem.Create("FeatureAdminActorSystem");

            //var container = new StandardKernel();
            //container.Bind<IStockPriceServiceGateway>().To<RandomStockPriceServiceGateway>();
            //container.Bind<StockPriceLookupActor>().ToSelf();

            //IDependencyResolver resolver = new NinjectDependencyResolver(container, ActorSystem);
        }
    }
}
