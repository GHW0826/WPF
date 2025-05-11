using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BehaviorEventToCommand
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ICommand ButtonClickCommand { get; }

        private string _output = "Waiting...";
        public string Output
        {
            get => _output;
            set { _output = value; OnPropertyChanged(); }
        }

        public MainViewModel()
        {
            ButtonClickCommand = new RelayCommand(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            Output = $"Clicked at {DateTime.Now:T}";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

    // RelayCommand 그대로 사용
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter) => _canExecute == null || _canExecute();
        public void Execute(object parameter) => _execute();

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
