using PasswordBox.ViewModels;
using PasswordBox.Views;
using Prism.Ioc;
using System.Windows;

namespace PasswordBox
{
    // 1. PasswordBox 기본 바인딩
    // 2. MaxLength, PasswordChar 설정
    // 3. 입력 오류 검증 (빈 값/ 패턴 검증)
    // 4. 보안 강화 (SecureString) 구조 확장
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PasswordBoxView, PasswordBoxViewModel>();
        }
    }
}
