using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MeasureArrange
{

    /*
        WPF 레이아웃 엔진의 MeasureOverride, ArrangeOverride 흐름 체험
        커스텀 Panel을 만들어 자식 요소들의 크기/배치 과정을 직접 제어해보기
        Measure/Arrange 호출 순서를 눈으로 확인
    */
    /*
        WPF는 두 단계 레이아웃 엔진 (Two-Pass Layout Engine) 으로 화면을 배치한다.

        두 단계
        Measure Pass: 내가 필요한 공간은 이만큼 (요청)
        Arrange Pass: 공간 할당 (배치)
        → 모든 UIElement는 이 두 단계를 반드시 거쳐야 화면에 표시된다.


        [Measure Pass (측정 단계)]
        MeasureOverride 호출 → 자식 컨트롤들에게 "필요한 크기"를 요청

        흐름
        1. 부모 컨트롤이 MeasureOverride 호출됨
        2. 각 자식 컨트롤에 child.Measure(availableSize) 호출
        3. 각 자식은 자신이 필요한 크기를 계산해서 DesiredSize에 기록
        4. 부모는 자식들의 DesiredSize를 보고 자신의 최종 요청 크기를 결정


        [Arrange Pass (배치 단계)]
        ArrangeOverride 호출 → 자식 컨트롤들에게 "실제 배치할 위치/크기"를 지정

        흐름
        1. 부모 컨트롤이 ArrangeOverride 호출됨
        2. 각 자식 컨트롤에 child.Arrange(Rect) 호출
        3. Rect = 어디(위치), 얼마나(크기) 배치할지 지정
        4. 자식 컨트롤은 그 Rect를 기반으로 스스로 크기와 위치를 조정


        [WPF 레이아웃 시스템의 특징]
        특징	                                                설명
        레이아웃 트리 전체를 Measure → Arrange 두 번 돈다	    최상위 Window부터 쭉 내려간다
        Measure 단계에서 공간 협상(Negotiation)이 이루어진다	부모-자식 간에 공간 조율
        Arrange 단계에서 최종 위치와 크기가 결정된다	        픽셀 정확하게 배치


        [MeasureOverride vs ArrangeOverride 비교]
        항목	            MeasureOverride	        ArrangeOverride
        목적	            필요한 공간 계산	    실제 위치/크기 배치
        메서드 호출 시기	레이아웃 측정 단계	    레이아웃 배치 단계
        중요한 메서드	    child.Measure()	        child.Arrange(Rect)
        반환값	            Size (요청하는 크기)	Size (최종 배치 크기)    
    */
    public partial class App : Application
    {
    }
}
