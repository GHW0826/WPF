﻿using CommunityToolkit.Mvvm.ComponentModel;
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

namespace DockPanel.ViewModels
{
   
    public partial class DockPanelViewModel : ObservableObject
    {
        [ObservableProperty]
        private Visibility leftVisibility = Visibility.Visible;

        [ObservableProperty]
        private Visibility rightVisibility = Visibility.Visible;

        public IRelayCommand ToggleLeftCommand { get; }
        public IRelayCommand ToggleRightCommand { get; }

        public DockPanelViewModel()
        {
            ToggleLeftCommand = new RelayCommand(OnToggleLeft);
            ToggleRightCommand = new RelayCommand(OnToggleRight);
        }

        private void OnToggleLeft()
        {
            LeftVisibility = (LeftVisibility == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;
        }

        private void OnToggleRight()
        {
            RightVisibility = (RightVisibility == Visibility.Visible) ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
