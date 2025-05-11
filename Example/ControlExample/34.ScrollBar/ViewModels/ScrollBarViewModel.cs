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

namespace ScrollBar.ViewModels
{
    public partial class ScrollBarViewModel : ObservableObject
    {
        [ObservableProperty]
        private double minimum;

        [ObservableProperty]
        private double maximum;

        [ObservableProperty]
        private double value;

        public IRelayCommand MoveToStartCommand { get; }
        public IRelayCommand MoveToMiddleCommand { get; }
        public IRelayCommand MoveToEndCommand { get; }

        public ScrollBarViewModel() {
            Minimum = 0;
            Maximum = 200;
            Value = 0;


            MoveToStartCommand = new RelayCommand(OnMoveToStart);
            MoveToMiddleCommand = new RelayCommand(OnMoveToMiddle);
            MoveToEndCommand = new RelayCommand(OnMoveToEnd);
        }

        private void OnMoveToStart()
        {
            Value = Minimum;
        }

        private void OnMoveToMiddle()
        {
            Value = (Minimum + Maximum) / 2;
        }

        private void OnMoveToEnd()
        {
            Value = Maximum;
        }
    }
}
