
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
- WPF는 트리 기반 아키텍처로 구성.   
- 트리는 용도에 따라 "Logical Tree"와 "Visual Tree"로 나뉨.   

|종류	        |용도|	                    주요 대상|
|-------------|----|------------------------------|
|Logical Tree	|데이터/구조 관계 표현|    ItemsControl, ContentControl, UserControl|
|Visual Tree	 |   렌더링/UI 요소 표현|	        Border, ScrollViewer, TextBox, Button 렌더링 구조|
   
[Logical Tree]   
- Logical Tree는 사용자 UI 구조를 논리적으로 구성하는 트리.   
- XAML 파일에서 선언한 "부모-자식" 관계를 나타냄.   

- LogicalTree 특징   
- 컨트롤과 데이터 관계만 반영한다.   
- 레이아웃 최적화나 스타일 적용 시 사용된다.   
- Binding의 DataContext 전파는 LogicalTree를 따라간다.   
   

[Visual Tree]   
- Visual Tree는 WPF가 화면에 실제로 그릴 요소들을 관리하는 트리.   
- ControlTemplate, Border, ContentPresenter 같은 시각적 요소까지 포함한다.   

- VisualTree 특징   
- 실질적으로 렌더링되는 모든 요소가 들어간다.   
- StackPanel 안에 Button이 있으면, Button 안에도 Border, ContentPresenter, TextBlock 등이 또 들어있다.   
- 이벤트 전파 (터널링/버블링)는 VisualTree를 따름.   

|특징|	        Logical Tree|	        Visual Tree|
|----|-----------------------|----------------------|
|주 목적|	        데이터 및 구조 관리|	    실제 화면 렌더링 관리|
|구성 요소|	    컨트롤, 데이터 객체|	    컨트롤 + 템플릿 내부 구조|
|이벤트 전파|	    일부 DataContext 전파|	대부분 이벤트 터널링/버블링|
|탐색 방법|	    LogicalTreeHelper|	    VisualTreeHelper|

[WPF 시스템에서 언제 어떤 트리를 쓰나]
|용도|	                        사용하는 트리|
|----|----------------------------------------|
|DataContext 전파|	            Logical Tree|
|스타일 적용 (StaticResource 등)|	Logical Tree|
|이벤트 전파 (터널링/버블링)|	    Visual Tree|
|Template 확장|	                Visual Tree|
|레이아웃/측정|	                Visual Tree|
   
```
[ XAML 작성 ]
    ↓
[ Logical Tree 구성 ]
    ↓
[ ControlTemplate 적용 ]
    ↓
[ Visual Tree 구성 (렌더링 구조) ]
```
   
## 6.Command   
[Command]
- WPF에서 Command는 사용자의 입력(버튼 클릭, 키보드 입력 등)을 논리적 작업(액션)으로 연결하는 패턴.   
- 입력과 동작을 분리하고 UI와 비즈니스 로직을 느슨하게 연결할 수 있게 해준다.   
- → MVVM 패턴의 연결고리   
   
[Command의 핵심 인터페이스: ICommand]
```
public interface ICommand {
    bool CanExecute(object parameter);
    void Execute(object parameter);
    event EventHandler CanExecuteChanged;
}
```
   
|메서드/이벤트|	    역할|
|-------------|------------|
|Execute|	            명령을 실행한다 (버튼 누르거나 키 입력 시)|
|CanExecute|	        명령을 실행할 수 있는지 확인 (버튼 활성화/비활성화 제어)|
|CanExecuteChanged|	CanExecute 값이 변했을 때 UI 갱신을 알림|

[Command 흐름]   
1. 사용자가 버튼을 클릭   
2. WPF가 Button 클릭 감지   
3. Button의 Command를 읽음   
4. Command.CanExecute(parameter)를 호출 false면 버튼 비활성화   
5. Command.Execute(parameter)를 호출해서 실제 동작 수행 → 항상 CanExecute를 먼저 체크한 뒤 Execute가 호출된다.   
   
