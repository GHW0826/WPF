using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace UniformGrid.ViewModels
{
    
    public partial class UniformGridViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<string> items = new();

        public UniformGridViewModel()
        {
            // 샘플 데이터 12개 생성
            for (int i = 1; i <= 12; i++)
            {
                Items.Add($"아이템 {i}");
            }
        }
    }
}
