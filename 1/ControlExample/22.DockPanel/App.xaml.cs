using DockPanel.ViewModels;
using DockPanel.Views;
using Prism.Ioc;
using System.Windows;

namespace DockPanel
{
    // 1. 기본 DockPanel 구조 만들기
    // 2. DockPanel 내부 정렬 조정 (LastChildFill 제어)
    // 3. MVVM 바인딩으로 DockPanel 내부 동적 제어
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<DockPanelView, DockPanelViewModel>();
        }
    }
}
