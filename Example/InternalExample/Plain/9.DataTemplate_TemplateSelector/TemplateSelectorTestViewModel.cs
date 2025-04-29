using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace DataTemplate_TemplateSelector
{
    public class MyTemplateSelector : DataTemplateSelector
    {
        public DataTemplate DefaultTemplate { get; set; }
        public DataTemplate HighlightedTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is ItemModel model)
            {
                return model.IsHighlighted ? HighlightedTemplate : DefaultTemplate;
            }
            return base.SelectTemplate(item, container);
        }
    }

    public class ItemModel
    {
        public string Name { get; set; }
        public bool IsHighlighted { get; set; }
    }

    public class TemplateSelectorTestViewModel
    {
        public ObservableCollection<ItemModel> Items { get; } = new ObservableCollection<ItemModel>
        {
            new ItemModel { Name = "Normal A", IsHighlighted = false },
            new ItemModel { Name = "Important B", IsHighlighted = true },
            new ItemModel { Name = "Normal C", IsHighlighted = false },
            new ItemModel { Name = "🔥 Critical D", IsHighlighted = true },
        };
    }
}
