
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
## 4.BindingEvaluation
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
