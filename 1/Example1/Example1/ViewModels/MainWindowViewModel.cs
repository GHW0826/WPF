using Example1.Views;
using Prism.Mvvm;
using Prism.Regions;

namespace Example1.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
        public MainWindowViewModel(IRegionManager regionManager)
        {
            regionManager.RequestNavigate("ContentRegion", nameof(HomeView));
        }
    }
}
