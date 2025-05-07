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

namespace Freezables
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private SolidColorBrush _sharedBrush;

        public MainWindow()
        {
            InitializeComponent();
            _sharedBrush = new SolidColorBrush(Colors.LightBlue);
            _sharedBrush.Freeze(); // Freezable 적용 (불변 상태)
            rectFreezable.Fill = _sharedBrush;
        }

        private void btnToggleColor_Click(object sender, RoutedEventArgs e)
        {
            if (_sharedBrush.IsFrozen)
            {
                MessageBox.Show("Brush is Frozen (Immutable)");
                return;
            }

            _sharedBrush.Color = _sharedBrush.Color == Colors.LightBlue ? Colors.LightGreen : Colors.LightBlue;
        }
    }
}
