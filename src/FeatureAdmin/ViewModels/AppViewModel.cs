using Caliburn.Micro;
using System.ComponentModel.Composition;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FeatureAdmin.Core.Contracts.Repositories;

namespace FeatureAdmin.ViewModels
{
    [Export(typeof(AppViewModel))]
    public class AppViewModel : PropertyChangedBase, IHaveDisplayName
    {
        private string _displayName = "Feature Admin 3 for SharePoint 2013";

        private IEventAggregator _eventAggregator;
        private ISharePointRepositoryRead _readRepository;
        private ISharePointRepositoryCommand _commandRepository;

        [ImportingConstructor]
        public AppViewModel(IEventAggregator eventAggregator,
         ISharePointRepositoryRead readRepository,
         ISharePointRepositoryCommand commandRepository)
        {
            _eventAggregator = eventAggregator;
            _eventAggregator.Subscribe(this);

            _readRepository = readRepository;
            _commandRepository = commandRepository;
        }

        public string DisplayName
        {
            get { return _displayName; }
            set { _displayName = value; }
        }



        public void OpenSettings()
        {
            IsSettingsFlyoutOpen = true;
        }

        private bool _isSettingsFlyoutOpen;

        public bool IsSettingsFlyoutOpen
        {
            get { return _isSettingsFlyoutOpen; }
            set
            {
                _isSettingsFlyoutOpen = value;
                NotifyOfPropertyChange(() => IsSettingsFlyoutOpen);
            }
        }
    }
}
