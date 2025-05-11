using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
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

namespace Calendar.ViewModels
{
    public class EnumValuesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Type enumType && enumType.IsEnum)
            {
                return Enum.GetValues(enumType);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public partial class CalendarViewModel : ObservableObject
    {
        [ObservableProperty]
        private DateTime? _selectedDate = DateTime.Today;

        //////////////////////////////////////////////////////


        [ObservableProperty]
        private DateTime _startDate = DateTime.Today.AddMonths(-1); // 1개월 전부터 선택 가능

        [ObservableProperty]
        private DateTime _endDate = DateTime.Today.AddMonths(1);    // 1개월 후까지 선택 가능

        
        //////////////////////////////////////////////////////

        [ObservableProperty]
        private CalendarSelectionMode _selectionMode = CalendarSelectionMode.SingleDate;

        //////////////////////////////////////////////////////

        public ObservableCollection<DateTime> BlackoutDates { get; } = new ObservableCollection<DateTime>();
        public CalendarViewModel()
        {
            // 예시로 며칠 막아둔다
            BlackoutDates.Add(DateTime.Today.AddDays(2));
            BlackoutDates.Add(DateTime.Today.AddDays(4));
            BlackoutDates.Add(DateTime.Today.AddDays(6));
        }

        
        //////////////////////////////////////////////////////


        [ObservableProperty]
        private DateTime _displayDate = DateTime.Today;

        [RelayCommand]
        private void MoveToPreviousMonth()
        {
            DisplayDate = DisplayDate.AddMonths(-1);
        }

        [RelayCommand]
        private void MoveToNextMonth()
        {
            DisplayDate = DisplayDate.AddMonths(1);
        }
    }
}
