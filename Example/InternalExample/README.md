
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
[Behavior 용도]
- MVVM 패턴에서 ViewModel은 View의 이벤트를 몰라야 함.
- but Button.Click, MouseEnter 같은 UI 이벤트는 View 쪽에만 존재.   
- <Button Click="OnClick"/> // x code-behind에 종속됨   
- Behavior + EventToCommand = View 이벤트를 ViewModel의 ICommand로 연결   
   
[Behavior 시스템]
- WPF에선 Microsoft.Xaml.Behaviors.Wpf 패키지를 사용해 View에 붙는 이벤트-기반 동작(Behavior)을 구성할 수 있다.   
   
[핵심 클래스]
- Interaction.Triggers: 동작 바인딩 컨테이너   
- EventTrigger: 특정 이벤트 발생 시 트리거   
- InvokeCommandAction: 특정 ICommand를 실행   

   
[EventToCommand 흐름 구조]
```
<Button>
  <i:Interaction.Triggers>
    <i:EventTrigger EventName="Click">
      <i:InvokeCommandAction Command="{Binding SomeCommand}" />
    </i:EventTrigger>
  </i:Interaction.Triggers>
</Button>

Click 발생 → EventTrigger 감지 → InvokeCommandAction 실행 → Command.Execute() 호출
```

[InvokeCommandAction]
```
public class InvokeCommandAction : TriggerAction<DependencyObject>
{
    public ICommand Command { get; set; }
    protected override void Invoke(object parameter)
    {
        if (Command?.CanExecute(parameter) == true)
            Command.Execute(parameter);
    }
}
```
- 내부적으로 Command.Execute(...)를 호출해준다   
- Parameter도 전달 가능 (CommandParameter 속성 있음)   


[이 구조의 장점]
|장점|	                                    설명|
|----|-------------------------------------------|
|ViewModel에서 View 이벤트 알 필요 없음|	    MVVM 원칙 지킴|
|XAML만으로 UI 이벤트 연결|	                코드비하인드 없음|
|Command 패턴 그대로 활용|	                CanExecute/Execute 로직도 그대로 적용 가능|
|Parameter 전달, 다양한 이벤트 대응 가능|	    MouseEnter, Loaded, Drop 등 확장 쉬움|

   
[정리]
|요소|	                            설명|
|----|-----------------------------------|
|Interaction.Triggers|	            이벤트를 잡아낼 컨테이너|
|EventTrigger EventName="Click"|	    대상 이벤트 지정|
|InvokeCommandAction|	                ICommand 실행 트리거|
|ViewModel.ButtonClickCommand|	    RelayCommand로 연결된 로직 수행|
|Output TextBlock|	                ViewModel의 Output 속성 바인딩 결과 출력|


[요약 구조도]
```
[ Button (View) ]
   └ Interaction.Triggers
      └ EventTrigger ("Click")
         └ InvokeCommandAction → ViewModel.Command.Execute()
```

Command (ICommand):   
- ViewModel에서 로직을 정의하는 방식   
- WPF에서는 주로 Button.Command에서 사용   
- UI 요소가 직접 Command를 연결 (ex. Button.Command="{Binding SomeCommand}")   

Behavior (EventToCommand):   
- View (XAML)에서 발생하는 이벤트를 ViewModel의 Command로 전달하는 방식   
- Interaction.Triggers를 통해 View의 다양한 이벤트 (Click, MouseEnter, Loaded)를 ViewModel로 연결   

- Command는 원래 Button 등 명령형 UI 요소에서만 사용 가능하지만,   
- Behavior는 모든 UI 요소 (TextBlock, Grid, Border 등)에서 이벤트 처리 가능   
   
