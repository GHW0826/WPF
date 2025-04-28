using Prism.Regions;
using System.Windows;

namespace ListBox.Views
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
            _regionManager.RequestNavigate("ContentRegion", nameof(ListBoxView), result =>
            {
                if (result.Result == false)
                    MessageBox.Show("Navigation 실패: ListBoxView 못 찾음");
            });
        }
    }
}
