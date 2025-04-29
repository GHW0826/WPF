using TextBlock.Views;
using Prism.Ioc;
using System.Windows;
using TextBlock.ViewModels;

namespace TextBlock
{
    // 1. 텍스트 바인딩, 줄바꿈 처리
    // 2. 인라인 서식 텍스트
    // 3. 하이퍼링크 추가
    // 4. 다국어 바인딩
    // 5. 툴팁과 강조
    // 6. Inlines 동적 생성(view model에서)
    // 7. 텍스트 강조 스타일
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }


        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<TextBlockView, TextBlockViewModel>();
        }
    }
}
