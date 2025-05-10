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

namespace ThemeDictionary
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
        private void OnDarkThemeClick(object sender, RoutedEventArgs e)
        {
            SwitchTheme("Themes/Dark.xaml");
        }

        private void OnLightThemeClick(object sender, RoutedEventArgs e)
        {
            SwitchTheme("Themes/Light.xaml");
        }

        private void SwitchTheme(string path)
        {
            var newTheme = new ResourceDictionary { Source = new System.Uri(path, System.UriKind.Relative) };

            var appRes = Application.Current.Resources;
            appRes.MergedDictionaries.Clear();
            appRes.MergedDictionaries.Add(newTheme);
        }
    }
}
