using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace VirtualizingStackPanel.ViewModels
{
    public partial class VirtualizingStackPanelViewModel : ObservableObject
    {

        [ObservableProperty]
        private ObservableCollection<string> items;
        public VirtualizingStackPanelViewModel()
        {
            Items = new ObservableCollection<string>();

            for (int i = 1; i <= 10000; i++)
            {
                Items.Add($"Item {i}");
            }
        }

    }
}
