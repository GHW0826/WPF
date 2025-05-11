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

namespace TemplateBindingRelativeSource
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

        private void btnTemplateBinding_Click(object sender, RoutedEventArgs e)
        {
            btnTemplateBinding.Background = btnTemplateBinding.Background == System.Windows.Media.Brushes.LightBlue
                ? System.Windows.Media.Brushes.LightGreen
                : System.Windows.Media.Brushes.LightBlue;
        }

        private void btnRelativeSource_Click(object sender, RoutedEventArgs e)
        {
            btnRelativeSource.Background = btnRelativeSource.Background == System.Windows.Media.Brushes.LightCoral
                ? System.Windows.Media.Brushes.LightYellow
                : System.Windows.Media.Brushes.LightCoral;
        }
    }
}
