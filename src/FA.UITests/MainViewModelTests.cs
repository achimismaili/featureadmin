using FA.UI;
using FA.UI.Events;
using FA.UI.Features;
using FA.UI.Locations;
using Moq;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FA.UITests
{
    public class MainViewModelTests
    {
        private MainViewModel _viewModel;

        private Mock<IFeatureViewModel> _featureViewModelMock;
        private Mock<ILocationViewModel> _locationViewModelMock;
        private Mock<IFeaturesListViewModel> _featureListViewModelMock;
        private Mock<ILocationsListViewModel> _locationsListViewModelMock;

        private Mock<IEventAggregator> _eventAggregatorMock;
        private LocationsLoadedEvent _locationLoadedEvent;
        private FeatureDeactivatedEvent _featureDeactivatedEvent;
        private FeatureActivatedEvent _featureActivatedEvent;
        private FeatureUninstalledEvent _featureUninstalledEvent;

        public MainViewModelTests()
        {
            _featureViewModelMock = new Mock<IFeatureViewModel>();
            _locationViewModelMock = new Mock<ILocationViewModel>();
            _featureListViewModelMock = new Mock<IFeaturesListViewModel>();
            _locationsListViewModelMock = new Mock<ILocationsListViewModel>();


            _eventAggregatorMock = new Mock<IEventAggregator>();

            _locationLoadedEvent = new LocationsLoadedEvent();
            _eventAggregatorMock.Setup(ea => ea.GetEvent<LocationsLoadedEvent>())
                .Returns(_locationLoadedEvent);

            _featureDeactivatedEvent = new FeatureDeactivatedEvent();
            _eventAggregatorMock.Setup(ea => ea.GetEvent<FeatureDeactivatedEvent>())
                .Returns(_featureDeactivatedEvent);

            _featureActivatedEvent = new FeatureActivatedEvent();
            _eventAggregatorMock.Setup(ea => ea.GetEvent<FeatureActivatedEvent>())
                .Returns(_featureActivatedEvent);

            _featureUninstalledEvent = new FeatureUninstalledEvent();
            _eventAggregatorMock.Setup(ea => ea.GetEvent<FeatureUninstalledEvent>())
                .Returns(_featureUninstalledEvent);

            _viewModel = new MainViewModel(
                _featureListViewModelMock.Object,
                _locationsListViewModelMock.Object,
                _eventAggregatorMock.Object);
        }

        [Fact]
        public void ShouldCallTheLoadMethodOfTheFeatureListViewModel()
        {
            _viewModel.Load();

            _featureListViewModelMock.Verify(vm => vm.Load(), Times.Once);
        }

    }
}
