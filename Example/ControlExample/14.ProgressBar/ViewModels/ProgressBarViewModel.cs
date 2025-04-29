using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prism.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading;
using System.Threading.Channels;
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

namespace ProgressBar.ViewModels
{
    public partial class ProgressBarViewModel : ObservableObject
    {

        [ObservableProperty]
        private int progress = 0;

        [ObservableProperty]
        private bool isBusy = false;

        [ObservableProperty]
        private bool isIndeterminate = false;

        private bool CanStart() => !IsBusy;

        public IRelayCommand StartCommand { get; }
        public IRelayCommand CancelCommand { get; }
        public IRelayCommand ToggleIndeterminateCommand { get; }


        private CancellationTokenSource? _cts;

        public ProgressBarViewModel()
        {
            StartCommand = new AsyncRelayCommand(StartAsync, CanStart);
            CancelCommand = new RelayCommand(Cancel, () => IsBusy);
            ToggleIndeterminateCommand = new RelayCommand(() => IsIndeterminate = !IsIndeterminate);
        }

        private async Task StartAsync()
        {
            IsBusy = true;
            StartCommand.NotifyCanExecuteChanged();
            CancelCommand.NotifyCanExecuteChanged();

            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            Progress = 0;
            try
            {
                for (int i = 0; i <= 100; i++)
                {
                    token.ThrowIfCancellationRequested();
                    Progress = i;
                    await Task.Delay(30, token);
                }
            }
            catch (TaskCanceledException) { }


            IsBusy = false;
            StartCommand.NotifyCanExecuteChanged();
            CancelCommand.NotifyCanExecuteChanged();
        }
        private void Cancel()
        {
            _cts?.Cancel();
        }
    }
}