## 13.StyleSelectorDataTriggerMultiTrigger   
 - StyleSelector:	객체 상태에 따라 동적으로 스타일 선택   
 - DataTrigger:	    IsActive == true일 때 스타일 적용   
 - MultiTrigger:	IsActive == true && Level == High일 때 스타일 적용   

 [흐름 정리]
 ```
 [ ViewModel ]
    → Item { IsActive = true, Level = "High" }

 [ View (XAML) ]
    → StyleSelector 적용 → 강조 스타일
    → DataTrigger 적용 → Green 색상
    → MultiTrigger 적용 → Blue 색상 + Italic
```

 [목적]
 - StyleSelector: 데이터 조건에 따라 다른 Style 적용   
 - DataTrigger: 단일 바인딩 조건으로 Setter 발동   
 - MultiDataTrigger: 여러 바인딩 조건을 만족해야 Setter 발동   

 
 [작동 흐름]
 ```
 [ ViewModel (StatusModel) ]
    ↳ IsActive = true, Level = "High"
         ↓
 [ ContentControl.Content = Item ]
         ↓
 [ DataTemplate 내 TextBlock → DataContext = Item ]
         ↓
 [ Style = MultiTriggerStyle → MultiDataTrigger 평가 ]
         ↓
 조건 만족 시:
         → Foreground = Blue, FontStyle = Italic
 ```
   
 [StyleSelector]
 |항목|	        내용|
 |----|--------------|
 |용도|	        한 객체 또는 항목에 대해 조건 분기하여 서로 다른 Style 객체 자체를 선택|
 |대상|	        ItemsControl.ItemContainerStyleSelector, ContentPresenter.ContentTemplateSelector 등|
 |작성 위치|	    C# 클래스에서 StyleSelector 상속 → SelectStyle() 오버라이드|
 |적용 방식|	    ItemContainerStyleSelector="{StaticResource MyStyleSelector}"|
 |특징|	        완전히 다른 스타일 객체를 반환할 수 있어 매우 유연|
 |제한점|	        TextBlock 등 대부분 컨트롤에는 직접 사용 불가 → 반드시 연결 가능한 컨트롤에서만 사용 가능|


 [DataTrigger]
 |항목|	        내용|
 |----|--------------|
 |용도|	        ViewModel의 속성 하나의 값이 특정 값과 일치할 때 스타일 Setter 발동|
 |대상|	        Style, ControlTemplate, DataTemplate 내에서 사용 가능|
 |작성 위치|	    XAML 내부에서 Style.Triggers에 직접 정의|
 |적용 방식|	    <DataTrigger Binding="{Binding IsActive}" Value="True">|
 |특징|	        가장 많이 쓰이는 단일 조건 기반 트리거|
 |제한점|	        하나의 조건만 검사 가능|

    
 [MultiDataTrigger]
 |항목|	        내용|
 |----|--------------|
 |용도|	        ViewModel의 여러 속성 값을 복합 조건으로 검사하여 스타일 Setter 발동|
 |대상|	        Style 내부에서 사용 가능|
 |작성 위치|	    XAML에서 Style.Triggers 내부|
 |적용 방식|	    <MultiDataTrigger> + <Condition Binding=...> 다수|
 |특징|	        복합 조건 분기에 적합 (AND 조건)|
 |제한점|	        OR 조건은 불가능, 반드시 모든 조건 만족 시 발동|

 
 [비교 요약]
 |항목|	    StyleSelector|	                        DataTrigger|	                MultiDataTrigger|
 ----|-------------------|--------------------------------------|-----------------------------------|
 |조건 위치|	C# 코드|	                                XAML|	                    XAML|
 |조건 개수|	자유|	                                1개|	                        2개 이상 (AND)|
 |제어 범위|	스타일 전체를 분기|	                    스타일 일부 속성 Setter|	    스타일 일부 속성 Setter|
 |재사용성|	매우 유연 (Style 객체 자체 교체)|	    비교적 간단|	                복합 분기 가능|
 |적용 위치|	ItemsControl, ContentControl 등|	        Style, ControlTemplate 등|	Style 내부|
 |장점|	완전한 스타일 전환 가능|	                    간단하고 직관적|	            여러 조건 통합 가능|
 |단점|	XAML만으로는 불가|	                        조건 1개만 가능|	            조건이 늘어날수록 복잡|
 
## 14.VisualStateManager   
- VisualStateManager는 컨트롤의 상태(UI 상태) 전이를 선언적으로 정의하고
- 마우스/포커스/사용자 동작에 따라 시각적 변화를 적용하는 시스템.

[요소]
|구성 요소|	        설명|
|--------|--------------|
|VisualStateGroup|	상태들을 하나의 그룹으로 묶음 (CommonStates, FocusStates 등)|
|VisualState|	        개별 상태 정의 (Normal, MouseOver, Pressed)|
|Storyboard|	        상태 진입 시 실행할 시각적 애니메이션 (색, 위치, 크기 등 전환)|


[상태 전이 흐름]
```
초기 상태 → MouseOver (마우스 진입)
          ↘ Pressed (마우스 클릭)
          ↘ MouseOver (버튼 뗌)
          ↘ Normal (마우스 나감)
```

[작동 원리]
1. VisualStateManager.GoToState(this, "Pressed", true) 호출됨   
2. 현재 컨트롤의 VisualStateGroup에서 "Pressed" 상태를 찾음   
3. 해당 상태의 Storyboard가 있으면 실행됨   
4. 이전 상태의 시각 효과는 자동으로 취소됨 (중복 전이 방지)   


[예시 해석]
```
<VisualState x:Name="MouseOver">
    <Storyboard>
        <ColorAnimation Storyboard.TargetName="bd" 
                        Storyboard.TargetProperty="(Border.Background).(SolidColorBrush.Color)"
                        To="LightBlue" Duration="0:0:0.2" />
    </Storyboard>
</VisualState>
```
- 상태 이름: "MouseOver"   
- 대상: "bd"라는 이름의 Border   
- 동작: Background의 Color를 LightBlue로 0.2초간 애니메이션   


[VisualStateManager 내부 흐름]
|그룹 이름|	        상태 예시|
|---------|------------------|
|CommonStates|	    Normal, MouseOver, Pressed, Disabled|
|FocusStates|	        Focused, Unfocused|
|ValidationStates|	Valid, Invalid|
|SelectionStates|	    Selected, Unselected|


