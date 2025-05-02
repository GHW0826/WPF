using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace VisualStateManager
{
    /*
        Button처럼 보이는 커스텀 컨트롤을 만들고
        VisualStateManager를 통해 상태 전이 (Normal, MouseOver, Pressed) 구현
        상태별로 배경색, 테두리, 텍스트 색 등을 변경
        (Optional) 상태 전이에 애니메이션 적용 실험도 준비

        마우스 올림	    배경 LightBlue
        누름	        배경 LightCoral
        뗌	            다시 LightBlue
        벗어남	        LightGray (기본)
    */
    /*
        VisualStateManager는 컨트롤의 상태(UI 상태) 전이를 선언적으로 정의하고
        마우스/포커스/사용자 동작에 따라 시각적 변화를 적용하는 시스템.

        [요소]
        구성 요소	        설명
        VisualStateGroup	상태들을 하나의 그룹으로 묶음 (CommonStates, FocusStates 등)
        VisualState	        개별 상태 정의 (Normal, MouseOver, Pressed)
        Storyboard	        상태 진입 시 실행할 시각적 애니메이션 (색, 위치, 크기 등 전환)


        [상태 전이 흐름]
        초기 상태 → MouseOver (마우스 진입)
                  ↘ Pressed (마우스 클릭)
                  ↘ MouseOver (버튼 뗌)
                  ↘ Normal (마우스 나감)


        [작동 원리]
        1. VisualStateManager.GoToState(this, "Pressed", true) 호출됨
        2. 현재 컨트롤의 VisualStateGroup에서 "Pressed" 상태를 찾음
        3. 해당 상태의 Storyboard가 있으면 실행됨
        4. 이전 상태의 시각 효과는 자동으로 취소됨 (중복 전이 방지)


        [예시 해석]
        <VisualState x:Name="MouseOver">
            <Storyboard>
                <ColorAnimation Storyboard.TargetName="bd" 
                                Storyboard.TargetProperty="(Border.Background).(SolidColorBrush.Color)"
                                To="LightBlue" Duration="0:0:0.2" />
            </Storyboard>
        </VisualState>

        상태 이름: "MouseOver"
        대상: "bd"라는 이름의 Border
        동작: Background의 Color를 LightBlue로 0.2초간 애니메이션

        
        [VisualStateManager 내부 흐름]
        그룹 이름	        상태 예시
        CommonStates	    Normal, MouseOver, Pressed, Disabled
        FocusStates	        Focused, Unfocused
        ValidationStates	Valid, Invalid
        SelectionStates	    Selected, Unselected

        
        [사용 위치]
        위치	            설명
        ControlTemplate	    기본 컨트롤 (Button, CheckBox 등)의 동작 정의
        UserControl	        우리가 만든 StatefulButton처럼 직접 정의 가능
        Style 내부	        ControlTemplate 안에서 시각 상태 정의 가능

        
        [정리]
        항목	        내용
        핵심 개념	    VisualStateGroup 안에 여러 상태를 정의, 상태 전환 시 Storyboard 실행
        주요 메서드	    VisualStateManager.GoToState(control, "StateName", true)
        적용 위치	    UserControl, ControlTemplate 내부
        실전 활용	    상태 기반 애니메이션, 사용자 상호작용 반응, 테마 전이 등
    */
    public partial class App : Application
    {
    }
}
