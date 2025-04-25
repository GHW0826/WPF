using TextBox.Views;
using Prism.Ioc;
using System.Windows;
using TextBox.ViewModels;

namespace TextBox
{

    // 1. 기본 바인딩
    // 2. Placeholder & 스타일
    // 3. MaxLength, IsReadOnly, TextWrapping
    // 4. 유효성 검사
    // 5. IME 입력 처리 + 커맨드 연동
    // 6. 다국어 바인딩 + 포맷팅
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<TextBoxView, TextBoxViewModel>();
        }
    }
}
