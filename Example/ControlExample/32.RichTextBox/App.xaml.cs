using RichTextBox.Views;
using Prism.Ioc;
using System.Windows;
using RichTextBox.ViewModels;

namespace RichTextBox
{
    /*
    RichTextBox의 Document는 "전체 객체"라서 바인딩 자체가 Property-level ChangeTracking이 안 된다.
    FlowDocument 자체는 INotifyPropertyChanged를 지원하지 않고,
    RichTextBox는 내부에서 Document를 새로 만드는 타이밍이 있음 (특히 Navigation 시점에)
    그래서 ViewModel에 FlowDocument를 만들어두고,
    View가 Loaded 됐을 때 직접 세팅해주는 게 가장 안정적이고 표준적인 WPF 패턴
    */

    // 1. 기본 RichTextBox MVVM 연결
    // 2. Bold/Italic/Underline 포맷 적용
    // 3. Color(텍스트 색상) 변경
    // 4. 텍스트 저장/불러오기
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<RichTextBoxView, RichTextBoxViewModel>();
        }
    }
}
