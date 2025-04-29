using PasswordBox.ViewModels;
using System.Windows.Controls;
using System.Windows;

namespace PasswordBox.Views
{
    /// <summary>
    /// PasswordBoxView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class PasswordBoxView : UserControl
    {
        public PasswordBoxView()
        {
            InitializeComponent();
        }

        private void PwdBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is PasswordBoxViewModel vm && sender is System.Windows.Controls.PasswordBox pwdBox)
            {
                // SecureString으로 바로 복사
                vm.Password?.Clear();
                foreach (char c in pwdBox.Password)
                {
                    vm.Password?.AppendChar(c);
                }

                vm.SubmitCommand.NotifyCanExecuteChanged();
            }
        }
    }
}
