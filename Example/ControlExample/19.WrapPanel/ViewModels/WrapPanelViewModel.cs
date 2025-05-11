using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

namespace WrapPanel.ViewModels
{
    public partial class WrapPanelViewModel : ObservableObject
    {
        [ObservableProperty]
        private Orientation wrapOrientation = Orientation.Horizontal;

        public IRelayCommand ToggleOrientationCommand { get; }


        ////////////////////////////////////////////////////////////////////

        public ObservableCollection<string> ButtonLabels { get; } = new();
        public IRelayCommand AddButtonCommand { get; }

        public WrapPanelViewModel()
        {
            ToggleOrientationCommand = new RelayCommand(OnToggleOrientation);

            ////////////////////////////////////////////////////////////////////

            // 기본 버튼 초기 추가
            ButtonLabels.Add("버튼 1");
            ButtonLabels.Add("버튼 2");
            ButtonLabels.Add("버튼 3");
            AddButtonCommand = new RelayCommand(OnAddButton);
        }

        private void OnToggleOrientation()
        {
            WrapOrientation = (WrapOrientation == Orientation.Horizontal) ? Orientation.Vertical : Orientation.Horizontal;
        }


        ////////////////////////////////////////////////////////////////////

        private void OnAddButton()
        {
            ButtonLabels.Add($"추가 버튼 {ButtonLabels.Count + 1}");
        }
    }
}
