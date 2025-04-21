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
using System.Windows.Threading;
using System.Xml.Linq;

namespace Example3.Modules
{
    public partial class ValidatedInputViewModel : ObservableValidator
    {
        [ObservableProperty]
        [Required(ErrorMessage = "이름은 필수입니다.")]
        [MinLength(2, ErrorMessage = "이름은 최소 2자 이상이어야 합니다.")]
        [NotifyPropertyChangedFor(nameof(bCanSubmit))]
        private string name;
        
        [ObservableProperty]
        private string age;


        [ObservableProperty] private string code1;
        [ObservableProperty] private string code2;
        [ObservableProperty] private string code3;
        [ObservableProperty] private string code4;


        [ObservableProperty] private string email;


        [ObservableProperty]
        private string searchKeyword;
        private readonly DispatcherTimer _debounceTimer;


        [ObservableProperty]
        private string comment;


        public bool bCanSubmit => !HasErrors && !string.IsNullOrWhiteSpace(Name);
        public IRelayCommand SubmitCommand { get; }

        public ValidatedInputViewModel()
        {
            SubmitCommand = new RelayCommand(OnSubmit, CanSubmit);


            _debounceTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(500)
            };
            _debounceTimer.Tick += (s, e) =>
            {
                _debounceTimer.Stop();
                Debug.WriteLine($"🔍 검색 실행: {SearchKeyword}");
            };
        }

        partial void OnNameChanged(string value)
        {
            ValidateProperty(value, "Name");
            OnPropertyChanged(nameof(bCanSubmit));
            SubmitCommand.NotifyCanExecuteChanged();
        }

        private void OnSubmit()
        {
            Debug.WriteLine($"제출됨: {Name}");
        }

        private bool CanSubmit()
        {
            return bCanSubmit;
        }

        partial void OnSearchKeywordChanged(string value)
        {
            _debounceTimer.Stop();
            _debounceTimer.Start();
        }
    }
}
