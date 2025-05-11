using CheckBox.ViewModels;
using CheckBox.Views;
using Prism.Ioc;
using System.Windows;

namespace CheckBox
{
    // 1. 단일 체크박스 -> 버튼 활성화
    // 2. 다중 체크박스 전체 동의 구성 (개별 항목 + 전체 선택)
    // 3. Validation 연동 (AdornerDecorator 대상 아님)
    // 4. Command 연동 (확정시 서버 전송 등.)
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CheckBoxView, CheckBoxViewModel>();
        }
    }
}
