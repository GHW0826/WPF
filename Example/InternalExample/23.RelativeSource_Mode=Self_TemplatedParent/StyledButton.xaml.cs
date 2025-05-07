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

namespace RelativeSource_Mode_Self_TemplatedParent
{
    /// <summary>
    /// StyledButton.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class StyledButton : UserControl
    {
        public StyledButton()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty SelfColorProperty =
            DependencyProperty.Register(nameof(SelfColor), typeof(SolidColorBrush), typeof(StyledButton),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public SolidColorBrush SelfColor
        {
            get => (SolidColorBrush)GetValue(SelfColorProperty);
            set => SetValue(SelfColorProperty, value);
        }

        public static readonly DependencyProperty TemplateColorProperty =
            DependencyProperty.Register(nameof(TemplateColor), typeof(SolidColorBrush), typeof(StyledButton),
                new PropertyMetadata(new SolidColorBrush(Colors.Transparent)));

        public SolidColorBrush TemplateColor
        {
            get => (SolidColorBrush)GetValue(TemplateColorProperty);
            set => SetValue(TemplateColorProperty, value);
        }
    }
}
