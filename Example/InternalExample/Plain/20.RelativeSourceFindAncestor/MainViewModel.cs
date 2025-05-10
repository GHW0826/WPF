using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RelativeSourceFindAncestor
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public string Title { get; set; }

        public ObservableCollection<string> Items { get; } = new ObservableCollection<string>
        {
            "Item A", "Item B", "Item C"
        };

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
