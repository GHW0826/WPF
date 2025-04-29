using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Command
{
    /*
        [Command란 무엇인가?]
        WPF에서 Command는 사용자의 입력(버튼 클릭, 키보드 입력 등)을 논리적 작업(액션)으로 연결하는 패턴.
        입력과 동작을 분리하고 UI와 비즈니스 로직을 느슨하게 연결할 수 있게 해준다.
        → MVVM 패턴의 핵심 연결고리

        [Command의 핵심 인터페이스: ICommand]
        public interface ICommand {
            bool CanExecute(object parameter);
            void Execute(object parameter);
            event EventHandler CanExecuteChanged;
        }

        메서드/이벤트	    역할
        Execute	            명령을 실행한다 (버튼 누르거나 키 입력 시)
        CanExecute	        명령을 실행할 수 있는지 확인 (버튼 활성화/비활성화 제어)
        CanExecuteChanged	CanExecute 값이 변했을 때 UI 갱신을 알림

        [Command 흐름]
        1. 사용자가 버튼을 클릭
        2. WPF가 Button 클릭 감지
        3. Button의 Command를 읽음
        4. Command.CanExecute(parameter)를 호출
            false면 버튼 비활성화
        5. Command.Execute(parameter)를 호출해서 실제 동작 수행 → 항상 CanExecute를 먼저 체크한 뒤 Execute가 호출된다.


        [RelayCommand 패턴]
        매번 ICommand 직접 구현하면 코드가 길고 귀찮다.

        해결: RelayCommand 같은 커스텀 클래스 하나 만들면,
        Action/ Func<bool> 만 넘겨서 쉽게 ICommand를 쓸 수 있다.
        
        Button → RelayCommand → ViewModel 메서드 실행

        [KeyBinding과 Command 연결]
        버튼뿐 아니라 키보드 입력(예: Enter 키 누르기)도 Command로 연결 가능
        <Window.InputBindings>
            <KeyBinding Key="Enter" Command="{Binding TestCommand}" />
        </Window.InputBindings>
        Enter 키를 누르면 TestCommand가 실행 → RoutedCommand, InputBinding 시스템까지 자연스럽게 연결
    */
    public partial class App : Application
    {
    }
}
