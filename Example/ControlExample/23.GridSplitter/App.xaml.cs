using GridSplitter.ViewModels;
using GridSplitter.Views;
using Prism.Ioc;
using System.Windows;

namespace GridSplitter
{
    // 1. 기본 Grid + GridSplitter 수평 분할
    // 2. 수직 분할 (위아래 나누기)
    // 3. 3분할 이상 레이아웃 만들기
    // 4. MVVM으로 영역 비율 초기 세팅
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<GridSplitterView, GridSplitterViewModel>();
        }
    }
}
