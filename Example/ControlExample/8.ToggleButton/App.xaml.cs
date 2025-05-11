using ToggleButton.Views;
using Prism.Ioc;
using System.Windows;
using ToggleButton.ViewModels;

namespace ToggleButton
{
    // 1. 기본 바인딩
    // 2. 버튼 상태에 따른 스타일 변경
    // 3. ToggleButton -> switch 형태 UI로 변형
    // 4. 외부 컨트롤과 상태 연동
    // 5. Command 연동
    // 6. 멀티 토글 그룹 구성
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ToggleView, ToggleViewModel>();
        }
    }
}
