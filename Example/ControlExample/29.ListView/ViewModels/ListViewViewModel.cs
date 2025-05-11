using CommunityToolkit.Mvvm.ComponentModel;
using Prism.Common;
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

namespace ListView.ViewModels
{
    public class ListItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }

    public partial class ListViewViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<ListItem> items = new();

        [ObservableProperty]
        private ListItem? selectedItem;
        
        public ListViewViewModel()
        {
            for (int i = 1; i <= 10; i++)
            {
                Items.Add(new ListItem
                {
                    Title = $"아이템 {i}",
                    Description = $"이것은 아이템 {i}의 설명입니다."
                });
            }
        }
    }
}