[사용 위치]
|위치|	            설명|
|----|-------------------|
|ControlTemplate|	    기본 컨트롤 (Button, CheckBox 등)의 동작 정의|
|UserControl|	        우리가 만든 StatefulButton처럼 직접 정의 가능|
|Style 내부|	        ControlTemplate 안에서 시각 상태 정의 가능|


[정리]
|항목|	        내용|
|---|----------------|
|핵심 개념|	    VisualStateGroup 안에 여러 상태를 정의, 상태 전환 시 Storyboard 실행|
|주요 메서드|	    VisualStateManager.GoToState(control, "StateName", true)|
|적용 위치|	    UserControl, ControlTemplate 내부|
|실전 활용|	    상태 기반 애니메이션, 사용자 상호작용 반응, 테마 전이 등|
   
## 15.TemplateBinding_Binding   
[TemplateBinding]   
|항목|	            내용|
|---|--------------------|
|정의|	            ControlTemplate 내부에서 TemplatedParent(컨트롤 자신) 의 속성을 연결하는 특수 바인딩|
|기준 경로|	        항상 TemplatedParent.<속성> (정적 연결)|
|주 사용 위치|	    ControlTemplate, Style.Setters, Generic.xaml|
|평가 시점|	        XAML 컴파일 시점에 경로가 고정됨|
|성능|             매우 빠름 (정적 평가, 최적화됨)|
|제한사항|	        DataContext 접근 불가, 기능 유연성 떨어짐|
|주 용도|	            재사용 가능한 CustomControl의 스타일 작성 시 속성 연결|

   
[Binding]
|항목|	            내용|
|----|-------------------|
|정의|	            가장 일반적인 바인딩 방식으로, DataContext 또는 RelativeSource 기준으로 동작|
|기준 경로|	        DataContext or ElementName, RelativeSource, Self, TemplatedParent 등|
|주 사용 위치|	    XAML 어디서나 가능 (View, Template, DataTemplate 등)|
|평가 시점|	        런타임에 동적으로 경로 탐색|
|성능|	            약간 느림 (런타임 바인딩 경로 탐색 필요)|
|유연성|	            높음 (모델, 뷰 요소, 상위 컨트롤 등 접근 가능)|
|주 용도|	            일반적인 MVVM 데이터 연결, 동적 구조 바인딩|


[요약]
|항목|	        TemplateBinding|	                Binding|
|----|--------------------------|--------------------------|
|적용 기준|	    컨트롤 자신 (TemplatedParent)|	외부 구조 or DataContext|
|결과 안정성|	    항상 고정 경로|	                구조 변경 시 깨질 수 있음|
|런타임 영향|	    거의 없음|	                    큰 트리 구조 + 다량일 때 부하 가능|
|실험 예|	        Label, LabelForeground|	        Content via RelativeSource|


[선택 기준 요약]
|사용 상황|	                                                추천 방식|
|--------|-----------------------------------------------------------|
|ControlTemplate 내부에서 컨트롤 자신의 속성만 쓰는 경우|	    TemplateBinding (간결, 빠름)|
|외부 데이터와 연결하거나 ViewModel 참조할 경우|	            Binding (유연, 범용)|
|RelativeSource 접근이 필요한 복합 경로|	                    Binding + RelativeSource=TemplatedParent|

   
[요약]
- TemplateBinding은 속도 빠르고 간단하지만 유연성 없음,   
- Binding은 유연하지만 성능 약간 떨어짐   
    → ControlTemplate 안에서는 TemplateBinding이 기본.   
- 복잡한 바인딩이 필요하면 Binding + RelativeSource 조합 사용   

   
## 16.StaticDynamicResource
## 17.ThemeDictionary   
- ThemeDictionary는 Application.Resources의 MergedDictionaries 안에   
- 다양한 테마 리소스 (예: Light.xaml, Dark.xaml) 를   
- 동적으로 전환할 수 있도록 구성하는 구조   

[구조 구성 요소]
|구성 요소|	                                                        설명|
|---------|-------------------------------------------------------------|
|ResourceDictionary|	                                                리소스를 묶는 단위 (색상, 스타일, 브러시 등)|
|MergedDictionaries|	                                                여러 Dictionary를 하나로 합침|
|DynamicResource|	                                                    런타임에서도 변경 감지 가능하게 연결|
|Application.Current.Resources.MergedDictionaries.Clear().Add()|	    리소스 전환 로직의 핵심|


[흐름 요약]
1. Light.xaml, Dark.xaml에 동일한 키(PrimaryBrush, TextBrush)로 리소스 정의   
2. MainWindow에서 DynamicResource를 사용해 이 키들을 바인딩   
3. 버튼 클릭 시 Application.Current.Resources.MergedDictionaries를 교체   
4. DynamicResource이기 때문에 UI가 즉시 변경됨   

