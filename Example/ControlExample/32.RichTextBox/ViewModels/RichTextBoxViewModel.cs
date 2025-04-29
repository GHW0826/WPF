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

namespace RichTextBox.ViewModels
{
    public partial class RichTextBoxViewModel : ObservableObject
    {
        [ObservableProperty]
        private FlowDocument document = new FlowDocument(
           new Paragraph(new Run("여기에 내용을 입력하세요."))
       );

        public IRelayCommand ToggleBoldCommand { get; private set; }
        public IRelayCommand ToggleItalicCommand { get; private set; }
        public IRelayCommand ToggleUnderlineCommand { get; private set; }

        public IRelayCommand SetRedCommand { get; private set; }
        public IRelayCommand SetBlueCommand { get; private set; }


        public IRelayCommand SaveCommand { get; private set; }
        public IRelayCommand LoadCommand { get; private set; }
        public void SetupCommands(
            Action boldAction, Action italicAction, Action underlineAction,
            Action setRedAction, Action setBlueAction,
            Action saveAction, Action loadAction)
        {
            ToggleBoldCommand = new RelayCommand(boldAction);
            ToggleItalicCommand = new RelayCommand(italicAction);
            ToggleUnderlineCommand = new RelayCommand(underlineAction);
            SetRedCommand = new RelayCommand(setRedAction);
            SetBlueCommand = new RelayCommand(setBlueAction);
            SaveCommand = new RelayCommand(saveAction);
            LoadCommand = new RelayCommand(loadAction);

            OnPropertyChanged(nameof(ToggleBoldCommand));
            OnPropertyChanged(nameof(ToggleItalicCommand));
            OnPropertyChanged(nameof(ToggleUnderlineCommand));
            OnPropertyChanged(nameof(SetRedCommand));
            OnPropertyChanged(nameof(SetBlueCommand));
            OnPropertyChanged(nameof(SaveCommand));
            OnPropertyChanged(nameof(LoadCommand));
        }
    }
}
