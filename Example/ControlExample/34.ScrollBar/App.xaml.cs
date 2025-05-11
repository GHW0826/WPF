using ScrollBar.Views;
using Prism.Ioc;
using System.Windows;
using ScrollBar.ViewModels;

namespace ScrollBar
{
    // 1. ScrollBar 기본 구성
    // 2. ScrollBar MVVM 바인딩 연결
    // 3. ScrollBar Value에 따라 콘텐츠 이동 제어
    // 4. MVVM으로 양방향 Value Update
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ScrollBarView, ScrollBarViewModel>();
        }
    }
}
