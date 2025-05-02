using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ElementNameRelativeSource
{
    /*
        같은 바인딩 목적(부모 컨트롤의 속성 접근)을
        ElementName 바인딩과 RelativeSource 바인딩으로 각각 구현
        위치 기반(트리 탐색)과 이름 기반(명시적 참조)의 차이점을 확인

        [비교 포인트]
        ElementName은 명확한 이름 지정 → 무조건 바인딩 성공
        RelativeSource는 위치 기반 탐색 → 현재 위치 기준으로 상위 트리 내에 있어야만 동작

        이 예제에서는 TextBlock → StackPanel → GroupBox → StackPanel이라
        TextBlock 기준으로 TextBox는 트리 외부에 있음
        → FindAncestor 실패 (null 바인딩)
    */

    /*
        [ElementName vs RelativeSource]
        바인딩                     방식	설명
        ElementName=xxx	           이름으로 명확히 지정한 요소를 바인딩 대상으로 사용
        RelativeSource Mode=...	   현재 요소 기준으로 상위 또는 자신을 기준으로 탐색하여 바인딩 대상 결정

        [결과 요약]
        TextBlock 위치	바인딩 방식	                결과
        GroupBox 안쪽	ElementName=txtSource	    정상 작동 (이름으로 명확 지정)
        GroupBox 안쪽	FindAncestor of TextBox	    실패 (상위 트리에 TextBox 없음)

        [동작 흐름 비교]
        - ElementName
        
        1. XAML 파싱 시 이름 등록됨 (txtSource)
        2. 바인딩 엔진이 TextBlock에서 txtSource 이름을 찾음
        3. 해당 요소의 Text 속성과 연결

        - RelativeSource Mode=FindAncestor
        1. 현재 요소 기준으로 시각적 트리 상위 탐색 시작
        2. `AncestorType=TextBox`에 해당하는 조상이 있는지 확인
        3. 없으면 바인딩 실패 (null)


        [차이 요약]
        항목	            ElementName	                    RelativeSource
        대상 지정 방식	    명시적 이름 기반	            트리 탐색 기반
        작동 범위	        어디든 이름만 알면 가능	        트리 상위 구조 내에서만 가능
        트리 독립성	        높음	                        트리 종속적
        해석 시점	        로딩 시	                        로딩 시 (탐색 필요)
        주요 용도	        외부에 있는 컨트롤 속성 접근	템플릿/트리 안에서 부모 접근

        [사용 팁]
        상황	                                                    추천 방식
        명확한 컨트롤 참조	                                        ElementName
        템플릿/데이터템플릿 내부 → 상위 ViewModel, Control 참조	RelativeSource Mode=FindAncestor
        내부 요소 간 참조 (Self, TemplatedParent, PreviousData)	    RelativeSource
        템플릿 외부에 있는 이름 있는 컨트롤 접근	                ElementName (단, 이름 해석 순서 유의)

        [주의사항]
        항목	                                        설명
        ElementName 해석 순서	                        참조 대상이 아직 생성되지 않았으면 바인딩 실패
        RelativeSource 탐색 실패	                    상위 트리에 원하는 타입이 없으면 null
        둘 다 OneTime Binding이면 초기값만 반영됨	    바인딩 모드 주의


        [결론 요약]
        ElementName은 이름으로 직접 지정해서 위치와 관계없이 안정적으로 바인딩할 수 있고,
        RelativeSource는 템플릿 내부처럼 이름이 없는 상황에서 상위 트리 구조를 따라 바인딩할 수 있게 해준다.
        실전에서는 둘을 목적에 따라 구분해서 사용해야 하고,
        XAML 구조가 복잡할수록 RelativeSource를 사용하는 빈도가 증가한다.
    */
    public partial class App : Application
    {
    }
}
