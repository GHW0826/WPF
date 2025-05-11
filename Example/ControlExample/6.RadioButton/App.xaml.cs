using RadioButton.Views;
using Prism.Ioc;
using System.Windows;
using RadioButton.ViewModels;

namespace RadioButton
{
    // 1. 기본 선택 바인딩 + 초기 선택값 지정
    // 2. 선택 강제 유효성 검사 (AdornerDecorator 대상 아님)
    // 3. Command 연동
    // 4. Label/ 이미지 등과 함꼐 커스터마이징
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<RadioButtonView, RadioButtonViewModel>();
        }
    }
}