[동작 원리]
```
[ XAML에서 DynamicResource "PrimaryBrush" 요청 ]
        ↓
[ ResourceDictionary에서 "PrimaryBrush" 경로 추적 ]
        ↓
[ 리소스 교체 시 → 경로 동일하므로 다시 평가됨 ]
        ↓
[ 브러시 인스턴스 변경 → 바인딩된 컨트롤에 즉시 반영 ]
```

|키 이름|	                                    설명|
|-------|-------------------------------------------|
|PrimaryBrush|	                            버튼 배경색, Border 배경 등|
|TextBrush|	                                기본 텍스트 색|
|ErrorBrush|                                경고 색 (적색 계열 등)|
|ControlForeground, ControlBackground|	    사용자 정의 컨트롤 스타일에 사용 가능|

   
[주의 사항]
|주의 항목|	                설명|
|--------|----------------------|
|StaticResource 사용 시|	    테마 전환 효과 없음 (변경 감지 안 됨)|
|Style 키 충돌|	            각 테마에 동일한 스타일 키 유지 필수|
|리소스 없는 키 참조|	        DynamicResource는 실행 시 오류 발생 안 하지만 화면 미출력됨|
|MergedDictionaries 순서|	    중복 키가 있을 경우, 마지막에 추가된 것이 우선됨|
   
## 18.MarkupExtension   
 [MarkupExtension]
 - {Binding}, {StaticResource}, {x:Type} 처럼 XAML에서 {} 문법으로 쓰이는 모든 것은 내부적으로 MarkupExtension을 상속한 클래스.   

   
 [XAML 처리 흐름]
 1. XAML 파서가 `{ext:Something}`을 만나면
 2. MarkupExtension 객체를 생성
 3. .ProvideValue(IServiceProvider) 호출
 4. 반환값이 해당 속성에 할당됨

 <TextBlock Text="{ext:EnumDescription Value={x:Static ext:MyEnum.First}}"/>
 → XAML 파서는 EnumDescriptionExtension 객체를 만들고
 → ProvideValue()를 호출해 반환된 문자열로 TextBlock.Text를 설정
 
 [핵심 메서드]
 - public override object ProvideValue(IServiceProvider serviceProvider)
 - 이 메서드는 실제로 해당 XAML 속성에 들어갈 값을 리턴함   
 - serviceProvider를 통해 바인딩 대상, 위치 등의 메타정보도 접근 가능 (고급 활용)   

    
 [IServiceProvider에서 얻을 수 있는 정보]
 |형식|	                설명|
 |----|----------------------|
 |IProvideValueTarget|	    대상 객체와 속성 (어떤 컨트롤의 어떤 속성에 적용 중인지)|
 |IRootObjectProvider|	    최상위 루트 요소|
 |IXamlTypeResolver|	    문자열 타입명을 .NET Type으로 변환 가능|

 
 [장점]
 |이유|	                    설명|
 |----|--------------------------|
 |XAML 구조를 확장 가능|	    {MyBinding}, {Translate Key=...} 등 커스텀 DSL 구현|
 |Binding보다 간결함|	        Binding 없이도 값 매핑 처리 가능|
 |리소스 재사용성 증가|	    Enum → Description, Localize, Bool → Visibility 등|
 |메타정보 활용 가능|	        위치, 타겟 속성, 객체 참조까지 다 접근 가능|
 
 [예]
 |예제|	                                                설명|
 |----|------------------------------------------------------|
 |{loc:Text Key=Main.Title}|	                            다국어 텍스트 변환|
 |{enum:Description Value={x:Static MyEnum.Value}}|	    enum → 설명 표시|
 |{vm:MethodInvoke Target=Foo, Method=Refresh}|	        ViewModel 메서드 호출 바인딩처럼 쓰기|
 |{proxy:ViewModel}|	                                    정적 리소스처럼 ViewModel 주입|

 
 [요약]
 |항목|	            내용|
 |----|------------------|
 |핵심 개념|	        {} 구문은 전부 MarkupExtension|
 |주요 메서드|	        ProvideValue() 에서 반환된 값이 XAML 속성에 설정됨|
 |적용 위치|	        거의 모든 XAML 속성, 바인딩, 트리거, 리소스 등|
 |활용 사례|	        Enum 설명, 다국어 처리, ViewModel 프록시, 조건 표현 등|
 |장점|	            선언적 DSL 확장 + 컴팩트한 표현 + 재사용 가능|
    
## 19.Messenger   
[Messenger 패턴]
- ViewModel ↔ ViewModel 간에 직접 참조 없이 느슨하게 메시지를 전달할 수 있는 구조   
   
