using ListView.ViewModels;
using ListView.Views;
using Prism.Ioc;
using System.Windows;

namespace ListView
{
    // 1. 기본 ListView 사용하기
    // 2. 선택 항목 바인딩하기 (SelectedItem)
    // 3. DataTemplate으로 아이템 스타일링
    // 4. Header 추가 + GridView 컬럼 나누기
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ListViewView, ListViewViewModel>();
        }
    }
}
