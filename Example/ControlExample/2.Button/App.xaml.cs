using Button.ViewModels;
using Button.Views;
using Prism.Ioc;
using System.Windows;

namespace Button
{
    // 1. 기본 바인딩
    // 2. CommandParameter 전달
    // 3. 상태 제어 (IsEnabled)
    // 4. 스타일/아이콘 버튼
    // 5. 버튼 그룹화/토글
    // 6 애니메이션/효과
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ButtonView, ButtonViewModel>();
        }
    }
}
