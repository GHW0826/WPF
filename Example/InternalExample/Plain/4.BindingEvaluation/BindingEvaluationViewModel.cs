using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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

namespace BindingEvaluation
{
    public class BindingEvaluationViewModel : INotifyPropertyChanged
    {
        private string _oneWayText = "OneWay 초기값";
        public string OneWayText
        {
            get => _oneWayText;
            set
            {
                _oneWayText = value;
                OnPropertyChanged();
                Debug.WriteLine($"OneWayText changed: {value}");
            }
        }

        private string _twoWayText = "TwoWay 초기값";
        public string TwoWayText
        {
            get => _twoWayText;
            set
            {
                _twoWayText = value;
                OnPropertyChanged();
                Debug.WriteLine($"TwoWayText changed: {value}");
            }
        }

        private string _oneTimeText = "OneTime 초기값";
        public string OneTimeText
        {
            get => _oneTimeText;
            set
            {
                _oneTimeText = value;
                OnPropertyChanged();
                Debug.WriteLine($"OneTimeText changed: {value}");
            }
        }

        private string _lostFocusText = "LostFocus 초기값";
        public string LostFocusText
        {
            get => _lostFocusText;
            set
            {
                _lostFocusText = value;
                OnPropertyChanged();
                Debug.WriteLine($"LostFocusText changed: {value}");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
