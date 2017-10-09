using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureAdmin.Core.Models;
using System.Collections.ObjectModel;

namespace FeatureAdmin.Actor.Actors
{
    public class ViewModelSyncActor : ReceiveActor
    {
        private ObservableCollection<Location> locations;

        public ViewModelSyncActor(ObservableCollection<Location> locations)
        {
            this.locations = locations;
        }
    }
}
