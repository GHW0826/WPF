using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace RelativeSourceFindAncestor
{
    /*
        DataTemplate 내부 또는 깊은 시각적 트리 안에서
        RelativeSource={RelativeSource FindAncestor}를 이용해
        상위 컨트롤의 속성에 바인딩하는 방식 실험

        ViewModel을 바꾸지 않고도 상위 ViewModel, UI 속성 접근 가능 확인.
    */
    /*
        [요약]
        RelativeSource={RelativeSource Mode=FindAncestor, AncestorType=...}
        이 바인딩은 현재 요소 기준으로 XAML 트리 상위에서 지정한 타입의 조상을 탐색해서 해당 요소의 속성에 바인딩하는 방식.

        [동작 흐름]
        현재 요소 내부 (예: DataTemplate 안) → 시각 트리 상위 탐색 →
        지정한 AncestorType과 일치하는 첫 번째 조상 찾기 → 그 조상의 속성에 바인딩

        [요약]
        ViewModel에 Title 속성이 있고, Window.DataContext.Title로 설정됨
        ItemsControl.ItemTemplate 내부에서:
        <TextBlock Text="{Binding DataContext.Title, RelativeSource={RelativeSource AncestorType=Window}}" />
        이 바인딩은 현재 템플릿 요소 기준으로 상위 Window를 찾고,
        그 Window.DataContext의 Title 속성을 가져와 출력함

        [주요 사용 예시]
        상황	                                        바인딩
        DataTemplate 내부에서 ViewModel 상위 참조	    AncestorType=Window or UserControl
        UserControl 내부에서 부모 컨트롤 속성 접근	    AncestorType=Grid, TabItem 등
        트리 내부에서 TreeViewItem → TreeView 접근	A   ncestorType=TreeView

        [구문 예시]
        <TextBlock Text="{Binding DataContext.Title, RelativeSource={RelativeSource AncestorType=Window}}" />
        
        요소	                설명
        AncestorType=Window	    조상 중 Window 타입을 찾음
        DataContext.Title	    찾은 조상의 DataContext에 바인딩


        [장점]
        항목	                설명
        MVVM 구조 유지	        ViewModel 간 직접 참조 없이 UI 상위 요소 접근 가능
        템플릿 재사용 가능	    템플릿 내부에서 ViewModel 상위 구조를 감지
        복잡한 UI 구조 대응	    중첩 컨트롤, ItemTemplate, ControlTemplate 등에서 유용

        [주의사항]
        항목	                    설명
        시각 트리 기준	            Visual Tree를 따라 탐색하므로 Logical Tree 기반 컨트롤과 혼동 주의
        AncestorType 정확해야 함	타입이 맞지 않으면 바인딩 실패 (값 없음)
        성능	                    트리 탐색이므로 과도한 중첩은 성능에 영향 가능성 있음

        [사용 포인트]
        상황	                                                이유
        DataTemplate 안에서 상위 ViewModel 정보가 필요할 때 	AncestorType=Window 사용
        CustomControl 내부에서 호스트 속성 참조	                AncestorType=UserControl
        복합 템플릿 구조에서 바인딩 경로가 꼬일 때	            명확한 조상 탐색으로 해결 가능
    */
    public partial class App : Application
    {
    }
}