[RelayCommand 패턴]
- 매번 ICommand 직접 구현하면 코드가 길고 귀찮다.   
- 해결: RelayCommand 같은 커스텀 클래스 하나 만들면,   
- Action/ Func<bool> 만 넘겨서 쉽게 ICommand를 쓸 수 있다.   
- Button → RelayCommand → ViewModel 메서드 실행   

[KeyBinding과 Command 연결]
- 버튼뿐 아니라 키보드 입력(예: Enter 키 누르기)도 Command로 연결 가능
```
<Window.InputBindings>
    <KeyBinding Key="Enter" Command="{Binding TestCommand}" />
</Window.InputBindings>
```
- Enter 키를 누르면 TestCommand가 실행 → RoutedCommand, InputBinding 시스템까지 자연스럽게 연결   
   
## 7.MeasureArrange   
- WPF는 두 단계 레이아웃 엔진 (Two-Pass Layout Engine) 으로 화면을 배치.
- Measure Pass: 내가 필요한 공간은 이만큼 (요청)   
- Arrange Pass: 공간 할당 (배치)   
→ 모든 UIElement는 이 두 단계를 반드시 거쳐야 화면에 표시된다.   


[Measure Pass (측정 단계)]
- MeasureOverride 호출 → 자식 컨트롤들에게 "필요한 크기"를 요청   

흐름
1. 부모 컨트롤이 MeasureOverride 호출됨   
2. 각 자식 컨트롤에 child.Measure(availableSize) 호출   
3. 각 자식은 자신이 필요한 크기를 계산해서 DesiredSize에 기록   
4. 부모는 자식들의 DesiredSize를 보고 자신의 최종 요청 크기를 결정   


[Arrange Pass (배치 단계)]
- ArrangeOverride 호출 → 자식 컨트롤들에게 "실제 배치할 위치/크기"를 지정   

흐름   
1. 부모 컨트롤이 ArrangeOverride 호출됨   
2. 각 자식 컨트롤에 child.Arrange(Rect) 호출   
3. Rect = 어디(위치), 얼마나(크기) 배치할지 지정   
4. 자식 컨트롤은 그 Rect를 기반으로 스스로 크기와 위치를 조정   

   
[WPF 레이아웃 시스템의 특징]
|특징|	                                                설명|
|----|-------------------------------------------------------|
|레이아웃 트리 전체를 Measure → Arrange|           	    최상위 Window부터 쭉 내려간다|
|Measure 단계에서 공간 협상(Negotiation)이 이루어진다|	부모-자식 간에 공간 조율|
|Arrange 단계에서 최종 위치와 크기가 결정된다|	        픽셀 정확하게 배치|


[MeasureOverride vs ArrangeOverride 비교]
|항목|	            MeasureOverride|	        ArrangeOverride|
|----|------------------------------|-------------------------|
|목적|	            필요한 공간 계산|	    실제 위치/크기 배치|
|메서드 호출 시기|	레이아웃 측정 단계|	    레이아웃 배치 단계|
|중요한 메서드|	    child.Measure()|	        child.Arrange(Rect)|
|반환값|	            Size (요청하는 크기)|	Size (최종 배치 크기)|
   
## 8.ResourceStyle   
[WPF에서 Resource]
- WPF Resource는 XAML 내부나 외부에서 재사용 가능한 객체나 값을 저장하는 시스템.
+ SolidColorBrush 같은 단순 객체   
+ Style, ControlTemplate 같은 복합 스타일   
+ even DataTemplate, Storyboard까지 포함 가능   
- 리소스는 키(key)로 등록하고, 필요할 때 키로 참조.   
   
[StaticResource vs DynamicResource 차이]
|항목|          StaticResource|              DynamicResource|
|----|------------------------|-----------------------------|
|참조 시점|     컴파일 타임 or 로딩 타임|    런타임|
|변경 반영|     X|                           O (런타임 리소스 변경 반영)|
|퍼포먼스|      빠름|                        느림|
|사용 예시|     대부분의 고정 리소스|        테마 변경, 다국어 변경 등|

