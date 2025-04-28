using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prism.Common;
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

namespace ToggleButton.ViewModels
{
    public partial class ToggleViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isNotificationOn;

        [ObservableProperty]
        private string notificationMessage;

        [ObservableProperty]
        private int progress = 40;

        [ObservableProperty]
        private bool isSliderEnabled;

        [ObservableProperty]
        private string notificationText;

        public IRelayCommand ToggleCommand { get; }

        [ObservableProperty]
        private bool isNoticeEnabled;

        [ObservableProperty]
        private bool isNewsEnabled;

        [ObservableProperty]
        private bool isUpdateEnabled;

        [ObservableProperty]
        private string selectedFilters;

        partial void OnIsNoticeEnabledChanged(bool value) => UpdateFilters();
        partial void OnIsNewsEnabledChanged(bool value) => UpdateFilters();
        partial void OnIsUpdateEnabledChanged(bool value) => UpdateFilters();


        public ToggleViewModel()
        {
            ToggleCommand = new RelayCommand(OnToggle);
        }

        partial void OnIsNotificationOnChanged(bool value)
        {
            NotificationMessage = value ? "🔔 알림이 활성화되었습니다." : "🔕 알림이 꺼져 있습니다.";
            IsSliderEnabled = value;
        }
        private void OnToggle()
        {
            NotificationText = IsNotificationOn
                ? "🔔 알림 상태가 서버에 전송되었습니다."
                : "🔕 알림이 꺼진 상태가 서버에 반영되었습니다.";
        }

        private void UpdateFilters()
        {
            var list = new List<string>();
            if (IsNoticeEnabled) list.Add("📢 공지");
            if (IsNewsEnabled) list.Add("📰 뉴스");
            if (IsUpdateEnabled) list.Add("⬆ 업데이트");

            SelectedFilters = list.Count > 0 ? $"선택된 필터: {string.Join(", ", list)}" : "필터 없음";
        }
    }
}
