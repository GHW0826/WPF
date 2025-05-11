using ToolBarStatusBar.Views;
using Prism.Ioc;
using System.Windows;
using ToolBarStatusBar.ViewModels;

namespace ToolBarStatusBar
{
    // 1. ToolBar 기본 구성
    // 2. ToolBar MVVM Command 연결
    // 3. StatusBar 기본 구성
    // 4. StatusBar MVVM 상태 표시 연동
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ToolBarStatusBarView, ToolBarStatusBarViewModel>();
        }
    }
}
