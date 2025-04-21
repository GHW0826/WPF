using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
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

namespace Example3.Modules
{
    public partial class ValidatedInputView : UserControl
    {
        // code-behind
        private bool _isComposingIme = false;

        public ValidatedInputView()
        {
            InitializeComponent();

            // 붙여넣기 방지 핸들러 등록
            CommandManager.AddPreviewExecutedHandler(NoCopy, TextBox_PreviewExecuted);

            // IME
            TextCompositionManager.AddPreviewTextInputStartHandler(CommentBox, OnImeStart);
            TextCompositionManager.AddPreviewTextInputUpdateHandler(CommentBox, OnImeUpdate);
            TextCompositionManager.AddPreviewTextInputHandler(CommentBox, OnImeComplete);
        }
        private static readonly Regex _onlyNumbers = new Regex("[^0-9]+");

        private void TextBox_OnlyNumbers_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = _onlyNumbers.IsMatch(e.Text);
        }

        // IME 입력 차단 (붙여넣기 포함 방지)
        private void TextBox_PreviewExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Paste)
            {
                e.Handled = true;
            }
        }

        private void MoveFocusOnFullInput(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox textBox && textBox.Text.Length == textBox.MaxLength) {
                textBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
        }

        private void OnImeStart(object sender, TextCompositionEventArgs e)
        {
            _isComposingIme = true;
        }

        private void OnImeUpdate(object sender, TextCompositionEventArgs e)
        {
            // 조합 중 상태 유지
        }

        private void OnImeComplete(object sender, TextCompositionEventArgs e)
        {
            _isComposingIme = false;
            Debug.WriteLine("✔ 한글 조합 완료됨: " + ((TextBox)sender).Text);
        }

    }

    public class StringNullOrEmptyToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return string.IsNullOrWhiteSpace(value as string) ? Visibility.Visible : Visibility.Collapsed;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
