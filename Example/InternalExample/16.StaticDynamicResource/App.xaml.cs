using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StaticDynamicResource
{
    /*
        같은 컨트롤에 StaticResource와 DynamicResource 각각 적용
        런타임에서 리소스를 변경했을 때 둘의 반응 차이 확인
        실시간 반영 가능 여부, 메모리 동작 차이 등을 실험
    */

    /*
        [StaticResource]
        항목	    설명
        해석 시점	컴파일 시점 (XAML 파싱 시) 리소스 경로가 확정됨
        캐싱 여부	리소스가 한 번 로드되면 캐시된 참조 사용
        동작 특징	이후 리소스를 바꿔도 UI에는 반영되지 않음
        성능	    빠름 (정적 해석, 경로 추적 없음)
        
        [DynamicResource]
        항목	    설명
        해석 시점	런타임에 리소스가 필요할 때 경로를 추적함
        경로 추적	리소스 키를 계속 추적하여 바뀌면 다시 평가
        동작 특징	런타임에서 리소스가 바뀌면 UI가 즉시 반영됨
        성능	    약간 느림 (해석 시 경로 유지 비용 발생


        [요약]
        항목	                    StaticResource	                        DynamicResource
        리소스 갱신 시 즉시 반영	❌ XAML에 고정됨	                    ✅ 실시간 반영
        리소스 해석 시점	        컴파일 시점	                            런타임
        성능	                    빠름 (1회 고정)	                        느림 (경로 추적)
        캐시/추적	                고정 참조	                            런타임 추적
        실전 용도	                변하지 않는 스타일, 크기, 간격 등	    테마 색상, 동적 스킨, 앱 리소스 교체 등

        [사용 판단 기준]
        상황	                                            추천 방식
        변하지 않는 리소스 (폰트 크기, 텍스트 색 등)	    StaticResource
        런타임에서 바뀔 수 있는 리소스 (테마, 다크모드 등)	DynamicResource
        퍼포먼스가 중요한 대량 바인딩 영역	                StaticResource (불필요한 추적 방지)


        [주의사항]
        DynamicResource는 내부적으로 FrameworkElement.OnStyleChanged/OnPropertyChanged에서 트리거됨
        → 리소스를 변경해도 StaticResource는 이 이벤트를 타지 않음

        ControlTemplate에서 DynamicResource를 남용하면 리소스 트리 lookup 비용이 누적됨
    */
    public partial class App : Application
    {
    }
}
