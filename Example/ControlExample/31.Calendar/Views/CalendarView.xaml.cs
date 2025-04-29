using Calendar.ViewModels;
using System;
using System.Collections.Generic;
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

namespace Calendar.Views
{
    /// <summary>
    /// CalendarView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class CalendarView : UserControl
    {
        public CalendarView()
        {
            InitializeComponent();
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is CalendarViewModel vm)
            {
                calendar.BlackoutDates.Clear();
                foreach (var date in vm.BlackoutDates)
                {
                    calendar.BlackoutDates.Add(new CalendarDateRange(date));
                }
            }
        }
    }
}