[구성 요소]
|구성요소|	            설명|
|-------|-------------------|
|Messenger (싱글턴)|	    메시지를 보내고(subscribe/send) 구독자를 관리하는 중심|
|Register<T>()|	        특정 타입의 메시지를 받을 콜백을 등록|
|Send(message)|	        등록된 모든 콜백에 메시지를 전달|
|ViewModel|	            메시지 전송자(Sender) 또는 수신자(Receiver)가 될 수 있음|

   
[동작 흐름]
1. ReceiverViewModel에서 Messenger.Register<string>() 호출   
2. MainViewModel에서 Messenger.Send(message) 호출   
3. Messenger는 모든 string 타입 구독자에게 메시지 전달   
4. ReceiverViewModel의 콜백이 실행되고 값 변경됨   
5. UI에 반영됨 (INotifyPropertyChanged)    
   
[요약]
- 텍스트 입력 → 버튼 클릭 → Messenger.Send(string) 실행   
- 구독자(ReceiverViewModel)에서 메시지 수신 → Message 속성 갱신   
- 바인딩된 TextBlock에 변경 내용 반영됨   


[장점]
|항목|	        설명|
|---|----------------|
|느슨한 결합|	    ViewModel 간에 서로 몰라도 통신 가능|
|구현이 간단|	    1~2개의 클래스만으로 구성 가능|
|실시간 전파|	    이벤트 없이도 전달 즉시 실행됨|

[단점 / 주의점]
|항목|	                                    설명|
|----|-------------------------------------------|
|모든 구독자가 동일 타입 콜백을 다 받음|	    메시지 분기 로직이 없으면 오염 위험|
|메모리 누수 위험|	                        구독 해제를 안 하면 GC 대상이 안 됨 (강한 참조 유지됨)|
|구조 커질수록 한계|	                        메타 정보 없음 (누가 보냈는지, 어디서 받는지 모름)|
|스레드 안전성 없음|	                        기본 구조는 멀티스레드 환경에서는 unsafe함|


[사용 전략]    
|상황|	                            적합 여부|
|----|----------------------------------------|
|ViewModel 간 간단한 메시지 전달|	    매우 적합|
|이벤트 UI ↔ ViewModel 전달|	        가능 (커맨드 대신 쓸 수 있음)|
|대규모 Pub/Sub 구조|	                Prism IEventAggregator가 더 나음|
|타입별 분기/필터링|	                확장 구조 필요 (메시지 타입 기반 Dictionary 등)|

   
[요약2]
- Messenger 패턴은 간단하고 빠른 메시지 전달용 구조로서 ViewModel 간 참조를 제거하고, 작은 규모 또는 학습용 프로젝트에서 유용.   
- 실전에서는 메모리 관리, 타입 분기, 스레드 안정성을 확장하거나 Prism의 IEventAggregator 등으로 넘어가는 게 일반적.   
   

## 20.MultiBindingIMultiValueConverter   
- 여러 ViewModel 속성의 값을 조합하여 하나의 UI 속성을 제어하고 싶을 때 사용하는 WPF 바인딩 구조.

[구성 요소]
|구성요소|	            설명|
|-------|-------------------|
|MultiBinding|	        <MultiBinding> 태그로 여러 개의 Binding을 그룹으로 묶음|
|IMultiValueConverter|	Convert(object[] values, ...)에서 모든 바인딩 값을 받아 하나의 출력값 생성|

[요약]
- IsAdmin, IsEnabled 두 속성을 체크박스로 제어   
- 버튼의 IsEnabled 속성은 MultiBinding으로 이 두 값을 결합   
- AndConverter는 둘 다 true일 때만 버튼 활성화   
- 버튼이 눌리면 ActionLog 텍스트가 바뀌며 리액션 발생   


[동작 흐름]
1. 사용자가 체크박스를 토글 → IsAdmin / IsEnabled 속성 변경
2. MultiBinding이 둘 값을 IMultiValueConverter에 전달
3. Convert(values[]) → true or false 반환
4. Button.IsEnabled 속성에 적용됨
5. 버튼이 눌리면 Command 실행 → 로그 출력됨

[코드 구조 핵심]
```
<Button.IsEnabled>
    <MultiBinding Converter="{StaticResource AndConverter}">
        <Binding Path="IsAdmin"/>
        <Binding Path="IsEnabled"/>
    </MultiBinding>
</Button.IsEnabled>
```

[사용처]
|상황|	                            MultiBinding 사용 적합 여부|
|----|----------------------------------------------------------|
|여러 조건 조합이 필요한 UI|	        적합|
|A && B → 색 바꾸기 / 버튼 활성화|	적합|
|단일 값으로 충분한 경우|	            일반 Binding 사용이 더 간단|
|값 변경 시점이 서로 다를 때|	        값 순서 유의 필요|

[장점]
|항목|	            설명|
|----|-------------------|
|선언적|	            XAML에서 바인딩만으로 복합 조건 처리 가능|
|MVVM 친화적|	        ViewModel 수정 없이 로직을 분리 가능|
|실전 확장성|	        색상, Visibility, ToolTip 등 거의 모든 속성에 활용 가능|

[주의사항]
|항목|	            설명|
|----|-------------------|
|순서 의존|	        values[]는 바인딩 순서 그대로 전달됨|
|ConvertBack 복잡|	대부분 OneWay 바인딩으로 사용|
|성능|	            과도한 MultiBinding은 렌더링 타이밍에 영향 줄 수 있음 (특히 트리거 내부 등)|

