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
using WrapPanel.ViewModels;

namespace WrapPanel.Views
{
    public partial class WrapPanelView : UserControl
    {
        private WrapPanelViewModel ViewModel => DataContext as WrapPanelViewModel;

        public WrapPanelView()
        {
            InitializeComponent();
            Loaded += MainView_Loaded;
        }

        private void MainView_Loaded(object sender, RoutedEventArgs e)
        {
            RefreshButtons();
            ViewModel.ButtonLabels.CollectionChanged += (s, ev) => RefreshButtons();
        }

        private void RefreshButtons()
        {
            WrapContainer.Children.Clear();

            foreach (var label in ViewModel.ButtonLabels)
            {
                var button = new Button
                {
                    Content = label,
                    Width = 100,
                    Height = 50,
                    Margin = new Thickness(5)
                };
                WrapContainer.Children.Add(button);
            }
        }
    }
}
