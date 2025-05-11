using Canvas.ViewModels;
using Canvas.Views;
using Prism.Ioc;
using System.Windows;

namespace Canvas
{
    // 1. 기본 Canvas 사용하기
    // 2. MVVM 데이터 바인딩으로 Canvas 안에 아이템 추가
    // 3. Canvas 안 아이템을 이동시키기
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CanvasView, CanvasViewModel>();
        }
    }
}
