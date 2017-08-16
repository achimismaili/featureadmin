using FeatureAdminForm.ActivatedFeatures;
using FeatureAdminForm.Common;
using FeatureAdminForm.FeatureDefs;
using FeatureAdminForm.SharePointContainers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;

namespace FeatureAdminForm
{
    public class MainWindowViewModel : BindableBase
    {
        private ActivatedFeatureListViewModel activatedFeatureListViewModel;
        private FeatureDefsListViewModel featureDefinitionListViewModel;
        private SharePointContainerListViewModel sharePointContainerListViewModel;

        private BindableBase currentViewModel;
        public BindableBase CurrentViewModel{
            get { return currentViewModel; }
            set { SetProperty(ref currentViewModel, value); }
        }

        public RelayCommand<string> NavCommand { get; private set; }

        public MainWindowViewModel()
        {
            activatedFeatureListViewModel = ContainerHelper.Container.Resolve<ActivatedFeatureListViewModel>();
            featureDefinitionListViewModel = ContainerHelper.Container.Resolve<FeatureDefsListViewModel>();
            sharePointContainerListViewModel = ContainerHelper.Container.Resolve<SharePointContainerListViewModel>();
            NavCommand = new RelayCommand<string>(OnNav);
            //_customerListViewModel.PlaceOrderRequested += NavToOrder;
            //_customerListViewModel.AddCustomerRequested += NavToAddCustomer;
            //_customerListViewModel.EditCustomerRequested += NavToEditCustomer;
            //_addEditViewModel.Done += NavToCustomerList;
        }

        private void OnNav(string destination)
        {
            switch (destination)
            {
                case Common.Constants.NavigationDestinations.ActivatedFeatures:
                    CurrentViewModel = activatedFeatureListViewModel;
                    break;
                case Common.Constants.NavigationDestinations.FeatureDefinitions:
                default:
                    CurrentViewModel = featureDefinitionListViewModel;
                    break;
            }
        }

        //private void NavToOrder(Guid customerId)
        //{
        //    _orderViewModel.CustomerId = customerId;
        //    CurrentViewModel = _orderViewModel;
        //}

        //private void NavToAddCustomer(Customer cust)
        //{
        //    //_addEditViewModel.EditMode = false;
        //    //_addEditViewModel.SetCustomer(cust);
        //    //CurrentViewModel = _addEditViewModel;
        //}

        //private void NavToEditCustomer(Customer cust)
        //{
        //    //_addEditViewModel.EditMode = true;
        //    //_addEditViewModel.SetCustomer(cust);
        //    //CurrentViewModel = _addEditViewModel;
        //}

        //private void NavToCustomerList()
        //{
        //    //CurrentViewModel = _customerListViewModel;
        //}

    }
}
