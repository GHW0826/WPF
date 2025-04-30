using System;
using System.Collections.Generic;
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

namespace CustomControl
{
    public class MyFancyControl : Control
    {
        static MyFancyControl()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MyFancyControl), new FrameworkPropertyMetadata(typeof(MyFancyControl)));
        }

        public string FancyText
        {
            get => (string)GetValue(FancyTextProperty);
            set => SetValue(FancyTextProperty, value);
        }

        public static readonly DependencyProperty FancyTextProperty =
            DependencyProperty.Register(nameof(FancyText), typeof(string), typeof(MyFancyControl), new PropertyMetadata("Default Text"));
    }
}
