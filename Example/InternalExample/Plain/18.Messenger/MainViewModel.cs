using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Messenger_IEventAggregator
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public string InputText { get; set; }

        public ICommand SendCommand { get; }

        public MainViewModel()
        {
            SendCommand = new RelayCommand(() =>
            {
                Messenger.Instance.Send(InputText);
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
