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

namespace Viewbox.ViewModels
{
    public partial class ViewboxViewModel : ObservableObject
    {
        [ObservableProperty]
        private double scale = 1.0;

        public IRelayCommand ZoomInCommand { get; }
        public IRelayCommand ZoomOutCommand { get; }

        public ViewboxViewModel()
        {
            ZoomInCommand = new RelayCommand(OnZoomIn);
            ZoomOutCommand = new RelayCommand(OnZoomOut);
        }

        private void OnZoomIn()
        {
            Scale += 0.1;
        }

        private void OnZoomOut()
        {
            Scale = Math.Max(0.1, Scale - 0.1);
        }
    }
}
