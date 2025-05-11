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

namespace Thumb.ViewModels
{
    public partial class ThumbViewModel : ObservableObject
    {

        [ObservableProperty]
        private double x;

        [ObservableProperty]
        private double y;

        public IRelayCommand MoveToLeftTopCommand { get; }
        public IRelayCommand MoveToCenterCommand { get; }
        public IRelayCommand MoveToBottomRightCommand { get; }


        public ThumbViewModel()
        {
            X = 100;
            Y = 100;

            MoveToLeftTopCommand = new RelayCommand(OnMoveToLeftTop);
            MoveToCenterCommand = new RelayCommand(OnMoveToCenter);
            MoveToBottomRightCommand = new RelayCommand(OnMoveToBottomRight);
        }

        private void OnMoveToLeftTop()
        {
            X = 0;
            Y = 0;
        }

        private void OnMoveToCenter()
        {
            X = 180; // 대략 중앙
            Y = 180;
        }

        private void OnMoveToBottomRight()
        {
            X = 340; // Thumb 크기 고려해서 오른쪽 하단
            Y = 340;
        }
    }
}
