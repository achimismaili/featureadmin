using Caliburn.Micro;
using FeatureAdmin.Core.Repositories.Contracts;
using FeatureAdmin.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace FeatureAdmin.Tests
{
    public class AppViewModelTests
    {
        // The interfaces/instances you will need to test with - this is your test subject
        AppViewModel _mainVM;

        // You can mock the other interfaces:
        Mock<IWindowManager> _windowManager;
        Mock<IEventAggregator> _eventAggregator;
        Mock<IDataService> _repository;

        public AppViewModelTests()
        {
            // Mock the window manager
            _windowManager = new Mock<IWindowManager>();

            // Mock the event aggregator
            _eventAggregator = new Mock<IEventAggregator>();

            _repository = new Mock<IDataService>();
          
            // Create the main VM injecting the mocked interfaces
            // Mocking interfaces is always good as there is a lot of freedom
            // Use mock.Object to get hold of the object, the mock is just a proxy that decorates the original object
         //   _mainVM = new AppViewModel( _eventAggregator.Object);
        }

        /// <summary>
        /// Test to make sure subscribe was called on the event aggregator at least once
        /// see also https://stackoverflow.com/questions/15591723/caliburn-micro-unit-test
        /// </summary>
        [Fact]
        public void Test_SubscribedToEventAggregator()
        {
            _eventAggregator.Verify(x => x.Subscribe(_mainVM));
        }


    }
}
