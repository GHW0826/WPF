using PriorityBinding.ViewModels;
using PriorityBinding.Views;
using Prism.Ioc;
using System.Windows;

namespace PriorityBinding
{
    // 1. 기본 PriorityBinding 적용
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<PriorityBindingView, PriorityBindingViewModel>();
        }
    }
}
