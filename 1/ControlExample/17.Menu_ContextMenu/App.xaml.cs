using Menu_ContextMenu.ViewModels;
using Menu_ContextMenu.Views;
using Prism.Ioc;
using System.Windows;

namespace Menu_ContextMenu
{
    // 1. Menu 기본 바인딩
    // 2. ContextMenu 기본 바인딩
    // 3. 동적 MenuItem 추가/삭제 (ItemsSource 연동)
    // 4. 서브 메뉴 구조
    // 5. 아이콘 + 단축키 표시 (InputGestureText, Icon)
    // 6. ContextMenu 동적 열기/닫기 제어 (프로그래밍 방식)
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MenuContextMenuView, MenuContextMenuViewModel>();
        }
    }
}
