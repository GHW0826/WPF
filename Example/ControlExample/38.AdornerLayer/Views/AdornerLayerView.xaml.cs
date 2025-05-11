using AdornerLayer.ViewModels;
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

namespace AdornerLayer.Views
{
    public class SimpleBorderAdorner : Adorner
    {
        private static readonly SolidColorBrush BackgroundBrush = new SolidColorBrush(Color.FromArgb(64, 0, 120, 215)); // 반투명 파랑
        private static readonly Pen BorderPen;

        static SimpleBorderAdorner()
        {
            BorderPen = new Pen(Brushes.Red, 2);
            BorderPen.DashStyle = DashStyles.Dash; // 점선
            BorderPen.Freeze();
        }

        public SimpleBorderAdorner(UIElement adornedElement)
            : base(adornedElement)
        {
            IsHitTestVisible = false;
        }

        protected override void OnRender(DrawingContext drawingContext)
        {
            base.OnRender(drawingContext);

            Rect rect = new Rect(this.AdornedElement.RenderSize);

            // 반투명 배경
            drawingContext.DrawRectangle(BackgroundBrush, null, rect);

            // 점선 테두리
            drawingContext.DrawRectangle(null, BorderPen, rect);
        }
    }

    public partial class AdornerLayerView : UserControl
    {

        private System.Windows.Documents.AdornerLayer? _adornerLayer;
        private SimpleBorderAdorner? _adorner;

        public AdornerLayerView()
        {
            InitializeComponent();
            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is AdornerLayerViewModel vm)
            {
                _adornerLayer = System.Windows.Documents.AdornerLayer.GetAdornerLayer(targetButton);

                if (_adornerLayer != null)
                {
                    vm.PropertyChanged += (s, args) =>
                    {
                        if (args.PropertyName == nameof(vm.IsAdornerVisible))
                        {
                            UpdateAdorner(vm.IsAdornerVisible);
                        }
                    };

                    // 초기 상태 반영
                    UpdateAdorner(vm.IsAdornerVisible);
                }
            }
        }

        private void UpdateAdorner(bool show)
        {
            if (_adornerLayer == null)
                return;

            if (show)
            {
                if (_adorner == null)
                {
                    _adorner = new SimpleBorderAdorner(targetButton);
                    _adornerLayer.Add(_adorner);
                }
            }
            else
            {
                if (_adorner != null)
                {
                    _adornerLayer.Remove(_adorner);
                    _adorner = null;
                }
            }
        }
    }
}
