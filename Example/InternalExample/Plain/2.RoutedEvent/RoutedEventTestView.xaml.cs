using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace RoutedEvent
{
    public partial class RoutedEventTestView : UserControl
    {
        public RoutedEventTestView()
        {
            InitializeComponent();
            this.DataContext = new RoutedEventTestViewModel();
        }

        private void Grid_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("Grid PreviewMouseDown (터널링)");
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("Grid MouseDown (버블링)");
        }

        private void StackPanel_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("StackPanel PreviewMouseDown (터널링)");
        }

        private void StackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("StackPanel MouseDown (버블링)");
        }

        private void Border_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("Border PreviewMouseDown (터널링)");

            // e.Handled = true; // 이걸 주석 해제하면 이벤트가 여기서 멈춘다
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("Border MouseDown (버블링)");
        }
    }
}
