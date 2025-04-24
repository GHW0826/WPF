using TreeView.Views;
using Prism.Ioc;
using System.Windows;
using TreeView.ViewModels;

namespace TreeView
{
    // 1. 기본 TreeView 구성
    // 2. 선택된 노드 바인딩
    // 3. 동적 노드 추가/삭제
    // 4. 트리 탐색 + UI 반응
    // 5. CheckBox 트리 노드
    // 6. ContextMenu + Command  (command가 안먹음)
    // 7. 아이콘, 스타일링
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<TreeViewView, TreeViewModel>();
        }
    }
}
