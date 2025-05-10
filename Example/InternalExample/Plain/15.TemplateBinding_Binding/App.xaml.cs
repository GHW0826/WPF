using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TemplateBinding_Binding
{
    /*
        ControlTemplate 내부에서
            Binding 사용 시: DataContext 기준 경로
            TemplateBinding 사용 시: TemplatedParent 기준 속성 연결
        동일한 컨트롤에 두 방식 다 적용해 결과 차이 비교
    */
    /*
        [결과 비교]
        항목	            바인딩 방식	        동작 기준	                        결과
        Label 텍스트	    TemplateBinding	    TemplatedParent 속성	            "템플릿 바인딩 테스트"
        Label 색상	        TemplateBinding	    TemplatedParent.LabelForeground	    Red
        Content 텍스트	    일반 Binding	    root.DataContext.Content	        "Hello TemplateBinding!"

        [요약]
        항목	        TemplateBinding	                Binding
        바인딩 기준	    TemplatedParent	                DataContext or ElementName
        적용 위치	    ControlTemplate 전용	        어디서나 가능
        주요 용도	    부모 컨트롤의 속성 전달	        데이터 모델 또는 외부 참조 연결
        장점	        빠르고 경량, XAML 처리 최적화	유연하고 경로 조절 가능
        단점	        컨트롤 외부 데이터 접근 불가	속도 약간 느림 (동적 평가)
    */

    /*

        [TemplateBinding]
        항목	            내용
        정의	            ControlTemplate 내부에서 TemplatedParent(컨트롤 자신) 의 속성을 연결하는 특수 바인딩
        기준 경로	        항상 TemplatedParent.<속성> (정적 연결)
        주 사용 위치	    ControlTemplate, Style.Setters, Generic.xaml
        평가 시점	        XAML 컴파일 시점에 경로가 고정됨
        성능	            매우 빠름 (정적 평가, 최적화됨)
        제한사항	        DataContext 접근 불가, 기능 유연성 떨어짐
        주 용도	            재사용 가능한 CustomControl의 스타일 작성 시 속성 연결


        [Binding]
        항목	            내용
        정의	            가장 일반적인 바인딩 방식으로, DataContext 또는 RelativeSource 기준으로 동작
        기준 경로	        DataContext or ElementName, RelativeSource, Self, TemplatedParent 등
        주 사용 위치	    XAML 어디서나 가능 (View, Template, DataTemplate 등)
        평가 시점	        런타임에 동적으로 경로 탐색
        성능	            약간 느림 (런타임 바인딩 경로 탐색 필요)
        유연성	            매우 높음 (모델, 뷰 요소, 상위 컨트롤 등 접근 가능)
        주 용도	            일반적인 MVVM 데이터 연결, 동적 구조 바인딩


        [요약]
        항목	        TemplateBinding	                Binding
        적용 기준	    컨트롤 자신 (TemplatedParent)	외부 구조 or DataContext
        결과 안정성	    항상 고정 경로	                구조 변경 시 깨질 수 있음
        런타임 영향	    거의 없음	                    큰 트리 구조 + 다량일 때 부하 가능
        실험 예	        Label, LabelForeground	        Content via RelativeSource

        
        [선택 기준 요약]
        사용 상황	                                                추천 방식
        ControlTemplate 내부에서 컨트롤 자신의 속성만 쓰는 경우	    TemplateBinding (간결, 빠름)
        외부 데이터와 연결하거나 ViewModel 참조할 경우	            Binding (유연, 범용)
        RelativeSource 접근이 필요한 복합 경로	                    Binding + RelativeSource=TemplatedParent


        [결론 요약]
        TemplateBinding은 속도 빠르고 간단하지만 유연성 없음,
        Binding은 강력하고 유연하지만 성능 약간 떨어짐
            → ControlTemplate 안에서는 TemplateBinding이 기본,
        복잡한 바인딩이 필요하면 Binding + RelativeSource 조합 사용


    */
    public partial class App : Application
    {
    }
}
