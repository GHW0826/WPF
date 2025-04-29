using MediaElement.ViewModels;
using Microsoft.Win32;
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

namespace MediaElement.Views
{
    /// <summary>
    /// MediaElementView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MediaElementView : UserControl
    {
        public MediaElementView()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is MediaElementViewModel vm)
            {
                vm.RequestPlay += () => mediaPlayer.Play();
                vm.RequestStop += () => mediaPlayer.Stop();
                vm.RequestOpenFile += () => OpenMediaFile(vm);
            }
        }
        private void OpenMediaFile(MediaElementViewModel vm)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Media files (*.mp4;*.mp3;*.wmv;*.avi)|*.mp4;*.mp3;*.wmv;*.avi|All files (*.*)|*.*"
            };

            if (dlg.ShowDialog() == true)
            {
                vm.SetSource(dlg.FileName);
            }
        }
    }
}
