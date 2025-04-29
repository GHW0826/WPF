using Calendar.ViewModels;
using Calendar.Views;
using Prism.Ioc;
using System.Windows;

namespace Calendar
{
    // 1. 기본 Calendar MVVM 바인딩
    // 2. 범위 지정
    // 3. 다중 선택 제어 (SelectionMode)
    // 4. BlackoutDates 처리
    // 5. DisplayDate 조작
    // 6. 스타일 커스터마이징 (Light)
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<CalendarView, CalendarViewModel>();
        }
    }
}
