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

namespace Popup.ViewModels
{
    public partial class PopupViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isPopupOpen = false;

        public IRelayCommand TogglePopupCommand { get; }


        ////////////////////////////////////////////////////////
        
        public IRelayCommand OpenPopupCommand { get; }
        public IRelayCommand ClosePopupCommand { get; }


        public PopupViewModel()
        {
            TogglePopupCommand = new RelayCommand(OnTogglePopup);


            ////////////////////////////////////////////////////////


            OpenPopupCommand = new RelayCommand(OnOpenPopup);
            ClosePopupCommand = new RelayCommand(OnClosePopup);
        }

        private void OnTogglePopup()
        {
            IsPopupOpen = !IsPopupOpen;
        }

        ////////////////////////////////////////////////////////

        private void OnOpenPopup()
        {
            IsPopupOpen = true;
        }
        private void OnClosePopup()
        {
            IsPopupOpen = false;
        }
    }
}
