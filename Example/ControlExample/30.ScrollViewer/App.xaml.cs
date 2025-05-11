using Prism.Ioc;
using ScrollViewer.Views;
using System.Windows;

namespace ScrollViewer
{
    // 1. 기본 ScrollViewer 사용하기
    // 2. 스크롤 방향 제한/제어하기
    // 3. MVVM과 ScrollViewer 연동하기
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ScrollViewerView, ScrollViewerViewModel>();
        }
    }
}
