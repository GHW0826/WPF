using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace TabControl.ViewModels
{
    public partial class TabControlViewModel : ObservableObject
    {
        [ObservableProperty]
        private string lastLog;

        [ObservableProperty]
        private int selectedTabIndex;

        [ObservableProperty]
        private string tabStatus;
        
        [ObservableProperty]
        private object? selectedTabContent;

        [ObservableProperty]
        private TabItemViewModel selectedTab;

        public ObservableCollection<TabItemViewModel> Tabs { get; }

        public TabControlViewModel()
        {
            Tabs = new ObservableCollection<TabItemViewModel>
            {
                new GeneralTabViewModel(),
                new NotificationTabViewModel(),
                new AdvancedTabViewModel()
            };

            SelectedTab = Tabs[0];
        }

        [RelayCommand]
        private void AddTab()
        {
            var newTab =  new GeneralTabViewModel();
            Tabs.Add(newTab);
            SelectedTab = newTab;
        }

        [RelayCommand]
        private void CloseTab(TabItemViewModel tab)
        {
            if (Tabs.Contains(tab))
                Tabs.Remove(tab);
        }

        partial void OnSelectedTabChanged(TabItemViewModel value)
        {
            if (value != null)
                LastLog = $"[{DateTime.Now:T}] '{value.Title}' 탭이 선택되었습니다.";
        }

        partial void OnSelectedTabIndexChanged(int value)
        {
            TabStatus = $"현재 선택된 탭 인덱스: {value}";
            SelectedTabContent = value switch
            {
                0 => new GeneralTabViewModel(),
                1 => new NotificationTabViewModel(),
                2 => new AdvancedTabViewModel(),
                _ => null
            };
        }
    }
}
