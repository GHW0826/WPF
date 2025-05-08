
## 1.DispatcherTest
- WPF 애플리케이션 전체에 하나의 UI 스레드가 존재.   
- 이 스레드가 모든 UI 요소 (컨트롤, 창, 텍스트, 그림 그리기 등)을 관리하고 렌더링.   
- UI 스레드를 STAThread (Single Thread Apartment) 로 등록해서 시작.   
- WPF는 기본적으로 UI 관련 모든 작업은 반드시 UI 스레드에서만 수행.   
- 다른 스레드에서 UI를 건드리고 싶으면 Dispatcher를 통해 요청해야 함.   

WPF의 스레드 구조
|스레드	                            |역할|
|-----------------------------------|----|
|UI Thread (Main Thread)	          |모든 UI 그리기, 이벤트 처리, 데이터 바인딩|
|ThreadPool / Background Threads	  |데이터 로딩, 네트워크 통신, 파일 IO, 무거운 계산 처리 등|
|Dispatcher	                        |다른 스레드 → UI 스레드로 안전하게 작업 요청하는 중개자   |
   
## 2.RoutedEvent   
이벤트가 UI 트리(Visual/Logical Tree)를 따라 전파되는 구조.   
버블링(Bubbling): 아래 컨트롤(Button)에서 이벤트 발생 → 위쪽 부모 컨트롤(Grid 등)으로 이벤트 전파   
터널링(Tunneling): 최상위 컨트롤(Grid 등)에서 이벤트를 먼저 캡처 → 자식(Button 등)으로 내려감   
Direct :	특정 요소에서만 발생.   
   
```
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
```   
## 3.DependencyProperty   
- WPF는 DependencyObject를 기반으로 "속성 테이블"을 관리.   
→ 이 테이블에 속성 이름, 기본값, 콜백, 메타데이터를 등록하는 게 DependencyProperty.Register().   
- DependencyObject마다 속성 저장소(Internal Storage)가 따로 있다   

```cpp
public static readonly DependencyProperty SampleTextProperty =
DependencyProperty.Register(
    "SampleText",                                   // 속성 이름
    typeof(string),                                 // 속성 타입
    typeof(DependencyPropertyTestView),             // 소유자 타입
    new PropertyMetadata("", OnSampleTextChanged)   // 기본값 + 변경콜백
);
```
- 이걸 하면, DependencyObject(UserControl)이 자신의 내부 테이블에 "SampleText" 속성을 등록 됨.   
→ 아직 값은 없지만, "SampleText"라는 슬롯이 생김.   
   
- DependencyObject는 값을 아래 처럼 설정.   
- SetValue(DependencyProperty, object) → 값을 "속성 테이블"에 세팅   
- GetValue(DependencyProperty) → "속성 테이블"에서 값을 읽기   

- SetValue() 호출 과정에서 값이 변경될 경우 (기존 값 ≠ 새 값)   
- DependencyProperty에 PropertyChangedCallback이 등록되어 있을 경우 그 콜백을 호출.   
- 즉, SetValue() -> 내부 비교 -> 변경됨 -> PropertyChangedCallback 호출 → 값 변경 감지   
   
## 4.BindingEvaluation   
- WPF에서 Binding은 단순히 값 복사(copy)가 아니라 "Binding 엔진"이라는 별도의 시스템이 동작하는 "연결(Connection)".   
- 흐름: 바인딩 객체 생성 → BindingEngine 등록 → 데이터 소스 모니터링 → 값 변화 감지 → 대상 컨트롤 업데이트   
- 단순히 "TextBox.Text = ViewModel.Property" 한 번 복사하고 끝나는 게 아니고,   
- ViewModel의 Property를 지속적으로 감시(Wire)해서, 값이 변하면 동기화.

|BindingMode	|동작 방식|	                    결과|
|--------------|--------|--------------------------|
|OneWay	|ViewModel → View 한 방향 연결	    |TextBox에 입력해도 ViewModel 업데이트 안 됨|
|TwoWay	|ViewModel ↔ View 양방향 연결	    |TextBox 입력하면 ViewModel 값도 변함|
|OneTime	|최초 바인딩 시 값 복사만	        |TextBox 입력해도 ViewModel 값 변하지 않음|
|OneWayToSource	|View → ViewModel (거꾸로)	|주로 드문 케이스 (입력기 중심 컨트롤용)|

[BindingEngine 내부 작동 흐름]
- 바인딩 구문을 만나면 ({Binding}), WPF가 BindingExpression을 생성한다.   
- BindingExpression은 Source 객체(ViewModel)를 등록하고 PropertyChanged 이벤트를 구독.   
- Source 객체가 값을 변경하면, BindingEngine이 자동으로 컨트롤(View)을 업데이트한다.   
- 반대로, UpdateSourceTrigger에 따라 View의 값을 Source로 밀어넣기도 함.   
- Binding은 실제로 Source와 Target 양쪽을 계속 감시하고 동기화하는 역할을 함.   
   
[Binding이 DependencyProperty 기반인 이유]
- DependencyProperty 시스템은 내부에서 "값 변경 감지"를 자동으로 해준다 (SetValue → PropertyChangedCallback).   
- 값이 변하면 BindingEngine이 자동으로 알 수 있다   
- 만약 일반 C# 프로퍼티였다면 INotifyPropertyChanged를 구현해서 BindingEngine에 알리는 코드가 별도로 필요함.   
- WPF 바인딩은 DependencyProperty에 최적화 되어 설계됨.   

[UpdateSourceTrigger]
- UpdateSourceTrigger는 "View → ViewModel으로 값을 언제 보낼지"를 결정하는 설정.   
   
|Trigger 종류|	    설명|
|------------|---------|
|Default|	            컨트롤 타입에 따라 다름 (TextBox는 LostFocus)|
|PropertyChanged|	    입력할 때마다 즉시 ViewModel로 전송|
|LostFocus|	        포커스 빠져나갈 때 ViewModel로 전송|
|Explicit|	        명시적으로 UpdateSource() 호출해야 전송|

   
## 5.VisualLogicalTree
## 6.Command
## 7.MeasureArrange
## 8.ResourceStyle
## 9.DataTemplate_TemplateSelector
## 10.CustomControl
## 11.AttachedProperty
## 12.BehaviorEventToCommand
## 13.StyleSelectorDataTriggerMultiTrigger
## 14.VisualStateManager
## 15.TemplateBinding_Binding
## 16.StaticDynamicResource
## 17.ThemeDictionary
## 18.MarkupExtension
## 19.Messenger_IEventAggregator
## 20.MultiBindingIMultiValueConverter
## 21.RelativeSourceFindAncestor
## 22.ElementNameRelativeSource
## 23.RelativeSource_Mode=Self_TemplatedParent
## 24.TemplateBindingRelativeSource
## 25.Freezables
## 26.NavigationFramePage
