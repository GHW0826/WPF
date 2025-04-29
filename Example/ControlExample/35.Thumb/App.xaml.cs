using Thumb.Views;
using Prism.Ioc;
using System.Windows;
using Thumb.ViewModels;

namespace Thumb
{
    // 1. Thumb 기본 구성
    // 2. Thumb MVVM 연결
    // 3. Thumb 위치를 ViewModel에서 강제 이동
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ThumbView, ThumbViewModel>();
        }
    }
}
