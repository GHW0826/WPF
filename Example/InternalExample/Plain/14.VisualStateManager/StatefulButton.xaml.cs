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

namespace VisualStateManager
{
    public partial class StatefulButton : UserControl
    {
        public StatefulButton()
        {
            InitializeComponent();

            this.MouseEnter += (s, e) => System.Windows.VisualStateManager.GoToState(this, "MouseOver", true);
            this.MouseLeave += (s, e) => System.Windows.VisualStateManager.GoToState(this, "Normal", true);
            this.MouseLeftButtonDown += (s, e) => System.Windows.VisualStateManager.GoToState(this, "Pressed", true);
            this.MouseLeftButtonUp += (s, e) => System.Windows.VisualStateManager.GoToState(this, "MouseOver", true);
        }

        public static readonly DependencyProperty ContentProperty =
            DependencyProperty.Register("Content", typeof(object), typeof(StatefulButton), new PropertyMetadata(null));

        public object Content
        {
            get => GetValue(ContentProperty);
            set => SetValue(ContentProperty, value);
        }
    }
}
