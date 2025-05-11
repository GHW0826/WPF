using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace MultiBinding.ViewModels
{
    public class NameConcatConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            // FirstName과 LastName이 순서대로 들어온다
            string firstName = values[0] as string ?? string.Empty;
            string lastName = values[1] as string ?? string.Empty;

            if (string.IsNullOrWhiteSpace(firstName) && string.IsNullOrWhiteSpace(lastName))
                return "이름을 입력하세요";

            return $"{lastName} {firstName}".Trim();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            // TextBlock에 표시된 값에서 성, 이름을 분리하는 것은 어려우니까
            // 여기서는 그냥 FullName만 반환하자
            string fullName = value as string ?? string.Empty;
            return new object[] { Binding.DoNothing, Binding.DoNothing, fullName };
        }
    }

    public partial class MultiBindingViewModel : ObservableObject
    {
        [ObservableProperty]
        private string firstName;

        [ObservableProperty]
        private string lastName;

        [ObservableProperty]
        private string fullName;

        partial void OnFirstNameChanged(string value)
        {
            UpdateFullName();
        }

        partial void OnLastNameChanged(string value)
        {
            UpdateFullName();
        }

        private void UpdateFullName()
        {
            if (string.IsNullOrWhiteSpace(FirstName) && string.IsNullOrWhiteSpace(LastName))
                FullName = string.Empty;
            else
                FullName = $"{LastName} {FirstName}".Trim();
        }
    }
}
