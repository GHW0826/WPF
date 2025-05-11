using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiBindingIMultiValueConverter
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private bool _isAdmin;
        public bool IsAdmin
        {
            get => _isAdmin;
            set { _isAdmin = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsAdmin))); }
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get => _isEnabled;
            set { _isEnabled = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsEnabled))); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
