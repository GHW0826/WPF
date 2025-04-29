using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Button.ViewModels
{
    public class ButtonItem : BindableBase
    {
        public string Label { get; set; }

        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }
    }

    public partial class ButtonViewModel : ObservableObject
    {
        public IRelayCommand ClickCommand { get; }
        public ButtonViewModel()
        {
            ClickCommand = new RelayCommand(OnClick);

            //////////////////////////////////////////////////////////

            ClickWithParamCommand = new RelayCommand<string>(OnClickWithParam);
            
            //////////////////////////////////////////////////////////

            SubmitCommand = new RelayCommand(OnSubmit, CanSubmit);

            //////////////////////////////////////////////////////////

            Buttons = new ObservableCollection<ButtonItem>
            {
                new ButtonItem { Label = "옵션 1" },
                new ButtonItem { Label = "옵션 2" },
                new ButtonItem { Label = "옵션 3" }
            };

            SelectCommand = new RelayCommand<ButtonItem>(OnSelect);
        }

        private void OnClick()
        {
            MessageBox.Show("버튼이 클릭되었습니다!");
        }

        //////////////////////////////////////////////////////////
        public IRelayCommand<string> ClickWithParamCommand { get; }

        private void OnClickWithParam(string param)
        {
            MessageBox.Show($"파라미터로 전달된 값: {param}");
        }

        //////////////////////////////////////////////////////////

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SubmitCommand))]
        private string input;

        public IRelayCommand SubmitCommand { get; }

        private void OnSubmit()
        {
            MessageBox.Show($"입력된 값: {Input}");
        }

        private bool CanSubmit()
        {
            return !string.IsNullOrWhiteSpace(Input);
        }

        //////////////////////////////////////////////////////////
        public ObservableCollection<ButtonItem> Buttons { get; }

        public IRelayCommand<ButtonItem> SelectCommand { get; }

        private void OnSelect(ButtonItem selected)
        {
            foreach (var btn in Buttons)
                btn.IsSelected = false;
            selected.IsSelected = true;
        }

        //////////////////////////////////////////////////////////

    }
}