[요약]
- MultiBinding은 복수 속성 → 단일 속성 흐름을 선언적으로 제어할 수 있는 WPF 기능.   
- IMultiValueConverter를 통해 MVVM 구조를 깔끔하게 유지하면서 UI 로직을 분리할 수 있다.   
- 실전에서는 버튼 제어, 색상, 텍스트 결정, 상태 결합 등 거의 모든 UI 조건 분기에 활용된다.   
   
## 21.RelativeSourceFindAncestor   
[RelativeSource]
- RelativeSource={RelativeSource Mode=FindAncestor, AncestorType=...}   
- 이 바인딩은 현재 요소 기준으로 XAML 트리 상위에서 지정한 타입의 조상을 탐색해서 해당 요소의 속성에 바인딩하는 방식.   
   
[동작 흐름]
- 현재 요소 내부 (예: DataTemplate 안) → 시각 트리 상위 탐색 → 지정한 AncestorType과 일치하는 첫 번째 조상 찾기 → 그 조상의 속성에 바인딩   
   
[요약]
- ViewModel에 Title 속성이 있고, Window.DataContext.Title로 설정됨   
- ItemsControl.ItemTemplate 내부에서:
```
<TextBlock Text="{Binding DataContext.Title, RelativeSource={RelativeSource AncestorType=Window}}" />
```
- 이 바인딩은 현재 템플릿 요소 기준으로 상위 Window를 찾고,
- 그 Window.DataContext의 Title 속성을 가져와 출력함   

[주요 사용 예시]
|상황|	                                        바인딩|
|----|-------------------------------------------------|
|DataTemplate 내부에서 ViewModel 상위 참조|	    AncestorType=Window or UserControl|
|UserControl 내부에서 부모 컨트롤 속성 접근|	    AncestorType=Grid, TabItem 등|
|트리 내부에서 TreeViewItem → TreeView 접근|	    AncestorType=TreeView|

[구문 예시]
<TextBlock Text="{Binding DataContext.Title, RelativeSource={RelativeSource AncestorType=Window}}" />

|요소|	                설명|
|----|-----------------------|
|AncestorType=Window|	    조상 중 Window 타입을 찾음|
|DataContext.Title|	    찾은 조상의 DataContext에 바인딩|


[장점]
|항목|	                설명|
|----|-----------------------|
|MVVM 구조 유지|	        ViewModel 간 직접 참조 없이 UI 상위 요소 접근 가능|
|템플릿 재사용 가능|	    템플릿 내부에서 ViewModel 상위 구조를 감지|
|복잡한 UI 구조 대응|	    중첩 컨트롤, ItemTemplate, ControlTemplate 등에서 유용|

[주의사항]
|항목|	                    설명|
|----|---------------------------|
|시각 트리 기준|	            Visual Tree를 따라 탐색하므로 Logical Tree 기반 컨트롤과 혼동 주의|
|AncestorType 정확해야 함|	타입이 맞지 않으면 바인딩 실패 (값 없음)|
|성능|	                    트리 탐색이므로 과도한 중첩은 성능에 영향 가능성 있음|

[사용 포인트]
|상황|	                                                이유|
|----|-------------------------------------------------------|
|DataTemplate 안에서 상위 ViewModel 정보가 필요할 때| 	AncestorType=Window 사용|
|CustomControl 내부에서 호스트 속성 참조|	                AncestorType=UserControl|
|복합 템플릿 구조에서 바인딩 경로가 꼬일 때|	            명확한 조상 탐색으로 해결 가능|

## 22.ElementNameRelativeSource   
[ElementName vs RelativeSource]
|바인딩|                     방식	설명|
|------|--------------------------------|
|ElementName=xxx|	           이름으로 명확히 지정한 요소를 바인딩 대상으로 사용|
|RelativeSource Mode=...|	   현재 요소 기준으로 상위 또는 자신을 기준으로 탐색하여 바인딩 대상 결정|

[결과 요약]
|TextBlock 위치|	바인딩 방식|	                결과|
|--------------|------------|------------------------|
|GroupBox 안쪽|	ElementName=txtSource|	    정상 작동 (이름으로 명확 지정)|
|GroupBox 안쪽|	FindAncestor of TextBox|	    실패 (상위 트리에 TextBox 없음)|

[동작 흐름 비교]
[ElementName]   
1. XAML 파싱 시 이름 등록됨 (txtSource)   
2. 바인딩 엔진이 TextBlock에서 txtSource 이름을 찾음   
3. 해당 요소의 Text 속성과 연결   

[RelativeSource Mode=FindAncestor]   
1. 현재 요소 기준으로 시각적 트리 상위 탐색 시작   
2. `AncestorType=TextBox`에 해당하는 조상이 있는지 확인   
3. 없으면 바인딩 실패 (null)   


