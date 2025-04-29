using ToolTip.Views;
using Prism.Ioc;
using System.Windows;
using ToolTip.ViewModels;

namespace ToolTip
{
    // 1. 기본 ToolTip 적용
    // 2. MVVM 바인딩으로 ToolTip 내용 제어
    // 3. ToolTip 스타일 꾸미기
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ToolTipView, ToolTipViewModel>();
        }
    }
}
