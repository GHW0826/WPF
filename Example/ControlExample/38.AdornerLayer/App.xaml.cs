using AdornerLayer.ViewModels;
using AdornerLayer.Views;
using Prism.Ioc;
using System.Windows;

namespace AdornerLayer
{
    // 1. Adorner 기본 구성
    // 2. MVVM으로 Adorner 표시 토글
    // 3. 커스텀 Adorner 스타일 적용
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<AdornerLayerView, AdornerLayerViewModel>();
        }
    }
}
