using Prism.Navigation.Regions;
using System.Windows;

namespace RichTextBox.Views
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
            _regionManager.RequestNavigate("ContentRegion", nameof(RichTextBoxView), result =>
            {
                if (result.Success == false)
                    MessageBox.Show("Navigation 실패: RichTextBoxView 못 찾음");
            });
        }
    }
}
