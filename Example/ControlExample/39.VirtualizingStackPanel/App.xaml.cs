using VirtualizingStackPanel.Views;
using Prism.Ioc;
using System.Windows;
using VirtualizingStackPanel.ViewModels;

namespace VirtualizingStackPanel
{
    // 1. VirtualizingStackPanel 기본 구성
    // 2. MVVM으로 대량 데이터 바인딩
    // 3. 가상화 성능 테스트 및 스크롤 최적화
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<VirtualizingStackPanelView, VirtualizingStackPanelViewModel>();
        }
    }
}
