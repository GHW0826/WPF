using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Messenger_IEventAggregator
{

    /*
        뷰 간, ViewModel 간 직접 참조 없이 메시지를 전달
        ① Messenger 패턴 (간단한 Action 중계 기반)
        ② Prism의 IEventAggregator (Pub/Sub 구조)
        → 두 구조를 비교 실험하며 동작 흐름 파악
    */

    /*
        ViewModel ↔ ViewModel 간에
        직접 참조 없이 느슨하게 메시지를 전달할 수 있는 구조
        
        
        [구성 요소]
        구성요소	            설명
        Messenger (싱글턴)	    메시지를 보내고(subscribe/send) 구독자를 관리하는 중심
        Register<T>()	        특정 타입의 메시지를 받을 콜백을 등록
        Send(message)	        등록된 모든 콜백에 메시지를 전달
        ViewModel	            메시지 전송자(Sender) 또는 수신자(Receiver)가 될 수 있음
        
        
        [동작 흐름]
        1. ReceiverViewModel에서 Messenger.Register<string>() 호출
        2. MainViewModel에서 Messenger.Send(message) 호출
        3. Messenger는 모든 string 타입 구독자에게 메시지 전달
        4. ReceiverViewModel의 콜백이 실행되고 값 변경됨
        5. UI에 반영됨 (INotifyPropertyChanged)

        [요약]
        텍스트 입력 → 버튼 클릭 → Messenger.Send(string) 실행
        구독자(ReceiverViewModel)에서 메시지 수신 → Message 속성 갱신
        바인딩된 TextBlock에 변경 내용 반영됨
        
        
        [장점]
        항목	        설명
        느슨한 결합	    ViewModel 간에 서로 몰라도 통신 가능
        구현이 간단	    1~2개의 클래스만으로 구성 가능
        실시간 전파	    이벤트 없이도 전달 즉시 실행됨

        [단점 / 주의점]
        항목	                                    설명
        모든 구독자가 동일 타입 콜백을 다 받음	    메시지 분기 로직이 없으면 오염 위험
        메모리 누수 위험	                        구독 해제를 안 하면 GC 대상이 안 됨 (강한 참조 유지됨)
        구조 커질수록 한계	                        메타 정보 없음 (누가 보냈는지, 어디서 받는지 모름)
        스레드 안전성 없음	                        기본 구조는 멀티스레드 환경에서는 unsafe함


        [사용 전략]    
        상황	                            적합 여부
        ViewModel 간 간단한 메시지 전달	    매우 적합
        이벤트 UI ↔ ViewModel 전달	        가능 (커맨드 대신 쓸 수 있음)
        대규모 Pub/Sub 구조	                Prism IEventAggregator가 더 나음
        타입별 분기/필터링	                확장 구조 필요 (메시지 타입 기반 Dictionary 등)


        [요약2]
        Messenger 패턴은 간단하고 빠른 메시지 전달용 구조로서
        ViewModel 간 참조를 제거하고, 작은 규모 또는 학습용 프로젝트에서 유용.
        실전에서는 메모리 관리, 타입 분기, 스레드 안정성을 확장하거나
        Prism의 IEventAggregator 등으로 넘어가는 게 일반적.
    */
    public partial class App : Application
    {
    }
}
