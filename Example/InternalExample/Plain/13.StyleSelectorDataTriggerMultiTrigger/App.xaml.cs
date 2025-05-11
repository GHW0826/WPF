using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace StyleSelectorDataTriggerMultiTrigger
{

    /*
        IsActive와 Level 속성에 따라 TextBlock 색상/폰트 변경
        StyleSelector로 객체 타입/속성에 따라 다른 스타일 적용
        DataTrigger/MultiTrigger로 속성 조건별 Setter 적용

        View 또는 ViewModel의 상태에 따라 UI 스타일을 동적으로 바꾸기 위한 구조
    */
    /*
        
        StyleSelector	객체 상태에 따라 동적으로 스타일 선택
        DataTrigger	    IsActive == true일 때 스타일 적용
        MultiTrigger	IsActive == true && Level == High일 때 스타일 적용

        [흐름 정리]
        [ ViewModel ]
           → Item { IsActive = true, Level = "High" }

        [ View (XAML) ]
           → StyleSelector 적용 → 강조 스타일
           → DataTrigger 적용 → Green 색상
           → MultiTrigger 적용 → Blue 색상 + Italic


        [목적]
        StyleSelector: 데이터 조건에 따라 다른 Style 적용
        DataTrigger: 단일 바인딩 조건으로 Setter 발동
        MultiDataTrigger: 여러 바인딩 조건을 만족해야 Setter 발동

        
        [작동 흐름]
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
    */
    /*
        
        [StyleSelector]
        항목	        내용
        용도	        한 객체 또는 항목에 대해 조건 분기하여 서로 다른 Style 객체 자체를 선택
        대상	        ItemsControl.ItemContainerStyleSelector, ContentPresenter.ContentTemplateSelector 등
        작성 위치	    C# 클래스에서 StyleSelector 상속 → SelectStyle() 오버라이드
        적용 방식	    ItemContainerStyleSelector="{StaticResource MyStyleSelector}"
        특징	        완전히 다른 스타일 객체를 반환할 수 있어 매우 유연
        제한점	        TextBlock 등 대부분 컨트롤에는 직접 사용 불가 → 반드시 연결 가능한 컨트롤에서만 사용 가능


        [DataTrigger]
        항목	        내용
        용도	        ViewModel의 속성 하나의 값이 특정 값과 일치할 때 스타일 Setter 발동
        대상	        Style, ControlTemplate, DataTemplate 내에서 사용 가능
        작성 위치	    XAML 내부에서 Style.Triggers에 직접 정의
        적용 방식	    <DataTrigger Binding="{Binding IsActive}" Value="True">
        특징	        가장 많이 쓰이는 단일 조건 기반 트리거
        제한점	        하나의 조건만 검사 가능

        
        [MultiDataTrigger]
        항목	        내용
        용도	        ViewModel의 여러 속성 값을 복합 조건으로 검사하여 스타일 Setter 발동
        대상	        Style 내부에서 사용 가능
        작성 위치	    XAML에서 Style.Triggers 내부
        적용 방식	    <MultiDataTrigger> + <Condition Binding=...> 다수
        특징	        복합 조건 분기에 적합 (AND 조건)
        제한점	        OR 조건은 불가능, 반드시 모든 조건 만족 시 발동

        
        [비교 요약]
        항목	    StyleSelector	                        DataTrigger	                MultiDataTrigger
        조건 위치	C# 코드	                                XAML	                    XAML
        조건 개수	자유	                                1개	                        2개 이상 (AND)
        제어 범위	스타일 전체를 분기	                    스타일 일부 속성 Setter	    스타일 일부 속성 Setter
        재사용성	매우 유연 (Style 객체 자체 교체)	    비교적 간단	                복합 분기 가능
        적용 위치	ItemsControl, ContentControl 등	        Style, ControlTemplate 등	Style 내부
        장점	완전한 스타일 전환 가능	                    간단하고 직관적	            여러 조건 통합 가능
        단점	XAML만으로는 불가	                        조건 1개만 가능	            조건이 늘어날수록 복잡
    */
    public partial class App : Application
    {
    }
}