[StaticResource 흐름]
- XAML 파싱 시점에 키를 찾아서 해당 리소스 값을 "복사"해서 설정   
→ 나중에 리소스가 바뀌어도 적용되지 않는다.   
   
[DynamicResource 흐름]
- 컨트롤이 만들어질 때, 키만 저장 필요할 때마다 리소스를 찾아서 읽는다   
→ 나중에 리소스를 바꾸면 적용


[Resource Lookup 순서]
- WPF는 리소스를 찾을 때 트리 구조를 따라 올라간다. 항상 가까운 곳 먼저 찾고, 없으면 부모로 올라감   
- 리소스 찾기 순서:
    1. 자신(UserControl, Button 등)의 Resources
    2. 부모 요소(StackPanel, Grid 등)의 Resources
    3. Window.Resources
    4. App.xaml Resources
    5. Theme 레벨 (SystemTheme)

[Style]
- WPF Style은 컨트롤의 속성(Setter) 집합을 묶어놓은 리소스

|특징|	                        설명|
|----|-------------------------------|
|여러 Setter 묶기|	            Background, FontSize, Margin 등 한번에 지정|
|재사용 가능|	                    여러 컨트롤에 Style 적용 가능|
|TargetType 지정|	                Button, TextBox 등 컨트롤 타입 지정 가능|
|트리거, 데이터바인딩 확장 가능|	Trigger, DataTrigger, MultiTrigger 지원|


[Style Setter 동작 구조]
|Setter 흐름|	    설명|
|-----------|----------|
|Style 적용|	    컨트롤이 생성될 때 Style이 적용된다|
|Setter 순회|	    각 Setter마다 속성을 설정한다|
|값 우선순위|  	직접 지정한 속성 > Style Setter 값|


[요약 흐름]
```
[ Resource 등록 (XAML, Code) ]
    ↓
[ Resource 조회 (트리 구조를 따라 올라감) ]
    ↓
[ StaticResource → 고정 값 / DynamicResource → 실시간 반영 ]
    ↓
[ Style → 여러 Setter를 묶어 속성 일괄 적용 ]
```
   
[Resource/Style 시스템]
|포인트|	            요약|
|-----|------------------|
|StaticResource|	    컴파일 시점 값 복사 (고정)|
|DynamicResource|	    런타임 시점 참조 (변경 반영)|
|리소스 찾기 순서|	자신 → 부모 → Window → App|
|Style|	            여러 Setter를 묶어서 컨트롤에 일괄 적용|
   
## 9.DataTemplate_TemplateSelector   
   
[DataTemplate]
- DataTemplate은 특정 데이터 타입에 어떤 UI를 보여줄지 정의한 XAML 템플릿   
- ViewModel이나 POCO 객체 같은 데이터 객체를 시각적으로 표현할 때 사용됨   
- 대부분 ItemsControl, ListBox, ComboBox, ContentPresenter 등에서 자동으로 사용됨   
- 데이터는 단순하지만, 보여주는 방식은 다르게 하고 싶을 때 → DataTemplate   
```
<DataTemplate DataType="{x:Type local:ItemModel}">
    <TextBlock Text="{Binding Name}" Foreground="Green"/>
</DataTemplate>
→ ItemModel 타입 데이터가 오면, 이 템플릿으로 그린다.
```

[DataTemplate 사용 위치]
|사용 위치|	                설명|
|--------|----------------------|
|ItemTemplate|	            ItemsControl, ListBox 등에서 사용|
|ContentTemplate|	            ContentControl, ContentPresenter 등에서 사용|
|DataType (암시적 스타일)|	특정 타입에 자동 매핑 (ex: DataTemplate.DataType)|

   
[TemplateSelector]   
- TemplateSelector는 조건에 따라 다른 DataTemplate을 선택할 수 있는 전략 클래스.   
- DataTemplateSelector를 상속해서 SelectTemplate() 메서드를 구현   
- 하나의 ItemsControl 안에서도 데이터에 따라 다른 템플릿을 적용할 수 있음   
```
public override DataTemplate SelectTemplate(object item, DependencyObject container)
{
    if (item is ItemModel m && m.IsHighlighted)
        return HighlightedTemplate;
    return DefaultTemplate;
}
<ItemsControl ItemsSource="{Binding Items}" ItemTemplateSelector="{StaticResource MySelector}" />
```

