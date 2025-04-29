using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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

namespace DispatcherTest
{
    public class DispatcherTestViewModel : INotifyPropertyChanged
    {
        private string _message = "Ready";
        public string Message
        {
            get => _message;
            set { _message = value; OnPropertyChanged(); }
        }

        public ICommand StartWorkCommand { get; }

        public DispatcherTestViewModel()
        {
            StartWorkCommand = new StartWorkCommandImpl(this);
        }

        private void StartWork()
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Message = "Work Started (Safe)";
                });
            });
        }

        private class StartWorkCommandImpl : ICommand
        {
            private readonly DispatcherTestViewModel _viewModel;

            public StartWorkCommandImpl(DispatcherTestViewModel viewModel)
            {
                _viewModel = viewModel;
            }

            public bool CanExecute(object parameter) => true;

            public void Execute(object parameter)
            {
                _viewModel.StartWork();
            }

            public event EventHandler CanExecuteChanged
            {
                add { }
                remove { }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
