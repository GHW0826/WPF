using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger_IEventAggregator
{
    public class ReceiverViewModel : INotifyPropertyChanged
    {
        private string _message;
        public string Message
        {
            get => _message;
            set { _message = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Message))); }
        }

        public ReceiverViewModel()
        {
            Messenger.Instance.Register<string>(msg =>
            {
                Message = $"[받은 메시지] {msg}";
            });
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
