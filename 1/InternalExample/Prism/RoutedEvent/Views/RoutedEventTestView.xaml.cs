using System.Diagnostics;
using System.Windows.Controls;
using System.Windows.Input;

namespace RoutedEvent.Views
{
    /// <summary>
    /// Interaction logic for RoutedEventTestView
    /// </summary>
    public partial class RoutedEventTestView : UserControl
    {
        public RoutedEventTestView()
        {
            InitializeComponent();
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

            // e.Handled = true; // 이걸 주석 해제하면 여기서 이벤트 흐름을 끊을 수 있음
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Debug.WriteLine("Border MouseDown (버블링)");
        }
    }
}
