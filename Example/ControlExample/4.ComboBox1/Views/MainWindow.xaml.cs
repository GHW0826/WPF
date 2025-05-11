using Prism.Regions;
using System.Windows;

namespace ComboBox1.Views
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
            _regionManager.RequestNavigate("ContentRegion", "ComboBoxView", result =>
            {
                if (result.Result == false)
                    MessageBox.Show("Navigation 실패: ComboBoxView 못 찾음");
            });
        }
    }
}
