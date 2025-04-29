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

namespace ToolTip.ViewModels
{
   
    public partial class ToolTipViewModel : ObservableObject
    {
        [ObservableProperty]
        private string toolTipMessage = "초기 툴팁 메시지입22니다.";

    }
}
