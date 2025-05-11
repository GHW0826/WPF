using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DependencyProperty
{
    /*
        DependencyProperty를 직접 등록해서 값 변경 감지 실험
        PropertyChangedCallback을 이용해서 변경 감지
        일반 프로퍼티와 DependencyProperty의 차이를 직접 체험

        진짜 목표는 ViewModel 대신 UserControl에 진짜 DependencyProperty를 직접 등록.
        그리고 PropertyChangedCallback을 등록해서, 값이 변경될 때 자동으로 이벤트 핸들링함.
    */
    /*
        DependencyProperty.Register(...)	WPF 속성 시스템의 핵심 등록 방식
        PropertyChangedCallback	            값 변경 감지 가능  
        ElementName=root	                XAML에서 UserControl 자신을 바인딩 대상으로 설정
        TextBox.Text ↔ DP.SampleText	    양방향 바인딩 실험
    */
    /*
        WPF는 DependencyObject를 기반으로 "속성 테이블"을 관리.
        → 이 테이블에 속성 이름, 기본값, 콜백, 메타데이터를 등록하는 게 DependencyProperty.Register().
        (DependencyObject마다 속성 저장소(Internal Storage)가 따로 있다)
    
        public static readonly DependencyProperty SampleTextProperty =
        DependencyProperty.Register(
            "SampleText",                  // 속성 이름
            typeof(string),                 // 속성 타입
            typeof(DependencyPropertyTestView),  // 소유자 타입
            new PropertyMetadata("", OnSampleTextChanged) // 기본값 + 변경콜백
        );
        이걸 하면, DependencyObject(UserControl)이 자신의 내부 테이블에 "SampleText" 속성을 등록 됨.
        → 아직 값은 없지만, "SampleText"라는 슬롯이 생김.
        
        DependencyObject는 값을 아래 처럼 설정.
        SetValue(DependencyProperty, object) → 값을 "속성 테이블"에 세팅
        GetValue(DependencyProperty) → "속성 테이블"에서 값을 읽기

        SetValue() 호출 과정에서 값이 변경될 경우 (기존 값 ≠ 새 값)
        이 DependencyProperty에 PropertyChangedCallback이 등록되어 있을 경우
        (InvalidateMeasure / InvalidateArrange / InvalidateVisual 호출할 수도 있음 (속성 종류에 따라))
        그 콜백을 호출.
        즉, SetValue() -> 내부 비교 -> 변경됨 -> PropertyChangedCallback 호출 → 값 변경 감지
    */
    /*
        DependencyProperty 변경 감지의 기술적 디테일

        특징	            설명
        메모리 최적화	    DP는 값이 기본값이면 테이블에 저장 자체를 안 한다 (스파스(Sparse) 구조)
        변경 감지	        값이 기존과 달라질 때만 콜백 호출 (변경 없으면 호출 안 함)
        Value Inheritance	부모 요소에서 값 상속 받을 수도 있음 (메타데이터 Inherits 설정 시)
        CoerceValue 가능	값이 설정되기 전에 Coerce 콜백으로 값 조정 가능
        유효성 검사 가능	ValidateValueCallback으로 유효성 검사 가능

        [일반 프로퍼티 vs DependencyProperty]

        항목	                일반 프로퍼티	                        DependencyProperty
        저장 방식	            필드(Heap에 직접)	                    내부 테이블(DependencyObject)
        변경 감지	            직접 INotifyPropertyChanged 구현 필요	PropertyChangedCallback 자동 지원
        기본값 처리	            수동 구현	                            PropertyMetadata로 기본값 제공
        바인딩 지원	            직접 구현 필요	                        WPF 바인딩 엔진 자동 지원
        스타일/애니메이션 적용	불가	                                가능 (Style, Trigger, Animation 적용)

        [PropertyMetadata]
        구성요소	                                    설명
        DefaultValue	                                DependencyProperty의 기본값
        PropertyChangedCallback	                        값이 변경됐을 때 호출되는 콜백
        CoerceValueCallback (옵션)	                    값이 강제 조정될 때 호출
        Inherits (FrameworkPropertyMetadata 사용 시)	값 상속 여부
    */
    public partial class App : Application
    {
    }
}
