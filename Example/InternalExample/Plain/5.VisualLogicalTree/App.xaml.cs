using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace VisualLogicalTree
{
    /*
        Logical Tree: 논리적 관계 (컨테이너/자식 연결) 탐색
        Visual Tree: 실제 렌더링 관계 (View 렌더링 요소 연결) 탐색
        둘의 구조 차이 눈으로 직접 확인
    */
    /*
        WPF는 트리 기반 아키텍처로 구성.
        트리는 용도에 따라 "Logical Tree"와 "Visual Tree"로 나뉨.

        종류	        용도	                    주요 대상
        Logical Tree	데이터/구조 관계 표현	    ItemsControl, ContentControl, UserControl
        Visual Tree	    렌더링/UI 요소 표현	        Border, ScrollViewer, TextBox, Button 렌더링 구조

        [Logical Tree]
        Logical Tree는 사용자 UI 구조를 논리적으로 구성하는 트리.
        XAML 파일에서 선언한 "부모-자식" 관계를 나타냄.
        
        LogicalTree 특징
        컨트롤과 데이터 관계만 반영한다.
        레이아웃 최적화나 스타일 적용 시 사용된다.
        Binding의 DataContext 전파는 LogicalTree를 따라간다.


        [Visual Tree]
        Visual Tree는 WPF가 화면에 실제로 그릴 요소들을 관리하는 트리.
        ControlTemplate, Border, ContentPresenter 같은 시각적 요소까지 포함한다.
        
        VisualTree 특징
        실질적으로 렌더링되는 모든 요소가 들어간다.
        StackPanel 안에 Button이 있으면,
        Button 안에도 Border, ContentPresenter, TextBlock 등이 또 들어있다.
        이벤트 전파 (터널링/버블링)는 VisualTree를 따름.

        특징	        Logical Tree	        Visual Tree
        주 목적	        데이터 및 구조 관리	    실제 화면 렌더링 관리
        구성 요소	    컨트롤, 데이터 객체	    컨트롤 + 템플릿 내부 구조
        이벤트 전파	    일부 DataContext 전파	대부분 이벤트 터널링/버블링
        탐색 방법	    LogicalTreeHelper	    VisualTreeHelper

        [WPF 시스템에서 언제 어떤 트리를 쓰나]
        용도	                        사용하는 트리
        DataContext 전파	            Logical Tree
        스타일 적용 (StaticResource 등)	Logical Tree
        이벤트 전파 (터널링/버블링)	    Visual Tree
        Template 확장	                Visual Tree
        레이아웃/측정	                Visual Tree

        [ XAML 작성 ]
            ↓
        [ Logical Tree 구성 ]
            ↓
        [ ControlTemplate 적용 ]
            ↓
        [ Visual Tree 구성 (렌더링 구조) ]
    */
    public partial class App : Application
    {
    }
}
