using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace DispatcherTest
{
    /*
        WPF 애플리케이션 전체에 단 하나의 UI 스레드가 존재.
        이 스레드가 모든 UI 요소 (컨트롤, 창, 텍스트, 그림 그리기 등)을 관리하고 렌더링.
        UI 스레드를 STAThread (Single Thread Apartment) 로 등록해서 시작.
        Main() 메서드 위에 [STAThread]가 이게 바로 UI 전용 단일 스레드를 만들겠다는 약속.
        
        WPF는 기본적으로
        UI 관련 모든 작업은 반드시 UI 스레드에서만 수행.
        다른 스레드에서 UI를 건드리고 싶으면 Dispatcher를 통해 요청해야 함.

        WPF의 스레드 구조
        스레드	                            역할
        UI Thread (Main Thread)	            모든 UI 그리기, 이벤트 처리, 데이터 바인딩
        ThreadPool / Background Threads	    데이터 로딩, 네트워크 통신, 파일 IO, 무거운 계산 처리 등
        Dispatcher	                        다른 스레드 → UI 스레드로 안전하게 작업 요청하는 중개자


        1. WPF는 오직 "UI 스레드"만이 UI를 직접 수정할 수 있다
        WPF (DispatcherObject 기반)는 생성된 STAThread(UI 메인 스레드)만이 UI 컨트롤 속성 변경을 허용한다.
        백그라운드 스레드가 직접 UI 속성 (TextBlock.Text, Button.Content 등)을 바꾸면 InvalidOperationException이 터진다.
        즉, UI는 오직 UI 스레드에서만 접근 가능.
        
        2. Dispatcher를 이용하면 백그라운드 스레드에서도 안전하게 UI를 수정할 수 있다
        Dispatcher.Invoke 또는 Dispatcher.BeginInvoke를 사용하면,
        백그라운드 작업 스레드가 UI 변경 요청을 메인(UI) 스레드에게 위임할 수 있다.
        여기선, Dispatcher.Invoke(() => { Message = "..." }); 호출로 UI 업데이트가 성공하는 걸 확인 가능.
        
        3. ThreadPool 스레드 → Dispatcher.Invoke 흐름을 눈으로 확인.
        버튼을 누르면 ThreadPool 스레드에서 작업이 시작된다.
        하지만 결과를 UI에 반영할 때는 Dispatcher를 통해 메인 스레드로 다시 올라온다.
        
        4. Plain MVVM에서는 Dispatcher를 직접 호출해야 한다는 것 체험
        (Prism, WinUI, MAUI 같은 현대적인 프레임워크에서는) 보통 IDispatcherService 같은 걸 주입해서 씀.
        그런데 Plain WPF에서는 저런 서비스가 없기 때문에,    
        직접 Application.Current.Dispatcher.Invoke(...) 같은 코드로 제어해야 하는 걸 알 수 있다.
        Dispatcher 사용 위치를 ViewModel 내부에 둬야 하는지, Service로 따로 빼야 하는지 고민 포인트도 생김.

    */
    public partial class App : Application
    {
    }
}
