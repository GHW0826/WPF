using TabControl.Views;
using Prism.Ioc;
using System.Windows;
using TabControl.ViewModels;

namespace TabControl
{
    // 1. 기본 탭 구성
    // 2. 탭 콘텐츠 MVVM 분리
    // 3. 동적 탭 추가/삭제
    // 4. 탭 선택 command 연동
    // 5. TabControl 스타일 커스터마이징
    // 6. Navigation 탭 연결 -- ?
    // 7. ContextMenu 및 닫기 버튼
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<TabControlView, TabControlViewModel>();
            containerRegistry.RegisterForNavigation<GeneralTabView, GeneralTabViewModel>();
            containerRegistry.RegisterForNavigation<NotificationTabView, NotificationTabViewModel>();
            containerRegistry.RegisterForNavigation<AdvancedTabView, AdvancedTabViewModel>();
            containerRegistry.RegisterForNavigation<HistoryView, HistoryViewModel>();
        }
    }
}
