using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ThemeDictionary
{

    /*
        여러 ThemeDictionary(예: Light.xaml, Dark.xaml)를 준비하고
        런타임에서 App.xaml의 ResourceDictionary를 교체하여 전체 UI 색상 테마를 전환
        DynamicResource를 통해 실시간 반영 확인
    */

    /*
        ThemeDictionary는 Application.Resources의 MergedDictionaries 안에
        다양한 테마 리소스 (예: Light.xaml, Dark.xaml) 를
        동적으로 전환할 수 있도록 구성하는 구조

        [구조 구성 요소]
        구성 요소	                                                        설명
        ResourceDictionary	                                                리소스를 묶는 단위 (색상, 스타일, 브러시 등)
        MergedDictionaries	                                                여러 Dictionary를 하나로 합침
        DynamicResource	                                                    런타임에서도 변경 감지 가능하게 연결
        Application.Current.Resources.MergedDictionaries.Clear().Add()	    리소스 전환 로직의 핵심


        [흐름 요약]
        1. Light.xaml, Dark.xaml에 동일한 키(PrimaryBrush, TextBrush)로 리소스 정의
        2. MainWindow에서 DynamicResource를 사용해 이 키들을 바인딩
        3. 버튼 클릭 시 Application.Current.Resources.MergedDictionaries를 교체
        4. DynamicResource이기 때문에 UI가 즉시 변경됨

        [동작 원리]
        [ XAML에서 DynamicResource "PrimaryBrush" 요청 ]
                ↓
        [ ResourceDictionary에서 "PrimaryBrush" 경로 추적 ]
                ↓
        [ 리소스 교체 시 → 경로 동일하므로 다시 평가됨 ]
                ↓
        [ 브러시 인스턴스 변경 → 바인딩된 컨트롤에 즉시 반영 ]


        키 이름	                                    설명
        PrimaryBrush	                            버튼 배경색, Border 배경 등
        TextBrush	                                기본 텍스트 색
        ErrorBrush	                                경고 색 (적색 계열 등)
        ControlForeground, ControlBackground	    사용자 정의 컨트롤 스타일에 사용 가능


        [주의 사항]
        주의 항목	                설명
        StaticResource 사용 시	    테마 전환 효과 없음 (변경 감지 안 됨)
        Style 키 충돌	            각 테마에 동일한 스타일 키 유지 필수
        리소스 없는 키 참조	        DynamicResource는 실행 시 오류 발생 안 하지만 화면 미출력됨
        MergedDictionaries 순서	    중복 키가 있을 경우, 마지막에 추가된 것이 우선됨
    */
    public partial class App : Application
    {
    }
}
