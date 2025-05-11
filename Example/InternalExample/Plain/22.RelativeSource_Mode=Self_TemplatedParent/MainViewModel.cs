using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelativeSource_Mode_Self_TemplatedParent
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string _status;
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Status)));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
