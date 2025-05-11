using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DataTemplate_TemplateSelector
{
    /*
        [DataTemplate]
        DataTemplate은 특정 데이터 타입에 어떤 UI를 보여줄지 정의한 XAML 템플릿
        ViewModel이나 POCO 객체 같은 데이터 객체를 시각적으로 표현할 때 사용됨
        대부분 ItemsControl, ListBox, ComboBox, ContentPresenter 등에서 자동으로 사용됨

        데이터는 단순하지만, 보여주는 방식은 다르게 하고 싶을 때 → DataTemplate
        
        <DataTemplate DataType="{x:Type local:ItemModel}">
            <TextBlock Text="{Binding Name}" Foreground="Green"/>
        </DataTemplate>
        → ItemModel 타입 데이터가 오면, 이 템플릿으로 그린다.

        
        [DataTemplate 사용 위치]
        사용 위치	                설명
        ItemTemplate	            ItemsControl, ListBox 등에서 사용
        ContentTemplate	            ContentControl, ContentPresenter 등에서 사용
        DataType (암시적 스타일)	특정 타입에 자동 매핑 (ex: DataTemplate.DataType)

        
        [TemplateSelector]
        TemplateSelector는 조건에 따라 다른 DataTemplate을 선택할 수 있는 전략 클래스.
        DataTemplateSelector를 상속해서 SelectTemplate() 메서드를 구현
        하나의 ItemsControl 안에서도 데이터에 따라 다른 템플릿을 적용할 수 있음

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is ItemModel m && m.IsHighlighted)
                return HighlightedTemplate;
            return DefaultTemplate;
        }
        <ItemsControl ItemsSource="{Binding Items}" ItemTemplateSelector="{StaticResource MySelector}" />
        

        [DataTemplateSelector 사용 구조]
        [ ItemsControl ]
            ↳ ItemSource: List<ItemModel>
            ↳ ItemTemplateSelector → SelectTemplate(model) 호출
                ↳ if (model.IsHighlighted) → HighlightedTemplate
                ↳ else → DefaultTemplate
        
        
        [DataTemplate vs TemplateSelector 차이]
        항목	    DataTemplate	    TemplateSelector
        역할	    하나의 템플릿 정의	여러 템플릿 중 조건 분기
        적용 방식	직접 XAML에 지정	Selector 통해 조건부 반환
        용도	    단순한 UI 반복	    조건부 UI 분기 (ex: 중요도 표시 등)
    */
    public partial class App : Application
    {
    }
}
