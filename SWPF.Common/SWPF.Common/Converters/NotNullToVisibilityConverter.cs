using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace SWPF.Common.Converters
{
    /*
        객체 존재 여부로 Visibility 변환    리스트/데이터가 있는지 여부
    */
    public class NotNullToVisibilityConverter : IValueConverter
    {
        public bool CollapseWhenNull { get; set; } = false;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool isNotNull = value != null;
            return isNotNull ? Visibility.Visible : (CollapseWhenNull ? Visibility.Collapsed : Visibility.Hidden);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
