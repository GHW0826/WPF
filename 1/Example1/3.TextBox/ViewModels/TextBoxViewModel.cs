using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prism.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Resources;
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
using TextBox.Resources;

namespace TextBox.ViewModels
{
    public class EmptyStringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string input = value as string;
            return string.IsNullOrEmpty(input) ? Visibility.Visible : Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public partial class TextBoxViewModel : ObservableValidator
    {
        [ObservableProperty]
        [NotifyDataErrorInfo]
        [Required(ErrorMessage = "값을 입력해주세요.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "숫자만 입력해주세요.")]
        private string userInput = null;

        //////////////////////////////////////////////////////////////////

        [ObservableProperty]
        private bool isReadOnly;

        //////////////////////////////////////////////////////////////////
        public IRelayCommand SubmitCommand { get; }

        public TextBoxViewModel()
        {
            SubmitCommand = new RelayCommand(OnSubmit, CanSubmit);

            SetCulture("ko");
        }
        partial void OnUserInputChanged(string value)
        {
            ValidateAllProperties();
            SubmitCommand.NotifyCanExecuteChanged();
        }

        private void OnSubmit()
        {
            System.Windows.MessageBox.Show($"제출된 값: {UserInput}");
        }

        private bool CanSubmit()
        {
            return !HasErrors;
        }

        //////////////////////////////////////////////////////////////////

        private readonly ResourceManager _rm = Strings.ResourceManager;
        
        private CultureInfo currentCulture = CultureInfo.CurrentUICulture;

        [ObservableProperty]
        private string langInput = "";
        
        [ObservableProperty]
        private string inputHint;

        [ObservableProperty]
        public string lengthDisplay;


        partial void OnLangInputChanged(string value)
        {
            OnPropertyChanged(nameof(LengthDisplay)); // 길이 메시지 갱신
            UpdateLengthDisplay();
        }

        public void SetCulture(string cultureName)
        {
            currentCulture = new CultureInfo(cultureName);
            InputHint = _rm.GetString(nameof(InputHint), currentCulture);
            UpdateLengthDisplay();
        }
        private void UpdateLengthDisplay()
        {
            LengthDisplay = string.Format(
                _rm.GetString(nameof(LengthDisplay), currentCulture),
                LangInput?.Length ?? 0
            );
        }
    }
}
