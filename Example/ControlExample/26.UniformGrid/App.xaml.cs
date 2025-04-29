using UniformGrid.Views;
using Prism.Ioc;
using System.Windows;
using UniformGrid.ViewModels;

namespace UniformGrid
{
    // 1. 기본 UniformGrid 사용하기
    // 2. Rows/Columns 수 직접 지정해보기
    // 3. MVVM 데이터 바인딩 연결하기
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<UniformGridView, UniformGridViewModel>();
        }
    }
}
