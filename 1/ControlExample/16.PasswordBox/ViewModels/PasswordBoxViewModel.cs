using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prism.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
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

namespace PasswordBox.ViewModels
{
    public partial class PasswordBoxViewModel : ObservableObject
    {
        [ObservableProperty]
        private SecureString password;

        public IRelayCommand SubmitCommand { get; }

        public PasswordBoxViewModel()
        {
            SubmitCommand = new RelayCommand(OnSubmit, CanSubmit);
            Password = new SecureString();
        }

        private void OnSubmit()
        {
            // SecureString을 일반 문자열로 변환 (주의: 평문 노출)
            var ptr = Marshal.SecureStringToBSTR(Password);
            try
            {
                string plainPassword = Marshal.PtrToStringBSTR(ptr);
                MessageBox.Show($"제출된 비밀번호: {plainPassword}");
            }
            finally
            {
                Marshal.ZeroFreeBSTR(ptr); // 평문 메모리 클리어
            }
        }

        // password box 공유 다 조건이 만족해야 함.
        private bool CanSubmit()
        {
            string plainPassword;
            var ptr = Marshal.SecureStringToBSTR(Password);
            try
            {
                plainPassword = Marshal.PtrToStringBSTR(ptr);
                // MessageBox.Show($"제출된 비밀번호: {plainPassword}");
            }
            finally
            {
                Marshal.ZeroFreeBSTR(ptr); // 평문 메모리 클리어
            }
            if (string.IsNullOrWhiteSpace(plainPassword))
                return false;

            // 6자 이상 체크
            if (plainPassword.Length < 6)
                return false;

            // 숫자와 영문 조합 체크 (간단)
            if (!Regex.IsMatch(plainPassword, @"^(?=.*[A-Za-z])(?=.*\d).+$"))
                return false;

            return true;
        }
    }
}
