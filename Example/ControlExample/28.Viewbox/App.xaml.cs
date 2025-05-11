using Viewbox.Views;
using Prism.Ioc;
using System.Windows;
using Viewbox.ViewModels;

namespace Viewbox
{
    // 1. 기본 Viewbox 사용하기
    // 2. Stretch 옵션 조정해보기
    // 3. MVVM 바인딩으로 크기 조절 실습 (축소, 확대 적용 나중에 다시)
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ViewboxView, ViewboxViewModel>();
        }
    }
}
