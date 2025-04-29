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

namespace ScrollViewer.Views
{
    public partial class ScrollViewerView : UserControl
    {
        public ScrollViewerView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ScrollViewerViewModel vm)
            {
                vm.PropertyChanged += (s, args) =>
                {
                    if (args.PropertyName == nameof(vm.ScrollPosition))
                    {
                        // ScrollToVerticalOffset 호출
                        MainScrollViewer.ScrollToVerticalOffset(vm.ScrollPosition);
                        MainScrollViewer2.ScrollToVerticalOffset(vm.ScrollPosition);
                        MainScrollViewer3.ScrollToVerticalOffset(vm.ScrollPosition);
                    }
                };
            }
        }
    }
}
