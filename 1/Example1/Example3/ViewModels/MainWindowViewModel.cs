using Prism.Mvvm;
using Prism.Regions;
using System.Diagnostics;

namespace Example3.ViewModels
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
            // Stage 1: HomeView
            // regionManager.RequestNavigate("ContentRegion", nameof(Modules.Home.HomeView));

            // ✅ Stage 2: LoginView로 초기 진입 변경
            // regionManager.RequestNavigate("ContentRegion", "LoginView");
            //regionManager.RequestNavigate("ContentRegion", "LoginView", result =>
            //{
            //    if (result.Result == false)
            //    {
            //        Debug.WriteLine($"[Navigation Error] {result.Error}");
            //    }
            //});
        }
    }
}
