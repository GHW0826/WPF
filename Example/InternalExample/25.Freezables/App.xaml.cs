using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Freezables
{
    /*
        [코드 구성]
        MainWindow.xaml: Rectangle 컨트롤을 사용하여 Freezable Brush를 적용.
        MainWindow.xaml.cs:
            SolidColorBrush (LightBlue) 생성.
            Freeze() 메서드를 통해 Freezable 적용 (불변 상태).
            버튼 클릭 시 Brush 색상 변경을 시도.
    
    
        [Freezable 구조 동작 원리]
        WPF에서 성능 최적화된 공유 가능한 객체를 정의하는 클래스.
        Freezable 객체는 Immutable (불변) 상태로 전환할 수 있어 성능과 메모리 사용을 최적화.
        대표적인 Freezable 객체:
            SolidColorBrush
            Transform (ScaleTransform, RotateTransform 등)
            Geometry (PathGeometry, EllipseGeometry 등)


        [Freezable 객체의 특징]
        특성	                    설명
        불변성 (Immutable)	        Freeze() 메서드를 호출하면 더 이상 수정할 수 없음.
        성능 최적화	                불변 상태로 전환되면 WPF 내부에서 공유 가능.
        상태 변경 가능 여부	        IsFrozen 속성을 통해 확인 가능.
        자식 Freezable 동기화	    Freezable 객체의 자식도 Freeze 상태로 전환.


        [실험 결과 (코드 실행 시)]
        Rectangle의 Brush는 LightBlue로 시작.
        버튼 클릭 시:
            Frozen 상태 (Freezable 적용)일 경우 "Brush is Frozen (Immutable)" 메시지.
            Frozen 상태가 아니면 색상 변경 (LightGreen ↔ LightBlue).
        성능 최적화 확인: Freeze 상태에서는 WPF 렌더링 엔진이 Brush를 공유 가능하게 처리하여 성능 향상.
    */
    public partial class App : Application
    {
    }
}
