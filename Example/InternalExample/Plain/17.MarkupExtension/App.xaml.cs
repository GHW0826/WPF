using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MarkupExtension
{
    /*
        {} 형식으로 쓰이는 XAML 확장 구문을 직접 구현해보기
        MarkupExtension을 상속받아 ProvideValue() 메서드 구현
        ViewModel 속성 또는 Enum 값을 변환하거나 라벨을 가져오는 간단한 바인딩 확장 시나리오를 실험

        포인트	                                    설명
        {} 문법 내부는 모두 MarkupExtension 객체	{Binding}, {StaticResource} 등도 전부 MarkupExtension
        ProvideValue() 호출 시점	                XAML 파싱 → 컨트롤 초기화 중 → 리소스 평가 시점
        IServiceProvider 활용 가능	                바인딩 타겟 정보, 서비스 컨텍스트 접근 가능 (다음 실험 확장)
    */


    /*
        [MarkupExtension]
        {Binding}, {StaticResource}, {x:Type} 처럼
        XAML에서 {} 문법으로 쓰이는 모든 것은 내부적으로
        MarkupExtension을 상속한 클래스.


        [XAML 처리 흐름]
        1. XAML 파서가 `{ext:Something}`을 만나면
        2. MarkupExtension 객체를 생성
        3. .ProvideValue(IServiceProvider) 호출
        4. 반환값이 해당 속성에 할당됨

        <TextBlock Text="{ext:EnumDescription Value={x:Static ext:MyEnum.First}}"/>
        → XAML 파서는 EnumDescriptionExtension 객체를 만들고
        → ProvideValue()를 호출해 반환된 문자열로 TextBlock.Text를 설정
        
        [핵심 메서드]
        public override object ProvideValue(IServiceProvider serviceProvider)
        이 메서드는 실제로 해당 XAML 속성에 들어갈 값을 리턴함
        serviceProvider를 통해 바인딩 대상, 위치 등의 메타정보도 접근 가능 (고급 활용)

        
        [IServiceProvider에서 얻을 수 있는 정보]
        형식	                설명
        IProvideValueTarget	    대상 객체와 속성 (어떤 컨트롤의 어떤 속성에 적용 중인지)
        IRootObjectProvider	    최상위 루트 요소
        IXamlTypeResolver	    문자열 타입명을 .NET Type으로 변환 가능

        
        [장점]
        이유	                    설명
        XAML 구조를 확장 가능	    {MyBinding}, {Translate Key=...} 등 커스텀 DSL 구현
        Binding보다 간결함	        Binding 없이도 값 매핑 처리 가능
        리소스 재사용성 증가	    Enum → Description, Localize, Bool → Visibility 등
        메타정보 활용 가능	        위치, 타겟 속성, 객체 참조까지 다 접근 가능
        
        [예]
        예제	                                                설명
        {loc:Text Key=Main.Title}	                            다국어 텍스트 변환
        {enum:Description Value={x:Static MyEnum.Value}}	    enum → 설명 표시
        {vm:MethodInvoke Target=Foo, Method=Refresh}	        ViewModel 메서드 호출 바인딩처럼 쓰기
        {proxy:ViewModel}	                                    정적 리소스처럼 ViewModel 주입

        
        [요약]
        항목	            내용
        핵심 개념	        {} 구문은 전부 MarkupExtension
        주요 메서드	        ProvideValue() 에서 반환된 값이 XAML 속성에 설정됨
        적용 위치	        거의 모든 XAML 속성, 바인딩, 트리거, 리소스 등
        활용 사례	        Enum 설명, 다국어 처리, ViewModel 프록시, 조건 표현 등
        장점	            선언적 DSL 확장 + 컴팩트한 표현 + 재사용 가능

    */
    public partial class App : Application
    {
    }
}
