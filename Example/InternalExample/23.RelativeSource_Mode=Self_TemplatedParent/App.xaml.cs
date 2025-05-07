using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RelativeSource_Mode_Self_TemplatedParent
{
    /*
    
        같은 바인딩 목적(컨트롤 속성 접근)을
        Mode=Self, Mode=TemplatedParent 두 방식으로 각각 구현
        자기 자신(Self) vs 템플릿 대상(TemplatedParent) 바인딩 차이 이해
        ControlTemplate 내부에서의 바인딩 동작 확인.

        [비교]
        바인딩 방식	                            설명	                        동작
        RelativeSource Mode=Self	            컨트롤 자기 자신	            StyledButton의 SelfColor 직접 참조
        RelativeSource Mode=TemplatedParent	    컨트롤이 포함된 템플릿 대상	    템플릿으로 감싸진 컨트롤의 부모 속성 (TemplateColor)

        
        [동작 원리]
        1. Mode=Self
        컨트롤 자체에서 자신의 속성에 접근
        UI 구조와 무관하게 항상 자기 자신 기준
        예제에서는 StyledButton.SelfColor 직접 사용
        <Setter Property="Background" Value="{Binding SelfColor, RelativeSource={RelativeSource Mode=Self}}"/>
        
        2. Mode=TemplatedParent
        ControlTemplate 또는 ContentPresenter로 감싸진 컨트롤의 속성
        ControlTemplate 내부에서 사용됨
        예제에서는 상위 UserControl의 TemplateColor 사용
        <Setter Property="Foreground" Value="{Binding TemplateColor, RelativeSource={RelativeSource Mode=TemplatedParent}}"/>

    */

    /*
        [핵심 개념]
        바인딩 모드	            설명
        Mode=Self	            컨트롤 자기 자신의 속성을 바인딩
        Mode=TemplatedParent	컨트롤이 적용된 템플릿의 대상 (호스트 컨트롤) 에 바인딩


        Button	                    배경색 (Self)	        글씨색 (TemplatedParent)
        Self Button	                LightGreen (정상)	    기본 색상 (적용 X)
        TemplatedParent Button	    기본 색상 (적용 X)	    LightSalmon (정상)

        [동작 차이]
        Self Button은 자기 자신의 속성을 직접 참조 (SelfColor).
        TemplatedParent Button은 ControlTemplate 대상 속성을 참조 (TemplateColor).


        <Setter Property="Background" Value="{Binding SelfColor, RelativeSource={RelativeSource Mode=Self}}"/>

        핵심 동작	    설명
        바인딩 대상	    자기 자신 (현재 컨트롤)
        적용 위치	    스타일, 트리거, ControlTemplate 내부
        주요 사용	    CustomControl 내부에서 자신의 속성 바인딩
        주요 특징	    ViewModel이나 외부 요소에 의존하지 않음

        
        <Setter Property="Foreground" Value="{Binding TemplateColor, RelativeSource={RelativeSource Mode=TemplatedParent}}"/>
        핵심 동작	    설명
        바인딩 대상	    ControlTemplate이 적용된 부모 컨트롤
        적용 위치	    ControlTemplate 내부 (예: CustomControl)
        주요 사용	    CustomControl에서 외부 스타일 적용
        주요 특징	    Template의 외부 속성 참조 가능 (재사용성 높음) 
    */
    public partial class App : Application
    {
    }
}
