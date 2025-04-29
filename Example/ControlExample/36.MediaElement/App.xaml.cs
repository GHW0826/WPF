using MediaElement.ViewModels;
using MediaElement.Views;
using Prism.Ioc;
using System.Windows;

namespace MediaElement
{
    // 1. MediaElement 기본 구성
    // 2. MediaElement MVVM 연결
    // 3. 파일 열기 (OpenFileDialog) 연결
    // 4. 재생 위치(Seek) 슬라이더 추가
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MediaElementView, MediaElementViewModel>();
        }
    }
}
