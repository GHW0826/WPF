using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StaticDynamicResource
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void OnChangeBrushClick(object sender, RoutedEventArgs e)
        {
            // Application 리소스를 변경 (키는 동일)
            Application.Current.Resources["StaticBrush"] = new SolidColorBrush(Colors.LightSalmon);
            Application.Current.Resources["DynamicBrush"] = new SolidColorBrush(Colors.LightSalmon);
        }
    }
}
