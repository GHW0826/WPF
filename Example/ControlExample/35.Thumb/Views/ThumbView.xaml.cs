using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Thumb.ViewModels;

namespace Thumb.Views
{
    /// <summary>
    /// ThumbView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ThumbView : UserControl
    {
        public ThumbView()
        {
            InitializeComponent();
        }

        private void Thumb_DragDelta(object sender, DragDeltaEventArgs e)
        {
            if (DataContext is ThumbViewModel vm)
            {
                vm.X += e.HorizontalChange;
                vm.Y += e.VerticalChange;
            }
        }
    }
}
