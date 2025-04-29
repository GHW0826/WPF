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
        string.IsNullOrEmpty → Visibility 변환    텍스트 없을 때 메시지 표시
    */
    public class StringIsNullOrEmptyToVisibilityConverter : IValueConverter
    {
        public bool Invert { get; set; } = false;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var str = value as string;
            bool isEmpty = string.IsNullOrEmpty(str);
            if (Invert)
                isEmpty = !isEmpty;

            return isEmpty ? Visibility.Collapsed : Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