[DataTemplateSelector 사용 구조]   
```
[ ItemsControl ]
    ↳ ItemSource: List<ItemModel>
    ↳ ItemTemplateSelector → SelectTemplate(model) 호출
        ↳ if (model.IsHighlighted) → HighlightedTemplate
        ↳ else → DefaultTemplate
```
   
[DataTemplate vs TemplateSelector]   
|항목|	    DataTemplate|	    TemplateSelector|
|----|-------------------|---------------------|
|역할|	    하나의 템플릿 정의|	여러 템플릿 중 조건 분기|
|적용 방식|	직접 XAML에 지정|	Selector 통해 조건부 반환|
|용도|	    단순한 UI 반복|	    조건부 UI 분기 (ex: 중요도 표시 등)|
   
## 10.CustomControl   
[UserControl vs CustomControl]
- UserControl은 조합용   
- CustomControl은 재사용/라이브러리/스킨 시스템용   
   
|항목|	        UserControl|	                        CustomControl|
|----|----------------------|--------------------------------------|
|상속 대상|	    UserControl|	                        Control|
|XAML 템플릿|	    클래스 내부에서 UI 정의 (고정)|	    외부 스타일/템플릿로 분리 가능 (동적 적용)|
|재사용|	        조합 기반, 확장 불리|	            완전 재사용 가능, 테마 스타일 연동 가능|
|Theme 대응|	    어려움|	                        가능 (Generic.xaml 사용)|


[CustomControl 생성 시 필수 구성 요소]
|구성|	            설명|
|----|-------------------|
|클래스|	            Control 상속 + DefaultStyleKeyProperty.OverrideMetadata() 호출|
|스타일 위치|	        Themes/Generic.xaml 파일 안|
|TemplateBinding|	    템플릿에서 외부 DependencyProperty 바인딩할 때 사용|


[핵심: DefaultStyleKeyProperty.OverrideMetadata(...)]
```
static MyFancyControl()
{
    DefaultStyleKeyProperty.OverrideMetadata(typeof(MyFancyControl),
        new FrameworkPropertyMetadata(typeof(MyFancyControl)));
}
이 코드를 호출하면 WPF는 Generic.xaml에서 TargetType=MyFancyControl인 스타일을 자동 적용하게 된다.
```
   
[Generic.xaml은 왜 Themes 폴더에 넣어야 할까]
- WPF는 다음 경로를 우선순위로 탐색  /Themes/Generic.xaml
- Control이 로드될 때, DefaultStyleKey에 지정된 타입으로 Generic.xaml 내의 Style을 찾아 자동 적용   

[TemplateBinding의 역할]
```
<TextBlock Text="{TemplateBinding FancyText}" />
```
   
|바인딩 종류|	                        설명|
|----------|-------------------------------|
|{Binding}|	                        DataContext를 기준으로 바인딩|
|{TemplateBinding}|	                템플릿 외부에서 설정된 속성을 바인딩|
|{RelativeSource TemplatedParent}|	TemplateBinding의 풀 버전 (MVVM friendly)|   
- TemplateBinding은 가볍고 빠르며 ControlTemplate 안에서 외부에서 설정된 DP 값을 바로 읽을 수 있게 한다.

   
[실행 흐름 전체 구조]
```
<MainWindow.xaml>
    <MyFancyControl FancyText="Hello" />
        ↓
[Control 생성됨]
        ↓
[DefaultStyleKey 검색 → Generic.xaml 조회]
        ↓
[Style 자동 연결 → ControlTemplate 로드]
        ↓
[TemplateBinding으로 FancyText 값 전달]
        ↓
[UI 렌더링]
```

