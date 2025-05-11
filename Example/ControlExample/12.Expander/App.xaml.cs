using Expander.ViewModels;
using Expander.Views;
using Prism.Ioc;
using System.Windows;

namespace Expander
{
    // 1. 기본 Expander
    // 2. IsExpanded 바인딩
    // 3. Command 연동
    // 4. 스타일 커스터마이징
    // 5. 중첩 Expander
    // 6. List + Expander
    // 7. ViewModel List 연동
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ExpanderView, ExpanderViewModel>();
        }
    }
}
