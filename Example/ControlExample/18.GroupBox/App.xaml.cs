using GroupBox.ViewModels;
using GroupBox.Views;
using Prism.Ioc;
using System.Windows;

namespace GroupBox
{
    // 1. 기본 GroupBox 생성 + Header 표시
    // 2. GroupBox 안에 입력 필드 추가 (TextBox, Button)
    // 3. GroupBox 스타일 변경 (Border 색/두께/여백 조정)
    // 4. GroupBox Enable/Disable 상태 연동 (MVVM 바인딩)
    // 5. GroupBox 내부 Layout 정리 (StackPanel)
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<GroupBoxView, GroupBoxViewModel>();
        }
    }
}
