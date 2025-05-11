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
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InkCanvas.ViewModels
{
   
    public partial class InkCanvasViewModel : ObservableObject
    {

        [ObservableProperty]
        private StrokeCollection strokes;
        public IRelayCommand ClearCommand { get; }

        public InkCanvasViewModel()
        {
            Strokes = new StrokeCollection();
            ClearCommand = new RelayCommand(OnClear);
        }
        private void OnClear()
        {
            Strokes.Clear();
        }
    }
}
