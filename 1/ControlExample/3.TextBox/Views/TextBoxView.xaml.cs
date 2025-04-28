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
using TextBox.ViewModels;

namespace TextBox.Views
{
    public partial class TextBoxView : UserControl
    {
        public TextBoxView()
        {
            InitializeComponent();
        }

        //////////////////////////////////////////////////////////

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            // IME 한글 조합 중이면 Enter 무시
            if (e.Key == Key.Enter) // && !InputMethod.GetIsInputMethodEnabled(TestInputBox))
            {
                if (DataContext is TextBoxViewModel vm && vm.SubmitCommand.CanExecute(null))
                {
                    vm.SubmitCommand.Execute(null);
                    e.Handled = true;
                }
            }
        }

        //////////////////////////////////////////////////////////


        private void OnKoreanClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is TextBoxViewModel vm)
                vm.SetCulture("ko");
        }

        private void OnEnglishClick(object sender, RoutedEventArgs e)
        {
            if (DataContext is TextBoxViewModel vm)
                vm.SetCulture("en");
        }
    }
}
