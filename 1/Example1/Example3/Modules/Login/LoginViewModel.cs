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

namespace Example3
{
    public partial class UserInputViewModel : ObservableValidator
    {
        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(CanLogin))]
        private string userId;

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                SetProperty(ref _password, value);
                ValidatePassword();
                OnPropertyChanged(nameof(CanLogin));
                LoginCommand.NotifyCanExecuteChanged();
            }
        }

        [ObservableProperty]
        private string statusMessage;

        public bool CanLogin => string.IsNullOrWhiteSpace(UserId) == false && string.IsNullOrWhiteSpace(Password) == false && Password.Length >= 4;

        public IAsyncRelayCommand LoginCommand { get; }

        public UserInputViewModel()
        {
            LoginCommand = new AsyncRelayCommand(LoginAsync, () => CanLogin);
        }

        private async Task LoginAsync()
        {
            StatusMessage = "로그인 중...";
            await Task.Delay(1500); // API 호출 대체

            if (UserId == "admin" && Password == "1234")
                StatusMessage = "✅ 로그인 성공";
            else
                StatusMessage = "❌ 아이디 또는 비밀번호 오류";
        }

        private void ValidatePassword()
        {
            ClearErrors(nameof(Password));
            ValidateAllProperties();
        }
    }
}
