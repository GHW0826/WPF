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

namespace VisualLogicalTree
{
    /// <summary>
    /// TreeStructureView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class TreeStructureView : UserControl
    {
        public TreeStructureView()
        {
            InitializeComponent();
        }
        private void OnPrintLogicalTree(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("=== Logical Tree ===");
            PrintLogicalTree(this, 0);
        }

        private void OnPrintVisualTree(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("=== Visual Tree ===");
            PrintVisualTree(this, 0);
        }

        private void PrintLogicalTree(DependencyObject parent, int indent)
        {
            foreach (var child in LogicalTreeHelper.GetChildren(parent))
            {
                if (child is DependencyObject depObj)
                {
                    Debug.WriteLine($"{new string(' ', indent * 2)}{depObj.GetType().Name}");
                    PrintLogicalTree(depObj, indent + 1);
                }
            }
        }

        private void PrintVisualTree(DependencyObject parent, int indent)
        {
            int count = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < count; ++i)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                Debug.WriteLine($"{new string(' ', indent * 2)}{child.GetType().Name}");
                PrintVisualTree(child, indent + 1);
            }
        }
    }
}
