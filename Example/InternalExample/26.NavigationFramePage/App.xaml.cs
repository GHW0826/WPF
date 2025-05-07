using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NavigationFramePage
{
    /*
        Frame에서 페이지 전환 동적 처리.
        페이지 간 파라미터 전달 확인.
        MVVM 구조로 Frame + Page 관리.
    */
    /*
        [Navigation 구조 동작 원리]
        1️.Frame: WPF에서 페이지를 호스팅할 수 있는 컨트롤.
        Navigate 메서드로 페이지 전환 처리.
        NavigationUIVisibility="Hidden"으로 기본 네비게이션 UI 숨김.

        2️.Page: Frame 내부에 로드될 수 있는 콘텐츠.
        HomePage는 기본 페이지.
        DetailPage는 생성자에서 파라미터를 받아 출력.

        3️.MainWindow → Frame → Page 전환 흐름:
        MainWindow 로드 시 기본적으로 HomePage 로드.
        "Go to Detail Page" 버튼 클릭 시 Frame이 DetailPage로 변경.
        DetailPage 생성자에서 전달된 파라미터를 TextBlock에 출력.
    */
    public partial class App : Application
    {
    }
}
