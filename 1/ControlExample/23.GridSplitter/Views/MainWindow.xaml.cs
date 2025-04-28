using Prism.Regions;
using System.Windows;

namespace GridSplitter.Views
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
            _regionManager.RequestNavigate("ContentRegion", nameof(GridSplitterView), result =>
            {
                if (result.Result == false)
                    MessageBox.Show("Navigation 실패: GridSplitterView 못 찾음");
            });
        }
    }
}
