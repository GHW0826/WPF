using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace DispatcherTest.ViewModels
{
    
    public class DispatcherTestViewModel : BindableBase
    {
        private readonly Dispatcher _dispatcher;

        private string _message = "Ready";
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        public DelegateCommand StartWorkCommand { get; }

        public DispatcherTestViewModel()
        {
            _dispatcher = Dispatcher.CurrentDispatcher;
            StartWorkCommand = new DelegateCommand(StartWork);
        }

        private void StartWork()
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                _dispatcher.Invoke(() =>
                {
                    Message = "Work Started (Safe)";
                });
            });
        }
    }
}
