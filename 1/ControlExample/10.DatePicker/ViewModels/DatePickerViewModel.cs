using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

namespace DatePicker.ViewModels
{
    public partial class DatePickerViewModel : ObservableObject
    {
        [ObservableProperty]
        private DateTime? birthDate = DateTime.Today;

        [ObservableProperty]
        private string ageText;

        [ObservableProperty]
        private string validationMessage = "생년월일을 선택해주세요.";
        public IRelayCommand ConfirmCommand { get; }

        public DatePickerViewModel()
        {
            birthDate = DateTime.Today;
            ConfirmCommand = new RelayCommand(OnConfirm, CanConfirm);
            OnBirthDateChanged(birthDate);
        }


        partial void OnBirthDateChanged(DateTime? value)
        {
            if (value is null)
            {
                ValidationMessage = "생년월일을 선택해주세요.";
                AgeText = string.Empty;
            }
            else if (value > DateTime.Today)
            {
                ValidationMessage = "미래의 날짜는 선택할 수 없습니다.";
                AgeText = "태어나지 않았습니다.";
            }
            else
            {
                ValidationMessage = null;

                var today = DateTime.Today;
                var age = today.Year - value.Value.Year;
                if (value.Value.Date > today.AddYears(-age)) 
                    age--;
                AgeText = $"만 {age}세";
            }

            ConfirmCommand.NotifyCanExecuteChanged(); // 상태 변경 시 버튼 활성화 갱신
        }
        private void OnConfirm()
        {
            AgeText = $"✅ {BirthDate:yyyy-MM-dd} 생년월일이 서버에 전송되었습니다.";
        }

        private bool CanConfirm()
        {
            return BirthDate is not null && BirthDate <= DateTime.Today;
        }
    }
}
