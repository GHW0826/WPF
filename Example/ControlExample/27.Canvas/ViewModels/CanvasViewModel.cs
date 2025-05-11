using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

namespace Canvas.ViewModels
{
    public partial class CanvasItem : ObservableObject
    {
        [ObservableProperty]
        public string title;
        [ObservableProperty]
        public double x;
        [ObservableProperty]
        public double y;
    }

    public partial class CanvasViewModel : ObservableObject
    {
        [ObservableProperty]
        private ObservableCollection<CanvasItem> items = new();


        public IRelayCommand MoveFirstItemCommand { get; }

        public CanvasViewModel()
        {
            // 샘플 데이터 추가
            Items.Add(new CanvasItem { Title = "버튼 4", X = 50, Y = 130 });
            Items.Add(new CanvasItem { Title = "버튼 5", X = 200, Y = 200 });
            Items.Add(new CanvasItem { Title = "버튼 6", X = 50, Y = 200 });


            MoveFirstItemCommand = new RelayCommand(MoveFirstItem);
        }

        private void MoveFirstItem()
        {
            if (Items.Count > 0)
            {
                Items[0].X += 20; // 오른쪽으로 20 이동
                Items[0].Y += 10; // 아래로 10 이동
            }
        }
    }
}
