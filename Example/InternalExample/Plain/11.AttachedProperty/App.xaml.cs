using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AttachedProperty
{

    /*
        AttachedProperty의 정의와 적용 구조를 실험.
        TextBoxHelper.IsWatermarked 같은 외부 확장 속성 구조를 체험.
        ViewModel 없이 View만으로 동작 구조 이해
    */
    /*
    [흐름]
    동작	                                                설명
    TextBox에 helper:FocusHelper.IsFocused="True" 설정	    AttachedProperty 적용
    OnIsFocusedChanged 실행	                                TextBox.Focus() 실행
    결과	                                                실행 시 자동 포커스가 설정됨

    MainWindow.xaml
       ↳ TextBox에 helper:FocusHelper.IsFocused="True"
           ↳ FocusHelper.SetValue → OnIsFocusedChanged
               ↳ TextBox.Focus() 실행


    [AttachedProperty]
    AttachedProperty는 다른 클래스가 정의한 DependencyProperty를, 다른 객체에 부착해서 사용할 수 있게 해주는 구조.
    원래 TextBox.IsFocused 같은 속성이 없다면 FocusHelper.IsFocused="True" 같이 외부에서 속성을 붙이는 방식으로 확장.


    [일반 DependencyProperty vs AttachedProperty]
    항목	        DependencyProperty	                AttachedProperty
    정의 위치	    그 속성을 가진 타입 안에서 정의	    외부 Helper 클래스에서 정의
    사용 대상	    자기 자신	                        다른 컨트롤에 부착 가능
    사용 예	        Button.Content	                    Grid.Row, Canvas.Left, ScrollViewer.HorizontalScrollBarVisibility


    [구조 예시]
    public static readonly DependencyProperty IsFocusedProperty =
    DependencyProperty.RegisterAttached(
        "IsFocused",
        typeof(bool),
        typeof(FocusHelper),
        new PropertyMetadata(false, OnIsFocusedChanged));

    요소	            설명
    RegisterAttached	부착 속성 등록 메서드
    IsFocused	        속성 이름 (XAML에서 쓰는 이름)
    FocusHelper	        소유자 클래스
    PropertyMetadata	기본값 및 변경 콜백 등록


    [예제 필수 메서드 2개]
    public static bool GetIsFocused(DependencyObject obj)
    {
        return (bool)obj.GetValue(IsFocusedProperty);
    }

    public static void SetIsFocused(DependencyObject obj, bool value)
    {
        obj.SetValue(IsFocusedProperty, value);
    }
    XAML 파서가 helper:FocusHelper.IsFocused="True"를 만나면:
    SetIsFocused() 호출 → 값 저장
    내부적으로 OnIsFocusedChanged() 호출 → 로직 실행


    [실험 흐름 복습]
    XAML: <TextBox helper:FocusHelper.IsFocused="True"/>
          ↓
    파서가 읽음 → SetIsFocused 호출
          ↓
    값이 바뀌었으므로 OnIsFocusedChanged 호출
          ↓
    TextBox.Focus() 실행 → 포커스 부여


    [요약]
    [ FocusHelper.cs ]
       └ RegisterAttached("IsFocused")
       └ Set/GetIsFocused()

    [ MainWindow.xaml ]
       └ <TextBox helper:FocusHelper.IsFocused="True"/>

    [ 실행 시 ]
       └ SetIsFocused → OnIsFocusedChanged → TextBox.Focus()

    */
    public partial class App : Application
    {
    }
}
