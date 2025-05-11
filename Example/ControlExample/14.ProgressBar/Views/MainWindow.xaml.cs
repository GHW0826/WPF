using Prism.Regions;
using System.Windows;

namespace ProgressBar.Views
{
    public partial class MainWindow : Window
    {
        private readonly IRegionManager _regionManager;

        public MainWindow(IRegionManager regionManager)
        {
            InitializeComponent();
            _regionManager = regionManager;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _regionManager.RequestNavigate("ContentRegion", nameof(ProgressBarView), result =>
            {
                if (result.Result == false)
                    MessageBox.Show("Navigation 실패: ProgressBarView 못 찾음");
            });
        }
    }
}
