using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
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

namespace CheckBox.ViewModels
{
    public partial class CheckBoxViewModel : ObservableValidator
    {
        [ObservableProperty] 
        private bool isAllAgreed;

        [ObservableProperty]
        [Required(ErrorMessage = "개인정보 수집 및 이용에 동의해야 합니다.")]
        private bool? isPrivacyAgreed;

        [ObservableProperty]
        [Required(ErrorMessage = "서비스 이용 약관에 동의해야 합니다.")]
        private bool? isAgreed;

        public IRelayCommand NextCommand { get; }

        public CheckBoxViewModel()
        {
            NextCommand = new RelayCommand(OnNext, CanNext);
        }

        partial void OnIsAgreedChanged(bool? value)
        {
            ValidateProperty(value, nameof(IsAgreed));
            SyncAllAgreed();
            NextCommand.NotifyCanExecuteChanged();
        }

        partial void OnIsPrivacyAgreedChanged(bool? value)
        {
            ValidateProperty(value, nameof(IsPrivacyAgreed));
            SyncAllAgreed();
            NextCommand.NotifyCanExecuteChanged();
        }

        partial void OnIsAllAgreedChanged(bool value)
        {
            if (value)
            {
                IsAgreed = true;
                IsPrivacyAgreed = true;
            }
        }

        private void SyncAllAgreed()
        {
            IsAllAgreed = IsAgreed == true && IsPrivacyAgreed == true;
        }

        private async void OnNext()
        {
            Debug.WriteLine("🌐 서버에 동의 정보 전송 중...");

            await Task.Delay(1000); // 모의 API 대기

            Debug.WriteLine("✅ 서버 전송 완료: 동의 상태 저장됨");
        }

        private bool CanNext()
        {
            return IsAgreed == true && IsPrivacyAgreed == true;
        }
    }
}
