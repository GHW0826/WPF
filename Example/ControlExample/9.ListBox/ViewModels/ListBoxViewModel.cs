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

namespace ListBox.ViewModels
{
    public class HobbyItem
    {
        public string Icon { get; set; }
        public string Name { get; set; }

        public override string ToString() => $"{Icon} {Name}";
    }


    public partial class ListBoxViewModel : ObservableObject
    {
        public ObservableCollection<HobbyItem> Hobbies { get; } = new()
        {
            new HobbyItem { Icon = "🏃", Name = "운동" },
            new HobbyItem { Icon = "🎨", Name = "그림" },
            new HobbyItem { Icon = "🎮", Name = "게임" },
            new HobbyItem { Icon = "📚", Name = "독서" },
            new HobbyItem { Icon = "🎵", Name = "음악감상" }
        };

        [ObservableProperty]
        private ObservableCollection<HobbyItem> selectedHobbies = new();

        [ObservableProperty]
        private HobbyItem selectedHobby;

        public IRelayCommand AddCommand { get; }
        public IRelayCommand ClearAllCommand { get; }
        public IRelayCommand RemoveSelectedCommand { get; }

        private int hobbyCounter = 1;

        public ListBoxViewModel()
        {
            AddCommand = new RelayCommand(AddHobby);
            ClearAllCommand = new RelayCommand(ClearAll);
            RemoveSelectedCommand = new RelayCommand(RemoveSelected, CanRemove);
        }
        private void AddHobby()
        {
            Hobbies.Add(new HobbyItem { Icon = "🎲", Name = $"새 취미 {hobbyCounter++}" });
        }

        private void ClearAll()
        {
            Hobbies.Clear();
            SelectedHobbies.Clear();
            RemoveSelectedCommand.NotifyCanExecuteChanged();
        }

        private void RemoveSelected()
        {
            foreach (var item in SelectedHobbies.ToList())
                Hobbies.Remove(item);
            SelectedHobbies.Clear();
        }

        private bool CanRemove()
        {
            return SelectedHobbies.Any();
        }
    }
}
