using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace PriorityBinding.ViewModels
{
    public class FirstNonEmptyConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            foreach (var value in values)
            {
                if (value is string str && !string.IsNullOrWhiteSpace(str))
                    return str;
            }

            return string.Empty;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

    public class NotEmptyValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value is string str)
            {
                if (string.IsNullOrWhiteSpace(str))
                    return new ValidationResult(false, "값이 비어있습니다.");
            }
            return ValidationResult.ValidResult;
        }
    }

    public partial class PriorityBindingViewModel : ObservableObject
    {

        [ObservableProperty]
        private string? primaryText = null; // 일부러 Null로 시작

        [ObservableProperty]
        private string? secondaryText = "세컨더리 텍스트";

        [ObservableProperty]
        private string? fallbackText = "폴백 텍스트";

        public IRelayCommand MakePrimaryValidCommand { get; }
        public IRelayCommand MakePrimaryNullCommand { get; }

        public PriorityBindingViewModel()
        {
            MakePrimaryValidCommand = new RelayCommand(OnMakePrimaryValid);
            MakePrimaryNullCommand = new RelayCommand(OnMakePrimaryNull);
        }

        private void OnMakePrimaryValid()
        {
            PrimaryText = "프라이머리 텍스트!";
        }

        private void OnMakePrimaryNull()
        {
            PrimaryText = null;
        }

        ////////////////////////////////////////////////////////

        private string _fastDP;
        private string _slowerDP;
        private string _slowestDP;

        public string FastDP
        {
            get { return _fastDP; }
            set { _fastDP = value; }
        }

        public string SlowerDP
        {
            get
            {
                Thread.Sleep(3000);
                return _slowerDP;
            }
            set { _slowerDP = value; }
        }

        public string SlowestDP
        {
            get
            {
                Thread.Sleep(5000);
                return _slowestDP;
            }
            set { _slowestDP = value; }
        }
    }
}