[차이 요약]
|항목|	            ElementName|	                    RelativeSource|
|----|--------------------------|-------------------------------------|
|대상 지정 방식|	    명시적 이름 기반|	            트리 탐색 기반|
|작동 범위|	        어디든 이름만 알면 가능|	        트리 상위 구조 내에서만 가능|
|트리 독립성|	        높음|	                        트리 종속적|
|해석 시점|	        로딩 시|	                        로딩 시 (탐색 필요)|
|주요 용도|	        외부에 있는 컨트롤 속성 접근|	템플릿/트리 안에서 부모 접근|

[사용 팁]
|상황|	                                                    추천 방식|
|----|----------------------------------------------------------------|
|명확한 컨트롤 참조|	                                        ElementName|
|템플릿/데이터템플릿 내부 → 상위 ViewModel, Control 참조|	RelativeSource Mode=FindAncestor|
|내부 요소 간 참조 (Self, TemplatedParent, PreviousData)|	    RelativeSource|
|템플릿 외부에 있는 이름 있는 컨트롤 접근|	                ElementName (단, 이름 해석 순서 유의)|

[주의사항]
|항목|	                                        설명|
|----|-----------------------------------------------|
|ElementName 해석 순서|	                        참조 대상이 아직 생성되지 않았으면 바인딩 실패|
|RelativeSource 탐색 실패|	                    상위 트리에 원하는 타입이 없으면 null|
|둘 다 OneTime Binding이면 초기값만 반영됨|	    바인딩 모드 주의|
   

[결론 요약]
- ElementName은 이름으로 직접 지정해서 위치와 관계없이 안정적으로 바인딩할 수 있고,   
- RelativeSource는 템플릿 내부처럼 이름이 없는 상황에서 상위 트리 구조를 따라 바인딩할 수 있게 해줌.   
- 실전에서는 둘을 목적에 따라 구분해서 사용해야 하고,
- XAML 구조가 복잡할수록 RelativeSource를 사용하는 빈도가 증가.
   
## 23.RelativeSource_Mode=Self_TemplatedParent   
[비교]
|바인딩 방식|	                            설명|	                        동작|
|----------|-----------------------------------|-------------------------------|
|RelativeSource Mode=Self|	            컨트롤 자기 자신|	            StyledButton의 SelfColor 직접 참조|
|RelativeSource Mode=TemplatedParent|	    컨트롤이 포함된 템플릿 대상|	    템플릿으로 감싸진 컨트롤의 부모 속성 (TemplateColor)|

[동작 원리]
1. Mode=Self
- 컨트롤 자체에서 자신의 속성에 접근   
- UI 구조와 무관하게 항상 자기 자신 기준   
- 예제에서는 StyledButton.SelfColor 직접 사용
```
<Setter Property="Background" Value="{Binding SelfColor, RelativeSource={RelativeSource Mode=Self}}"/>
```

2. Mode=TemplatedParent
- ControlTemplate 또는 ContentPresenter로 감싸진 컨트롤의 속성   
- ControlTemplate 내부에서 사용됨   
- 예제에서는 상위 UserControl의 TemplateColor 사용
```
<Setter Property="Foreground" Value="{Binding TemplateColor, RelativeSource={RelativeSource Mode=TemplatedParent}}"/>
```

|Button|	                    배경색 (Self)|        글씨색 (TemplatedParent)|
|------|----------------------------------|--------------------------------|
|Self Button|	                LightGreen (정상)|	    기본 색상 (적용 X)|
|TemplatedParent Button|	    기본 색상 (적용 X)|	    LightSalmon (정상)|

[동작 차이]
- Self Button은 자기 자신의 속성을 직접 참조 (SelfColor).
- TemplatedParent Button은 ControlTemplate 대상 속성을 참조 (TemplateColor).   

```
<Setter Property="Background" Value="{Binding SelfColor, RelativeSource={RelativeSource Mode=Self}}"/>
```
|핵심 동작|	    설명|
|--------|----------|
|바인딩 대상|	    자기 자신 (현재 컨트롤)|
|적용 위치|	    스타일, 트리거, ControlTemplate 내부|
|주요 사용|	    CustomControl 내부에서 자신의 속성 바인딩|
|주요 특징|	    ViewModel이나 외부 요소에 의존하지 않음|

```
<Setter Property="Foreground" Value="{Binding TemplateColor, RelativeSource={RelativeSource Mode=TemplatedParent}}"/>
```
|핵심 동작|	    설명|
|--------|----------|
|바인딩 대상|	    ControlTemplate이 적용된 부모 컨트롤|
|적용 위치|	    ControlTemplate 내부 (예: CustomControl)|
|주요 사용|	    CustomControl에서 외부 스타일 적용|
|주요 특징|	    Template의 외부 속성 참조 가능 (재사용성 높음) |
   
## 24.TemplateBindingRelativeSource   
[TemplateBinding vs RelativeSource]

