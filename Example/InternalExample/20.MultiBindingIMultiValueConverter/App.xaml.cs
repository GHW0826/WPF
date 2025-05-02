using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace MultiBindingIMultiValueConverter
{
    /*
        2개의 ViewModel 속성 (IsAdmin, IsEnabled) MultiBinding을 이용해 두 값이 모두 true일 때만 버튼 활성화
        IMultiValueConverter를 사용해 조건 조합 로직 작성
    */
    /*
        여러 ViewModel 속성의 값을 조합하여 하나의 UI 속성을 제어하고 싶을 때 사용하는 WPF 바인딩 구조.

        [구성 요소]
        구성요소	            설명
        MultiBinding	        <MultiBinding> 태그로 여러 개의 Binding을 그룹으로 묶음
        IMultiValueConverter	Convert(object[] values, ...)에서 모든 바인딩 값을 받아 하나의 출력값 생성

        [요약]
        IsAdmin, IsEnabled 두 속성을 체크박스로 제어
        버튼의 IsEnabled 속성은 MultiBinding으로 이 두 값을 결합
        AndConverter는 둘 다 true일 때만 버튼 활성화
        버튼이 눌리면 ActionLog 텍스트가 바뀌며 리액션 발생


        [동작 흐름]
        1. 사용자가 체크박스를 토글 → IsAdmin / IsEnabled 속성 변경
        2. MultiBinding이 둘 값을 IMultiValueConverter에 전달
        3. Convert(values[]) → true or false 반환
        4. Button.IsEnabled 속성에 적용됨
        5. 버튼이 눌리면 Command 실행 → 로그 출력됨

        [코드 구조 핵심]
        <Button.IsEnabled>
            <MultiBinding Converter="{StaticResource AndConverter}">
                <Binding Path="IsAdmin"/>
                <Binding Path="IsEnabled"/>
            </MultiBinding>
        </Button.IsEnabled>
        
        [when?]
        상황	                            MultiBinding 사용 적합 여부
        여러 조건 조합이 필요한 UI	        매우 적합
        A && B → 색 바꾸기 / 버튼 활성화	적합
        단일 값으로 충분한 경우	            일반 Binding 사용이 더 간단
        값 변경 시점이 서로 다를 때	        값 순서 유의 필요

        [장점]
        항목	            설명
        선언적	            XAML에서 바인딩만으로 복합 조건 처리 가능
        MVVM 친화적	        ViewModel 수정 없이 로직을 분리 가능
        실전 확장성	        색상, Visibility, ToolTip 등 거의 모든 속성에 활용 가능

        [주의사항]
        항목	            설명
        순서 의존	        values[]는 바인딩 순서 그대로 전달됨
        ConvertBack 복잡	대부분 OneWay 바인딩으로 사용
        성능	            과도한 MultiBinding은 렌더링 타이밍에 영향 줄 수 있음 (특히 트리거 내부 등)

        [요약]
        MultiBinding은 복수 속성 → 단일 속성 흐름을 선언적으로 제어할 수 있는 WPF 기능이다.
        IMultiValueConverter를 통해 MVVM 구조를 깔끔하게 유지하면서 UI 로직을 분리할 수 있다.
        실전에서는 버튼 제어, 색상, 텍스트 결정, 상태 결합 등 거의 모든 UI 조건 분기에 활용된다.
    */
    public partial class App : Application
    {
    }
}
