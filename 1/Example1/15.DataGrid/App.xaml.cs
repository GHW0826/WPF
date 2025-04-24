using DataGrid.ViewModels;
using DataGrid.Views;
using Prism.Ioc;
using System.Windows;

namespace DataGrid
{
    // 1. 기본 바인딩
    // 2. 수동 컬럼 정의
    // 3. Row 편집 활성화
    // 4. 커맨드 연동
    // 5. 셀 편집 & 커스텀 편집기
    // 6. 조건부 색상 스타일
    // 7. 정렬/필터
    // 8. 헤더 & 셀 템플릿
    // 9. Validation 연동
    // 10. 가상화 성능 & 커스텀 스크롤
    // 11. Excel 내보내기, 복사/붙여넣기
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<DataGridView, DataGridViewModel>();
        }
    }
}
