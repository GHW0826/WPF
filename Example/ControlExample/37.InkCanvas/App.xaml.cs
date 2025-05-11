using InkCanvas.ViewModels;
using InkCanvas.Views;
using Prism.Ioc;
using System.Windows;

namespace InkCanvas
{
    // 1. InkCanvas 기본 구성
    // 2. InkCanvas MVVM 연결
    // 3. InkCanvas Clear(지우기) 기능 추가
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<InkCanvasView, InkCanvasViewModel>();
        }
    }
}
