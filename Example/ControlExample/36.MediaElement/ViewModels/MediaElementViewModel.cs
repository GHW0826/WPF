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

namespace MediaElement.ViewModels
{
    
    public partial class MediaElementViewModel : ObservableObject
    {

        [ObservableProperty]
        private string source;
        public IRelayCommand PlayCommand { get; }
        public IRelayCommand StopCommand { get; }
        public IRelayCommand OpenFileCommand { get; }

        public event Action? RequestPlay;
        public event Action? RequestStop;
        public event Action? RequestOpenFile;

        public MediaElementViewModel()
        {
            Source = "sample.mp4"; // 나중에 파일 선택 기능 추가 예정

            PlayCommand = new RelayCommand(OnPlay);
            StopCommand = new RelayCommand(OnStop);
            OpenFileCommand = new RelayCommand(OnOpenFile);
        }

        private void OnPlay()
        {
            RequestPlay?.Invoke();
        }

        private void OnStop()
        {
            RequestStop?.Invoke();
        }
        private void OnOpenFile()
        {
            RequestOpenFile?.Invoke();
        }

        public void SetSource(string filePath)
        {
            Source = filePath;
        }
    }
}
