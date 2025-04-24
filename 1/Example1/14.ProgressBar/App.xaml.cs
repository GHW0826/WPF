using ProgressBar.Views;
using Prism.Ioc;
using System.Windows;
using ProgressBar.ViewModels;

namespace ProgressBar
{
    // 1. 기본 바인딩
    // 2. 버튼 클릭시 비동기 예제
    // 3. Task.Delay로 progress++ 시뮬
    // 4. 작업 중 상태 표시 (IsBusy, 버튼 비활성화)
    // 5. Cancel 지원
    // 6. Indeterminate 모드로 애니메이션 효과
    // 7. Style 또는 Template로 모양 커스터마이징
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ProgressBarView, ProgressBarViewModel>();
        }
    }
}
