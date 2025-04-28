using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

namespace Expander.ViewModels
{
    public class ExpanderGroup
    {
        public string Header { get; set; }
        public ObservableCollection<string> Items { get; set; }
    }

    public partial class ExpanderViewModel :  ObservableObject
    {
        [ObservableProperty]
        private bool isExpanded = true;

        [ObservableProperty]
        private string statusMessage;

        public ObservableCollection<ExpanderGroup> Expanders { get; } = new()
        {
            new ExpanderGroup
            {
                Header = "운동",
                Items = new ObservableCollection<string> { "🏃‍♂️ 달리기@@", "🏋️‍♀️ 헬스@@", "🚴‍♀️ 자전거 타기@@" }
            },
            new ExpanderGroup
            {
                Header = "취미",
                Items = new ObservableCollection<string> { "🎨 그림 그리기@@", "🎮 게임하기@@", "📚 독서하기@@" }
            }
        };

        partial void OnIsExpandedChanged(bool value)
        {
            if (value)
                LoadMoreCommand.Execute(null);
        }

        [RelayCommand]
        private void LoadMore()
        {
            StatusMessage = $"[{DateTime.Now:T}] Expander가 열렸습니다!";
        }
    }
}
