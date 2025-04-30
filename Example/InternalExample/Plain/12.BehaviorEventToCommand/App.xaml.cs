using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace BehaviorEventToCommand
{
    /*
        Button 클릭 이벤트를 ViewModel의 ICommand로 직접 연결 (code-behind 없이)
        Microsoft.Xaml.Behaviors.Wpf NuGet 패키지 활용
        Behavior 시스템 기반 EventToCommand 흐름 이해
    */
    /*
        동작
        1. Button에 Click 이벤트 직접 핸들러 없음
        2. Interaction.Trigger → EventTrigger → InvokeCommandAction 흐름 적용
        3. ViewModel의 ButtonClickCommand 실행됨
        4. TextBlock에 실행 결과 출력됨

        [실험 흐름]
        <Button>
           ↳ Interaction.Triggers
              ↳ EventTrigger("Click")
                 ↳ InvokeCommandAction
                     ↳ ViewModel.ButtonClickCommand.Execute()
        

        [Behavior 용도]
        MVVM 패턴에서 ViewModel은 View의 이벤트를 몰라야 한다
        그런데 Button.Click, MouseEnter 같은 UI 이벤트는 View 쪽에만 존재

        <Button Click="OnClick"/> // ❌ code-behind에 종속됨
        Behavior + EventToCommand = View 이벤트를 ViewModel의 ICommand로 연결


        [Behavior 시스템]
        WPF에선 Microsoft.Xaml.Behaviors.Wpf 패키지를 사용해 View에 붙는 이벤트-기반 동작(Behavior)을 구성할 수 있다.
        
        핵심 클래스
        Interaction.Triggers: 동작 바인딩 컨테이너
        EventTrigger: 특정 이벤트 발생 시 트리거
        InvokeCommandAction: 특정 ICommand를 실행

        
        [EventToCommand 흐름 구조]
        <Button>
          <i:Interaction.Triggers>
            <i:EventTrigger EventName="Click">
              <i:InvokeCommandAction Command="{Binding SomeCommand}" />
            </i:EventTrigger>
          </i:Interaction.Triggers>
        </Button>
        
        Click 발생 →
        EventTrigger 감지 →
        InvokeCommandAction 실행 →
        Command.Execute() 호출


        [InvokeCommandAction]
        public class InvokeCommandAction : TriggerAction<DependencyObject>
        {
            public ICommand Command { get; set; }
            protected override void Invoke(object parameter)
            {
                if (Command?.CanExecute(parameter) == true)
                    Command.Execute(parameter);
            }
        }

        내부적으로 Command.Execute(...)를 호출해준다
        Parameter도 전달 가능 (CommandParameter 속성 있음)

        
        [이 구조의 장점]
        장점	                                    설명
        ViewModel에서 View 이벤트 알 필요 없음	    MVVM 원칙 지킴
        XAML만으로 UI 이벤트 연결	                코드비하인드 없음
        Command 패턴 그대로 활용	                CanExecute/Execute 로직도 그대로 적용 가능
        Parameter 전달, 다양한 이벤트 대응 가능	    MouseEnter, Loaded, Drop 등 확장 쉬움


        [복습 정리]
        요소	                            설명
        Interaction.Triggers	            이벤트를 잡아낼 컨테이너
        EventTrigger EventName="Click"	    대상 이벤트 지정
        InvokeCommandAction	                ICommand 실행 트리거
        ViewModel.ButtonClickCommand	    RelayCommand로 연결된 로직 수행
        Output TextBlock	                ViewModel의 Output 속성 바인딩 결과 출력

        
        [요약 구조도]
        [ Button (View) ]
           └ Interaction.Triggers
              └ EventTrigger ("Click")
                 └ InvokeCommandAction → ViewModel.Command.Execute()
    */
    public partial class App : Application
    {
    }
}
