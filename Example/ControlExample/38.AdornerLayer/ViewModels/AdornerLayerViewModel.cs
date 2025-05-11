using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

namespace AdornerLayer.ViewModels
{
    public partial class AdornerLayerViewModel : ObservableObject
    {
        public AdornerLayerViewModel()
        {
            ToggleAdornerCommand = new RelayCommand(OnToggleAdorner);
        }

        [ObservableProperty]
        private bool isAdornerVisible;

        public IRelayCommand ToggleAdornerCommand { get; }

        private void OnToggleAdorner()
        {
            IsAdornerVisible = !IsAdornerVisible;
        }
    }
}
