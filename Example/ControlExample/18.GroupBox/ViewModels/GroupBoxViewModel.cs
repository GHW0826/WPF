using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prism.Common;
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

namespace GroupBox.ViewModels
{
    public partial class GroupBoxViewModel : ObservableObject
    {
        [ObservableProperty]
        private string nameInput;
        public IRelayCommand SubmitCommand { get; }

        ////////////////////////////////////////////////////////

        [ObservableProperty]
        private bool isGroupEnabled = true;
        public IRelayCommand ToggleEnableCommand { get; }


        public GroupBoxViewModel()
        {
            SubmitCommand = new RelayCommand(OnSubmit);
            ToggleEnableCommand = new RelayCommand(OnToggleEnable);
        }

        private void OnSubmit()
        {
            MessageBox.Show($"이름 입력됨: {NameInput}");
        }

        private void OnToggleEnable()
        {
            IsGroupEnabled = !IsGroupEnabled;
        }
    }
}
