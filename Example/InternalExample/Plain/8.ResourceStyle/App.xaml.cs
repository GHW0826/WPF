using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ResourceStyle
{
    /*
        StaticResource 버튼은 App.xaml 리소스를 컴파일 시점에 복사
        DynamicResource 버튼은 런타임에 리소스를 참조
        StyledButton은 여러 속성이 Style로 일괄 적용

        StaticResource로 브러시 사용	컴파일 시점 리소스 고정
        DynamicResource로 브러시 사용	런타임에 리소스 참조
        Style로 여러 Setter 적용	Button 스타일 일괄 적용
    */
    /*
        [WPF에서 Resource]
        WPF Resource는 XAML 내부나 외부에서 재사용 가능한 객체나 값을 저장하는 시스템.
            SolidColorBrush 같은 단순 객체
            Style, ControlTemplate 같은 복합 스타일
            even DataTemplate, Storyboard까지 포함 가능
        리소스는 키(key)로 등록하고, 필요할 때 키로 참조.

        
        [StaticResource vs DynamicResource 차이]
        항목          StaticResource              DynamicResource
        참조 시점     컴파일 타임 or 로딩 타임    런타임
        변경 반영     X                           O (런타임 리소스 변경 반영)
        퍼포먼스      빠름                        느림
        사용 예시     대부분의 고정 리소스        테마 변경, 다국어 변경 등

        [StaticResource 흐름]
        XAML 파싱 시점에 키를 찾아서 해당 리소스 값을 "복사"해서 설정
        → 나중에 리소스가 바뀌어도 적용되지 않는다.

        [DynamicResource 흐름]
        컨트롤이 만들어질 때, 키만 저장 필요할 때마다 리소스를 찾아서 읽는다
        → 나중에 리소스를 바꾸면 적용

        
        [Resource Lookup 순서]
        WPF는 리소스를 찾을 때 트리 구조를 따라 올라간다. 항상 가까운 곳 먼저 찾고, 없으면 부모로 올라감
        리소스 찾기 순서:
            1. 자신(UserControl, Button 등)의 Resources
            2. 부모 요소(StackPanel, Grid 등)의 Resources
            3. Window.Resources
            4. App.xaml Resources
            5. Theme 레벨 (SystemTheme)
        
        [Style]
        WPF Style은 컨트롤의 속성(Setter) 집합을 묶어놓은 리소스
        
        특징	                        설명
        여러 Setter 묶기	            Background, FontSize, Margin 등 한번에 지정
        재사용 가능	                    여러 컨트롤에 Style 적용 가능
        TargetType 지정	                Button, TextBox 등 컨트롤 타입 지정 가능
        트리거, 데이터바인딩 확장 가능	Trigger, DataTrigger, MultiTrigger 지원

        
        [Style Setter 동작 구조]
        Setter 흐름	    설명
        Style 적용	    컨트롤이 생성될 때 Style이 적용된다
        Setter 순회	    각 Setter마다 속성을 설정한다
        값 우선순위  	직접 지정한 속성 > Style Setter 값


        [요약 흐름]
        [ Resource 등록 (XAML, Code) ]
            ↓
        [ Resource 조회 (트리 구조를 따라 올라감) ]
            ↓
        [ StaticResource → 고정 값 / DynamicResource → 실시간 반영 ]
            ↓
        [ Style → 여러 Setter를 묶어 속성 일괄 적용 ]

        

        [Resource/Style 시스템]
        포인트	            요약
        StaticResource	    컴파일 시점 값 복사 (고정)
        DynamicResource	    런타임 시점 참조 (변경 반영)
        리소스 찾기 순서	자신 → 부모 → Window → App
        Style	            여러 Setter를 묶어서 컨트롤에 일괄 적용
    */
    public partial class App : Application
    {
    }
}
