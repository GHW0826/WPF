using MultiBinding.ViewModels;
using MultiBinding.Views;
using Prism.Ioc;
using System.Windows;

namespace MultiBinding
{
    // 1. 기본 MultiBinding 적용
    // 2. MultiBinding + Converter 사용
    // 3. ViewModel 속성에 MultiBinding 반영
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<MultiBindingView, MultiBindingViewModel>();
        }
    }
}
