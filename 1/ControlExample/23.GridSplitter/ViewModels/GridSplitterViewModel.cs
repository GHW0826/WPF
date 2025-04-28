using CommunityToolkit.Mvvm.ComponentModel;
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

namespace GridSplitter.ViewModels
{
    public partial class GridSplitterViewModel : ObservableObject
    {
        [ObservableProperty]
        private GridLength leftColumnWidth = new GridLength(2, GridUnitType.Star);  // 왼쪽 2*

        [ObservableProperty]
        private GridLength centerColumnWidth = new GridLength(3, GridUnitType.Star); // 가운데 3*

        [ObservableProperty]
        private GridLength topRowHeight = new GridLength(1, GridUnitType.Star);    // Top 1*

        [ObservableProperty]
        private GridLength bottomRowHeight = new GridLength(1, GridUnitType.Star); // Bottom 1*
    }
}
