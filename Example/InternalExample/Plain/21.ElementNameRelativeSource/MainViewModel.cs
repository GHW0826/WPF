using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementNameRelativeSource
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _inputText = "초기 텍스트";

        public string InputText
        {
            get => _inputText;
            set
            {
                _inputText = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(InputText)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
