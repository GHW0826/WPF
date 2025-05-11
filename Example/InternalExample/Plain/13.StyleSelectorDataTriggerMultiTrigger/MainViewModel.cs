using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace StyleSelectorDataTriggerMultiTrigger
{
    public class StatusModel
    {
        public bool IsActive { get; set; }
        public string Level { get; set; } // 예: "Low", "High"
    }

    public class MainViewModel
    {
        public StatusModel Item { get; } = new StatusModel
        {
            IsActive = true,
            Level = "High"
        };
    }

    public class StatusStyleSelector : StyleSelector
    {
        public Style DefaultStyle { get; set; }
        public Style HighlightedStyle { get; set; }

        public override Style SelectStyle(object item, DependencyObject container)
        {
            if (item is StatusModel model && model.IsActive && model.Level == "High")
                return HighlightedStyle;
            return DefaultStyle;
        }

        // Optional 키 사용 시 XAML에서 DynamicResource로도 활용 가능
        public static readonly ComponentResourceKey SelectorKey = new ComponentResourceKey(typeof(StatusStyleSelector), "SelectorKey");
    }
}
