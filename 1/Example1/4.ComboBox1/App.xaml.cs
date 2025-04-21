using ComboBox1.ViewModels;
using ComboBox1.Views;
using Prism.Ioc;
using System.Windows;

namespace ComboBox1
{
    // 1. 기본 바인딩 + (초기 선택값 설정)
    // 2. 선택 유효성 검사
    // 3. Command 연동
    // 4. 선택 값에 따른 분기 처리
    // 5. ComboBox 동적 목록
    // 6. ComboBox + Enum 바인딩
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ComboBoxView, ComboBoxViewModel>(); // ✅ 추가
        }
    }
}
