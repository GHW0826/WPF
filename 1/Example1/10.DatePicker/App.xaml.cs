using DatePicker.ViewModels;
using DatePicker.Views;
using Prism.Ioc;
using System.Windows;

namespace DatePicker
{
    public partial class App
    {
        // 1. 기본 바인딩
        // 2. 날짜 유효성 검사
        // 3. 나이 계산
        // 4. 날짜 범위 제한
        // 5. 날짜 선택 Command 연동
        // 6. 커스텀 포맷 표시
        // 7. 시각적 커스터마이징
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<DatePickerView, DatePickerViewModel>();
        }
    }
}
