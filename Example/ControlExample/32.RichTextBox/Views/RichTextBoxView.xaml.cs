using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using RichTextBox.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace RichTextBox.Views
{
    /// <summary>
    /// RichTextBoxView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class RichTextBoxView : UserControl
    {
        public RichTextBoxView()
        {
            InitializeComponent();
            this.Loaded += RichTextBoxView_Loaded;
        }
        private void RichTextBoxView_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is RichTextBoxViewModel vm)
            {
                myRichTextBox.Document = vm.Document;

                // Command 연결
                vm.SetupCommands(
                    ToggleBold,
                    ToggleItalic,
                    ToggleUnderline,
                    SetRed,
                    SetBlue,
                    SaveDocument,
                    LoadDocument
                );
            }
        }

        private void ToggleBold()
        {
            if (myRichTextBox.Selection != null)
            {
                var selection = myRichTextBox.Selection;
                var current = selection.GetPropertyValue(TextElement.FontWeightProperty);
                if (current == DependencyProperty.UnsetValue || (FontWeight)current != FontWeights.Bold)
                    selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Bold);
                else
                    selection.ApplyPropertyValue(TextElement.FontWeightProperty, FontWeights.Normal);
            }
        }

        private void ToggleItalic()
        {
            if (myRichTextBox.Selection != null)
            {
                var selection = myRichTextBox.Selection;
                var current = selection.GetPropertyValue(TextElement.FontStyleProperty);
                if (current == DependencyProperty.UnsetValue || (FontStyle)current != FontStyles.Italic)
                    selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Italic);
                else
                    selection.ApplyPropertyValue(TextElement.FontStyleProperty, FontStyles.Normal);
            }
        }

        private void ToggleUnderline()
        {
            if (myRichTextBox.Selection != null)
            {
                var selection = myRichTextBox.Selection;
                var decorations = selection.GetPropertyValue(Inline.TextDecorationsProperty);

                if (decorations == DependencyProperty.UnsetValue || decorations != TextDecorations.Underline)
                    selection.ApplyPropertyValue(Inline.TextDecorationsProperty, TextDecorations.Underline);
                else
                    selection.ApplyPropertyValue(Inline.TextDecorationsProperty, null);
            }
        }

        private void SetRed()
        {
            if (myRichTextBox.Selection != null)
            {
                myRichTextBox.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Red);
            }
        }

        private void SetBlue()
        {
            if (myRichTextBox.Selection != null)
            {
                myRichTextBox.Selection.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Blue);
            }
        }

        private void SaveDocument()
        {
            SaveFileDialog dlg = new SaveFileDialog
            {
                Filter = "Rich Text Format (*.rtf)|*.rtf",
                DefaultExt = ".rtf"
            };

            if (dlg.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(dlg.FileName, FileMode.Create))
                {
                    TextRange range = new TextRange(myRichTextBox.Document.ContentStart, myRichTextBox.Document.ContentEnd);
                    range.Save(fs, DataFormats.Rtf);
                }
            }
        }

        private void LoadDocument()
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Filter = "Rich Text Format (*.rtf)|*.rtf",
                DefaultExt = ".rtf"
            };

            if (dlg.ShowDialog() == true)
            {
                using (FileStream fs = new FileStream(dlg.FileName, FileMode.Open))
                {
                    TextRange range = new TextRange(myRichTextBox.Document.ContentStart, myRichTextBox.Document.ContentEnd);
                    range.Load(fs, DataFormats.Rtf);
                }
            }
        }
    }
}
