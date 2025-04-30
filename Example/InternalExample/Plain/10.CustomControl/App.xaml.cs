using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace CustomControl
{
    /*
        UserControl이 아닌 CustomControl을 만들기.
        외형은 ControlTemplate으로 분리하고
        속성은 DependencyProperty로 노출한다
        스타일을 Themes/Generic.xaml로 연결해서 Themeable Control 구조 체험

        MyFancyControl	    CustomControl 생성 (Control 상속 + Style 연결)
        FancyText	        DependencyProperty 등록
        ControlTemplate	    외형 정의 (Generic.xaml 안)
        실행 시	Template이 자동으로 연결되고, FancyText 바인딩됨
    */
    /*
        [UserControl vs CustomControl]
        UserControl은 조합용
        CustomControl은 재사용/라이브러리/스킨 시스템용

        항목	        UserControl	                        CustomControl
        상속 대상	    UserControl	                        Control
        XAML 템플릿	    클래스 내부에서 UI 정의 (고정)	    외부 스타일/템플릿로 분리 가능 (동적 적용)
        재사용	        조합 기반, 확장 불리	            완전 재사용 가능, 테마 스타일 연동 가능
        Theme 대응	    ❌ 어려움	                        ✅ 가능 (Generic.xaml 사용)


        [CustomControl 생성 시 필수 구성 요소]
        구성	            설명
        클래스	            Control 상속 + DefaultStyleKeyProperty.OverrideMetadata() 호출
        스타일 위치	        Themes/Generic.xaml 파일 안
        TemplateBinding	    템플릿에서 외부 DependencyProperty 바인딩할 때 사용


        [핵심: DefaultStyleKeyProperty.OverrideMetadata(...)]
        static MyFancyControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyFancyControl),
                new FrameworkPropertyMetadata(typeof(MyFancyControl)));
        }
        이 코드를 호출하면 WPF는 Generic.xaml에서 TargetType=MyFancyControl인 스타일을 자동 적용하게 된다.


        [Generic.xaml은 왜 Themes 폴더에 넣어야 할까]
        WPF는 다음 경로를 우선순위로 탐색 
        /Themes/Generic.xaml
        
        Control이 로드될 때, DefaultStyleKey에 지정된 타입으로 Generic.xaml 내의 Style을 찾아 자동 적용


        [TemplateBinding의 역할]
        <TextBlock Text="{TemplateBinding FancyText}" />

        바인딩 종류	                        설명
        {Binding}	                        DataContext를 기준으로 바인딩
        {TemplateBinding}	                템플릿 외부에서 설정된 속성을 바인딩
        {RelativeSource TemplatedParent}	TemplateBinding의 풀 버전 (MVVM friendly)
        
        TemplateBinding은 가볍고 빠르며 ControlTemplate 안에서 외부에서 설정된 DP 값을 바로 읽을 수 있게 한다.


        [실행 흐름 전체 구조]
        <MainWindow.xaml>
            <MyFancyControl FancyText="Hello" />
                ↓
        [Control 생성됨]
                ↓
        [DefaultStyleKey 검색 → Generic.xaml 조회]
                ↓
        [Style 자동 연결 → ControlTemplate 로드]
                ↓
        [TemplateBinding으로 FancyText 값 전달]
                ↓
        [UI 렌더링]

        
        [CustomControl의 재사용성과 Themeability]
        CustomControl은 테마에 따라 외형만 바꾸고 로직은 그대로 유지할 수 있다
        Light/Dark 테마용 Generic.xaml을 여러 개 둘 수도 있다
        외부 라이브러리로 배포할 수 있다 (NuGet 가능)


        [실험 코드 복습 구조 요약]
        구성 요소	                역할
        MyFancyControl.cs	        Control 상속 + DP 등록 + Style 연결
        FancyTextProperty	        사용자 정의 DP
        Generic.xaml	            ControlTemplate 등록 위치
        TemplateBinding FancyText	외부 속성 바인딩용 연결

        [MainWindow.xaml]
             ↓
        <MyFancyControl FancyText="..." />
             ↓
        [MyFancyControl.cs] → DependencyProperty + OverrideMetadata
             ↓
        [Generic.xaml] → Style + TemplateBinding
             ↓
        [ControlTemplate 적용] → 렌더링
    */
    public partial class App : Application
    {
    }
}
