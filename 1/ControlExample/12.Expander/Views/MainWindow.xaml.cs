using Prism.Regions;
using System.Windows;

namespace Expander.Views
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
            _regionManager.RequestNavigate("ContentRegion", nameof(ExpanderView), result =>
            {
                if (result.Result == false)
                    MessageBox.Show("Navigation 실패: ExpanderView 못 찾음");
            });
        }
    }
}
