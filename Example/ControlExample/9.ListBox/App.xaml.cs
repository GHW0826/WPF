using ListBox.ViewModels;
using ListBox.Views;
using Prism.Ioc;
using System.Windows;

namespace ListBox
{
    // 1. 기본 바인딩
    // 2. 다중 선택 항목 추적
    // 3. 선택 항목 커맨드 연동
    // 4. ItemTemplate 사용(아이콘 + 텍스트)
    // 5. 삭제 버튼 등 리스트 편집 기능
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ListBoxView, ListBoxViewModel>();
        }
    }
}
