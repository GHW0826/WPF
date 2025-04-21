using Example3.Modules;
using Example3.Views;
using Prism.Ioc;
using System.Windows;

namespace Example3
{
    public partial class App
    {

        // 1. Validation. [Required], [MinLength] + ValidateProperty(...)
        // 2. Command 연동. 이름이 유효해야 버튼이 활성화 CanExecute + NotifyCanExecuteChanged() 
        // 3. 입력 제한 – 숫자만 허용(붙여넣기 차단)
        // 4. 자동 포커스 이동 기능
        // 5. Placeholder (converter로 구현)
        // 6. Debounce 검색 기능 
        // 7. IME 고려 (한글 입력 중 조합 상태 처리)


        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // containerRegistry.RegisterForNavigation<HomeView, HomeViewModel>(); // 유지
            // containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>(); // ✅ 추가
            containerRegistry.RegisterForNavigation<UserInputView, UserInputViewModel>(); // ✅ 추가
            containerRegistry.RegisterForNavigation<ValidatedInputView, ValidatedInputViewModel>(); // ✅ 추가
        }
    }
}