[CustomControl의 재사용성과 Themeability]
- CustomControl은 테마에 따라 외형만 바꾸고 로직은 그대로 유지할 수 있다   
- Light/Dark 테마용 Generic.xaml을 여러 개 둘 수도 있다   
- 외부 라이브러리로 배포할 수 있다 (NuGet 가능)   


[실험 코드 복습 구조 요약]
|구성 요소|	                역할|
|--------|----------------------|
|MyFancyControl.cs|	        Control 상속 + DP 등록 + Style 연결|
|FancyTextProperty|	        사용자 정의 DP|
|Generic.xaml|	            ControlTemplate 등록 위치|
|TemplateBinding FancyText|	외부 속성 바인딩용 연결|
   
```
[MainWindow.xaml]
     ↓
<MyFancyControl FancyText="..." />
     ↓
[MyFancyControl.cs] → DependencyProperty + OverrideMetadata
     ↓
[Generic.xaml] → Style + TemplateBinding
     ↓
[ControlTemplate 적용] → 렌더링
```
   
## 11.AttachedProperty   
 [AttachedProperty]   
 - AttachedProperty는 다른 클래스가 정의한 DependencyProperty를, 다른 객체에 부착해서 사용할 수 있게 해주는 구조.   
 - 원래 TextBox.IsFocused 같은 속성이 없다면 FocusHelper.IsFocused="True" 같이 외부에서 속성을 붙이는 방식으로 확장.   
   
 [일반 DependencyProperty vs AttachedProperty]
 |항목|	        DependencyProperty|	                AttachedProperty|
 |----|----------------------------|-----------------------------------|
 |정의 위치|	    그 속성을 가진 타입 안에서 정의|	    외부 Helper 클래스에서 정의|
 |사용 대상|	    자기 자신|	                        다른 컨트롤에 부착 가능|
 |사용 예|	        Button.Content|	                    Grid.Row, Canvas.Left|


 [구조 예시]
 ```
 public static readonly DependencyProperty IsFocusedProperty =
 DependencyProperty.RegisterAttached(
     "IsFocused",
     typeof(bool),
     typeof(FocusHelper),
     new PropertyMetadata(false, OnIsFocusedChanged));
```
 |요소|	            설명|
 |----|------------------|
 |RegisterAttached|	부착 속성 등록 메서드|
 |IsFocused|	        속성 이름 (XAML에서 쓰는 이름)|
 |FocusHelper|	        소유자 클래스|
 |PropertyMetadata|	기본값 및 변경 콜백 등록|


 [예제 필수 메서드 2개]
 ```
 public static bool GetIsFocused(DependencyObject obj)
 {
     return (bool)obj.GetValue(IsFocusedProperty);
 }

 public static void SetIsFocused(DependencyObject obj, bool value)
 {
     obj.SetValue(IsFocusedProperty, value);
 }
```
 - XAML 파서가 helper:FocusHelper.IsFocused="True"를 만나면:   
 - SetIsFocused() 호출 → 값 저장   
 - 내부적으로 OnIsFocusedChanged() 호출 → 로직 실행   


 [실험 흐름 복습]
 ```
 XAML: <TextBox helper:FocusHelper.IsFocused="True"/>
       ↓
 파서가 읽음 → SetIsFocused 호출
       ↓
 값이 바뀌었으므로 OnIsFocusedChanged 호출
       ↓
 TextBox.Focus() 실행 → 포커스 부여
```

 [요약]
 ```
 [FocusHelper.cs]
    └ RegisterAttached("IsFocused")
    └ Set/GetIsFocused()

 [MainWindow.xaml]
    └ <TextBox helper:FocusHelper.IsFocused="True"/>

 [실행 시]
    └ SetIsFocused → OnIsFocusedChanged → TextBox.Focus()
```
   
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
