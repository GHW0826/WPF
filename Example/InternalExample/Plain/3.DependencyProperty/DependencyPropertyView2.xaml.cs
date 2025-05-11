using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DependencyProperty
{
    /// <summary>
    /// DependencyPropertyTestView2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class DependencyPropertyView2 : UserControl
    {
        public DependencyPropertyView2()
        {
            InitializeComponent();
        }

        // 📌 ① SampleText DependencyProperty 등록
        public static readonly System.Windows.DependencyProperty SampleTextProperty =
            System.Windows.DependencyProperty.Register(
                nameof(SampleText),                            // 프로퍼티 이름
                typeof(string),                                // 타입
                typeof(DependencyPropertyView2),               // 소유자 타입
                new PropertyMetadata("", OnSampleTextChanged)  // 기본값 및 콜백
            );

        // 📌 ② CLR Wrapper
        public string SampleText
        {
            get => (string)GetValue(SampleTextProperty);
            set => SetValue(SampleTextProperty, value);
        }

        // 📌 ③ PropertyChangedCallback
        private static void OnSampleTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var control = d as DependencyPropertyView2;
            var newValue = e.NewValue as string;
            Debug.WriteLine($"[DP] SampleText changed: {newValue}");
        }
    }
}
