using Popup.ViewModels;
using Popup.Views;
using Prism.Ioc;
using System.Windows;

namespace Popup
{
    // 1. 기본 Popup 생성 + 버튼 클릭으로 열기
    // 2. Popup 위치 조정 (Placement 속성 사용)
    // 3. Popup 열림/닫힘 MVVM 제어
    // 4. Popup 스타일/모양 꾸미기
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PopupView, PopupViewModel>();
        }
    }
}
