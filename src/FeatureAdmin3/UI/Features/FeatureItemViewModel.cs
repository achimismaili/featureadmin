using FeatureAdmin3.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using System.Windows.Input;

namespace FeatureAdmin3.UI.Features
{
    public class FeatureItemViewModel : ViewModelBase
  {
    private string _displayMember;
        private IEventAggregator _eventAggregator;

        public FeatureItemViewModel(Guid id,
          string displayMember,
          IEventAggregator eventAggregator)
        {
            Id = id;
            DisplayMember = displayMember;
            OpenFeatureEditViewCommand = new DelegateCommand(OnFeatureEditViewExecute);
            _eventAggregator = eventAggregator;
        }

        private void OnFeatureEditViewExecute(object obj)
        {
            _eventAggregator.GetEvent<OpenFeatureEditViewEvent>()
              .Publish(Id);
        }

        public Guid Id { get; private set; }
        public ICommand OpenFeatureEditViewCommand { get; private set; }

        public string DisplayMember
        {
            get
            {
                return _displayMember;
            }

            set
            {
                _displayMember = value;
                OnPropertyChanged();
            }
        }
    }
}