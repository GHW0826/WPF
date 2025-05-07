using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TemplateBindingRelativeSource
{
    /*
        [TemplateBinding vs RelativeSource]

        1. TemplateBinding (템플릿 바인딩)
        동작 방식: ControlTemplate 내부에서 TemplateBinding을 통해 상위 컨트롤의 속성을 직접 바인딩.
        바인딩 구조: 내부적으로 상위 컨트롤의 Dependency Property에 직접 연결.
        속도: 가장 빠른 바인딩 방식 중 하나 (PropertyChangedCallback 없이 직접 연결).
        주요 특징:
            ControlTemplate 내부에서만 사용할 수 있음.
            ControlTemplate의 TargetType에서 정의된 속성에만 적용 가능.
            변경 알림 없이 속성이 즉시 반영.

        2. RelativeSource (TemplatedParent)
        동작 방식: Binding 사용, RelativeSource Mode를 TemplatedParent로 지정하여 상위 컨트롤에 바인딩.
        바인딩 구조: Binding 시스템을 통해 DependencyProperty 값 접근.
        속도: Binding 시스템을 사용하므로 TemplateBinding보다 약간 느림.
        주요 특징:
            ControlTemplate 외부에서도 사용 가능.
            DependencyProperty 변경 알림을 통해 자동으로 UI에 반영.

        [차이점 요약]
        특성	        TemplateBinding	                            RelativeSource (TemplatedParent)
        성능	        매우 빠름 (직접 바인딩)	                    Binding 시스템 사용 (약간 느림)
        사용 위치	    ControlTemplate 내부에서만 사용 가능	    ControlTemplate 외부에서도 사용 가능
        바인딩 대상	    ControlTemplate의 TargetType 속성	        DependencyProperty가 있는 모든 컨트롤
        변경 알림	    없음 (즉시 반영)	                        DependencyProperty의 변경 자동 반영
        동적 변경	    직접 설정된 값만 반영	                    Binding을 통해 자동 반영

        [실험 결과 (코드 실행 시)]
        TemplateBinding 버튼 (LightBlue → LightGreen): 
        클릭 시 Background 속성이 바로 변경되지만, ControlTemplate의 Background 속성은 변하지 않음.

        RelativeSource 버튼 (LightCoral → LightYellow): 
        클릭 시 Background 속성이 변경되며, ControlTemplate 내부에서도 변경이 반영됨.
    */
    public partial class App : Application
    {
    }
}
