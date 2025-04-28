using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RoutedEvent
{
    /*
        버블링(Bubbling): 아래 컨트롤(Button)에서 이벤트 발생 → 위쪽 부모 컨트롤(Grid 등)으로 이벤트 전파
        터널링(Tunneling): 최상위 컨트롤(Grid 등)에서 이벤트를 먼저 캡처 → 자식(Button 등)으로 내려감
        PreviewMouseDown (터널링) / MouseDown (버블링) 비교
        Handled 설정이 이벤트 전파를 막음.


        RoutedEvent는 WPF에서 이벤트가 "트리(계층 구조)"를 따라 흐를 수 있도록 만든 특수한 이벤트 시스템.
        WPF는 Logical Tree / Visual Tree 구조를 갖고 있으니까, 이벤트도 부모와 자식 사이를 흘러야 자연스러움.
        그래서 "루트를 따라 흐르는(Event routing) 이벤트 시스템" 을 설계.
        ex) UI 구조 (위 아래로 터널링, 버블링)
        Window
         └── Grid
              └── StackPanel
                   └── Button
        핵심은 WPF 내부에서 모든 UI 요소(UIElement, ContentElement)들이 이벤트를 다룰 수 있게 
        AddHandler/RemoveHandler/RaiseEvent 메커니즘을 갖고 있다는 것.
        모든 UIElement는 RoutedEvent를 등록/발생/구독할 수 있는 메서드를 제공.
        이벤트가 발생하면, Dispatcher(이벤트 전파 관리자) 가 "이벤트 라우팅 경로(Routing Path)"를 계산해서 이벤트를 순서대로 호출.

        - 실제 전파 흐름 (구조적)
        1.사용자가 Button을 클릭한다.
        2.WPF의 InputManager가 Raw Mouse Input을 받아서 해석한다.
        3.InputManager가 HitTest를 해서 "어떤 Visual 요소가 클릭됐는지"를 찾는다. (→ Visual Tree를 따라가며 계산함.)
        4.RoutedEvent 인스턴스를 생성하고, 터널링 경로를 먼저 따라간다.
        5.이벤트가 최종 타겟(Button)까지 도착한다.
        6.그 다음 버블링 경로를 역방향으로 올라간다.
        7.중간에 e.Handled = true 를 누가 설정하면, 이벤트가 더 이상 전파되지 않는다.

        WPF는: 입력(Input), 커맨드(Command), 포커스(Focus), 레이아웃(Layout)
        이런 모든 흐름을 "트리 단위로 처리" 

        핵심 컴포넌트	역할
        InputManager	입력 수집 및 초기 이벤트 트리거
        HitTest	        어떤 요소가 입력을 받았는지 결정
        RoutedEvent	    터널링/버블링 구조를 가진 이벤트 객체
        Dispatcher	    이벤트 전파 관리
        UIElement	    이벤트 핸들러 등록 및 이벤트 수신

        [ 사용자 입력 발생 (MouseDown) ]
            ↓
        [ InputManager: Raw Input 수집 ]
            ↓
        [ HitTest: 클릭된 요소(Button) 찾음 ]
            ↓
        [ RoutedEvent 인스턴스 생성 ]
            ↓
        [ 터널링 전파 (Window → Grid → StackPanel → Button) ]
            ↓
        [ 타겟 도착(Button) ]
            ↓
        [ 버블링 전파 (Button → StackPanel → Grid → Window) ]
            ↓
        [ 완료 or Handled로 중단 ]
    */
    public partial class App : Application
    {
    }
}
