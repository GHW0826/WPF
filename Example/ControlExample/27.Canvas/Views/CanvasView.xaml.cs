using Canvas.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography.Xml;
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

namespace Canvas.Views
{
    public partial class CanvasView : UserControl
    {
        private Dictionary<CanvasItem, TranslateTransform> _transforms = new();

        public CanvasView()
        {
            InitializeComponent();
            Loaded += OnLoaded;
        }

        private void OnLoaded(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is ViewModels.CanvasViewModel vm)
            {
                vm.Items.CollectionChanged += OnItemsChanged;

                foreach (var item in vm.Items)
                {
                    AttachItem(item);
                }
            }
        }

        private void OnItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (CanvasItem item in e.NewItems)
                {
                    AttachItem(item);
                }
            }
        }

        private void AttachItem(CanvasItem item)
        {
            var button = new Button
            {
                Content = item.Title,
                Width = 100,
                Height = 50
            };

            var transform = new TranslateTransform
            {
                X = item.X,
                Y = item.Y
            };
            button.RenderTransform = transform;

            _transforms[item] = transform;

            CanvasArea.Children.Add(button);

            // **여기 중요**: PropertyChanged 직접 구독
            item.PropertyChanged += (s, e) =>
            {
                if (_transforms.TryGetValue(item, out var trans))
                {
                    if (e.PropertyName == nameof(CanvasItem.X))
                    {
                        trans.X = item.X;
                    }
                    else if (e.PropertyName == nameof(CanvasItem.Y))
                    {
                        trans.Y = item.Y;
                    }
                }
            };
        }
    }
}
