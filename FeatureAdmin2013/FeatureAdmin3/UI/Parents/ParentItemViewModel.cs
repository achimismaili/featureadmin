using FeatureAdmin3.UI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Prism.Events;
using System.Windows.Input;

namespace FeatureAdmin3.UI.Parents
{
    public class ParentItemViewModel : ViewModelBase
    {
            private string _displayMember;
            private IEventAggregator _eventAggregator;

            public ParentItemViewModel(Guid id,
              string displayMember,
              IEventAggregator eventAggregator)
            {
                Id = id;
                DisplayMember = displayMember;
            OpenParentEditViewCommand = new DelegateCommand(OnParentEditViewExecute);
                _eventAggregator = eventAggregator;
            }

            private void OnParentEditViewExecute(object obj)
            {
                _eventAggregator.GetEvent<OpenParentEditViewEvent>()
                  .Publish(Id);
            }

            public Guid Id { get; private set; }
            public ICommand OpenParentEditViewCommand { get; private set; }

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