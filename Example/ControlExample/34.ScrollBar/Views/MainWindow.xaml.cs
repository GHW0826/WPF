using Prism.Regions;
using System.Windows;

namespace ScrollBar.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
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
            _regionManager.RequestNavigate("ContentRegion", nameof(ScrollBarView), result =>
            {
                if (result.Result == false)
                    MessageBox.Show("Navigation 실패: ScrollBarView 못 찾음");
            });
        }
    }
}
