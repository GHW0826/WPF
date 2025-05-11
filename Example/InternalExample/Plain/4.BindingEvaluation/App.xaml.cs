using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BindingEvaluation
{
    /*
        OneWay, TwoWay, OneTime 바인딩 모드 차이 테스트
        UpdateSourceTrigger(PropertyChanged vs LostFocus) 비교 테스트
        바인딩 데이터 흐름과 트리거 시점 직접 눈으로 확인
    */

    /*
        WPF에서 Binding은 단순히 값 복사(copy)가 아니라 "Binding 엔진"이라는 별도의 시스템이 동작하는 "연결(Connection)".
        흐름: 바인딩 객체 생성 → BindingEngine 등록 → 데이터 소스 모니터링 → 값 변화 감지 → 대상 컨트롤 업데이트
        단순히 "TextBox.Text = ViewModel.Property" 한 번 복사하고 끝나는 게 아니고,
        ViewModel의 Property를 지속적으로 감시(Wire)해서, 값이 변하면 동기화.

        BindingMode	동작 방식	                    결과
        OneWay	ViewModel → View 한 방향 연결	    TextBox에 입력해도 ViewModel 업데이트 안 됨
        TwoWay	ViewModel ↔ View 양방향 연결	    TextBox 입력하면 ViewModel 값도 변함
        OneTime	최초 바인딩 시 값 복사만	        TextBox 입력해도 ViewModel 값 변하지 않음
        OneWayToSource	View → ViewModel (거꾸로)	주로 드문 케이스 (입력기 중심 컨트롤용)

        [UpdateSourceTrigger]
        UpdateSourceTrigger는 "View → ViewModel으로 값을 언제 보낼지"를 결정하는 설정.

        Trigger 종류	    설명
        Default	            컨트롤 타입에 따라 다름 (TextBox는 LostFocus)
        PropertyChanged	    입력할 때마다 즉시 ViewModel로 전송
        LostFocus	        포커스 빠져나갈 때 ViewModel로 전송
        Explicit	        명시적으로 UpdateSource() 호출해야 전송


        [BindingEngine 내부 작동 흐름]
        바인딩 구문을 만나면 ({Binding}), WPF가 BindingExpression을 생성한다.
        BindingExpression은 Source 객체(ViewModel)를 등록하고 PropertyChanged 이벤트를 구독.
        Source 객체가 값을 변경하면, BindingEngine이 자동으로 컨트롤(View)을 업데이트한다.
        반대로, UpdateSourceTrigger에 따라 View의 값을 Source로 밀어넣기도 함.
        Binding은 실제로 Source와 Target 양쪽을 계속 감시하고 동기화하는 역할을 함.


        [Binding이 DependencyProperty 기반인 이유]
        DependencyProperty 시스템은 내부에서 "값 변경 감지"를 자동으로 해준다 (SetValue → PropertyChangedCallback).
        값이 변하면 BindingEngine이 자동으로 알 수 있다
        만약 일반 C# 프로퍼티였다면 INotifyPropertyChanged를 구현해서 BindingEngine에 알리는 코드가 별도로 필요함.
        WPF 바인딩은 DependencyProperty에 최적화 되어 설계됨.
    */
    public partial class App : Application
    {
    }
}
