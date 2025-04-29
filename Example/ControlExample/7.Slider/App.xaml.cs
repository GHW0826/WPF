using Slider.Views;
using Prism.Ioc;
using System.Windows;
using Slider.ViewModels;

namespace Slider
{
    // 1. 기본 바인딩
    // 2. Slider 값 변경 시 실시간 메시지 반영
    // 3. Threshold 구간별 색상 변경
    // 4. Disable 상태/최소 조건 설정
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SliderView, SliderViewModel>();
        }
    }
}
