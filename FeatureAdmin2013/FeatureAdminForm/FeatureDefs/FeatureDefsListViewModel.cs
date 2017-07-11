using FeatureAdminForm.Services;

namespace FeatureAdminForm.FeatureDefs
{
    public class FeatureDefsListViewModel : BindableBase
    {
        private IFeatureRepository repo;

        public FeatureDefsListViewModel(IFeatureRepository repository)
        {
            repo = repository;

//            LoadFeatureDefinitions (repository);
        }

        //private ObservableCollection<FeatureDefinition> _featureDefinitions;
        //public ObservableCollection<FeatureDefinition> FeatureDefinitions
        //{
        //    get { return _featureDefinitions; }
        //    set { SetProperty(ref _featureDefinitions, value); }
        //}

        //public void LoadFeatureDefinitions()
        //{
        //    FeatureDefinitions = new ObservableCollection<FeatureDefinition>(
        //        repo.GetFeatureDefinitions());
        //}

        // public RelayCommand<FeatureDefinition> UninstallCommand { get; private set; }

        //private void OnUninstall ( FeatureDefinition fd)
        //{

        //}

    }
}
