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

namespace MeasureArrange
{

    public class SimpleLayoutPanel : Panel
    {
        protected override Size MeasureOverride(Size availableSize)
        {
            Debug.WriteLine($"[MeasureOverride] Panel AvailableSize: {availableSize}");

            foreach (UIElement child in InternalChildren)
            {
                child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
                Debug.WriteLine($"[MeasureOverride] Measuring Child: {child.DesiredSize}");
            }

            // 패널의 크기는 자식들의 최대 크기로 가정
            return new Size(availableSize.Width, availableSize.Height);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            Debug.WriteLine($"[ArrangeOverride] Panel FinalSize: {finalSize}");

            double offsetY = 0;
            foreach (UIElement child in InternalChildren)
            {
                child.Arrange(new Rect(10, offsetY + 10, child.DesiredSize.Width, child.DesiredSize.Height));
                Debug.WriteLine($"[ArrangeOverride] Arranging Child at (10, {offsetY + 10}) size {child.DesiredSize}");
                offsetY += child.DesiredSize.Height + 10;
            }

            return finalSize;
        }
    }
}
