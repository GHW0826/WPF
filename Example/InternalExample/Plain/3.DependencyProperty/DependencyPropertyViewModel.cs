using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
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

namespace DependencyProperty
{
    public class DependencyPropertyViewModel : INotifyPropertyChanged
    {
        private string _sampleText;
        public string SampleText
        {
            get => _sampleText;
            set
            {
                if (_sampleText != value)
                {
                    _sampleText = value;
                    OnPropertyChanged();
                    Output = $"Text changed: {value}";
                }
            }
        }

        private string _output = "Waiting...";
        public string Output
        {
            get => _output;
            set 
            { 
                _output = value; 
                OnPropertyChanged(); 
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
