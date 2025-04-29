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

namespace ToolBarStatusBar.ViewModels
{
    public partial class ToolBarStatusBarViewModel : ObservableObject
    {
        [ObservableProperty]
        private string statusMessage;

        public IRelayCommand NewCommand { get; }
        public IRelayCommand OpenCommand { get; }
        public IRelayCommand SaveCommand { get; }


        public ToolBarStatusBarViewModel()
        {
            NewCommand = new RelayCommand(OnNew);
            OpenCommand = new RelayCommand(OnOpen);
            SaveCommand = new RelayCommand(OnSave);
            StatusMessage = "Ready";
        }


        private void OnNew()
        {
            StatusMessage = "New 작업 시작";
        }

        private void OnOpen()
        {
            StatusMessage = "파일 열기";
        }

        private void OnSave()
        {
            StatusMessage = "파일 저장 완료";
        }
    }
}