1. TemplateBinding (템플릿 바인딩)
- 동작 방식: ControlTemplate 내부에서 TemplateBinding을 통해 상위 컨트롤의 속성을 직접 바인딩.   
- 바인딩 구조: 내부적으로 상위 컨트롤의 Dependency Property에 직접 연결.   
- 속도: 가장 빠른 바인딩 방식 중 하나 (PropertyChangedCallback 없이 직접 연결).   
- 주요 특징:   
   + ControlTemplate 내부에서만 사용할 수 있음.   
   + ControlTemplate의 TargetType에서 정의된 속성에만 적용 가능.   
   + 변경 알림 없이 속성이 즉시 반영.   

2. RelativeSource (TemplatedParent)
- 동작 방식: Binding 사용, RelativeSource Mode를 TemplatedParent로 지정하여 상위 컨트롤에 바인딩.   
- 바인딩 구조: Binding 시스템을 통해 DependencyProperty 값 접근.   
- 속도: Binding 시스템을 사용하므로 TemplateBinding보다 약간 느림.    
- 주요 특징:   
   + ControlTemplate 외부에서도 사용 가능.   
   + DependencyProperty 변경 알림을 통해 자동으로 UI에 반영.   

[차이점 요약]
|특성|	        TemplateBinding|	                            RelativeSource (TemplatedParent)|
|----|--------------------------|---------------------------------------------------------------|
|성능|	        매우 빠름 (직접 바인딩)|	                    Binding 시스템 사용 (약간 느림)|
|사용 위치|	    ControlTemplate 내부에서만 사용 가능|	    ControlTemplate 외부에서도 사용 가능|
|바인딩 대상|	    ControlTemplate의 TargetType 속성|	        DependencyProperty가 있는 모든 컨트롤|
|변경 알림|	    없음 (즉시 반영)|	                        DependencyProperty의 변경 자동 반영|
|동적 변경|	    직접 설정된 값만 반영|	                    Binding을 통해 자동 반영|

[실험 결과 (코드 실행 시)]
- TemplateBinding 버튼 (LightBlue → LightGreen):    
- 클릭 시 Background 속성이 바로 변경되지만, ControlTemplate의 Background 속성은 변하지 않음.   

- RelativeSource 버튼 (LightCoral → LightYellow):    
- 클릭 시 Background 속성이 변경되며, ControlTemplate 내부에서도 변경이 반영됨.   
   
## 25.Freezables   
[코드 구성]
- MainWindow.xaml: Rectangle 컨트롤을 사용하여 Freezable Brush를 적용.
- MainWindow.xaml.cs:   
   + SolidColorBrush (LightBlue) 생성.   
   + Freeze() 메서드를 통해 Freezable 적용 (불변 상태).   
   + 버튼 클릭 시 Brush 색상 변경을 시도.   
    
    
[Freezable 구조 동작 원리]
- WPF에서 성능 최적화된 공유 가능한 객체를 정의하는 클래스.   
- Freezable 객체는 Immutable (불변) 상태로 전환할 수 있어 성능과 메모리 사용을 최적화.   
- 대표적인 Freezable 객체:   
   + SolidColorBrush   
   + Transform (ScaleTransform, RotateTransform 등)   
   + Geometry (PathGeometry, EllipseGeometry 등)   
   
[Freezable 객체의 특징]
|특성|	                    설명|
|----|---------------------------|
|불변성 (Immutable)|	        Freeze() 메서드를 호출하면 더 이상 수정할 수 없음.|
|성능 최적화|	                불변 상태로 전환되면 WPF 내부에서 공유 가능.|
|상태 변경 가능 여부|	        IsFrozen 속성을 통해 확인 가능.|
|자식 Freezable 동기화|	    Freezable 객체의 자식도 Freeze 상태로 전환.|

   
[실험 결과 (코드 실행 시)]
- Rectangle의 Brush는 LightBlue로 시작.   
- 버튼 클릭 시:   
   + Frozen 상태 (Freezable 적용)일 경우 "Brush is Frozen (Immutable)" 메시지.   
   + Frozen 상태가 아니면 색상 변경 (LightGreen ↔ LightBlue).   
- 성능 최적화 확인: Freeze 상태에서는 WPF 렌더링 엔진이 Brush를 공유 가능하게 처리하여 성능 향상.   
   
## 26.NavigationFramePage
[Navigation 구조 동작 원리]
1. Frame: WPF에서 페이지를 호스팅할 수 있는 컨트롤.   
   - Navigate 메서드로 페이지 전환 처리.   
   - NavigationUIVisibility="Hidden"으로 기본 네비게이션 UI 숨김.   

2. Page: Frame 내부에 로드될 수 있는 콘텐츠.   
   - HomePage는 기본 페이지.   
   - DetailPage는 생성자에서 파라미터를 받아 출력.   

3. MainWindow → Frame → Page 전환 흐름:   
   - MainWindow 로드 시 기본적으로 HomePage 로드.   
   - "Go to Detail Page" 버튼 클릭 시 Frame이 DetailPage로 변경.   
   - DetailPage 생성자에서 전달된 파라미터를 TextBlock에 출력.   
   
