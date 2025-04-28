using WrapPanel.Views;
using Prism.Ioc;
using System.Windows;
using WrapPanel.ViewModels;

namespace WrapPanel
{
    // 1. 기본 WrapPanel 생성 + 버튼 여러 개 배치
    // 2. WrapPanel 방향 변경 (가로 ↔ 세로)
    // 3. WrapPanel 내부 아이템 크기 고정/유동 설정
    // 4. WrapPanel 동적 아이템 추가 (MVVM 연동)
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<WrapPanelView, WrapPanelViewModel>();
        }
    }
}
