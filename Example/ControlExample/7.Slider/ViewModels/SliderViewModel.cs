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

namespace Slider.ViewModels
{
    public partial class SliderViewModel : ObservableObject
    {
        [ObservableProperty]
        private int volume = 50;

        [ObservableProperty]
        private string volumeMessage;

        [ObservableProperty]
        private string volumeLevel;

        [ObservableProperty]
        private bool isSliderEnabled = true;


        public IRelayCommand ApplyCommand { get; }

        public SliderViewModel()
        {
            ApplyCommand = new RelayCommand(OnApply);
        }

        // 2. Slider 값 변경 시 실시간 메시지 반영
        partial void OnVolumeChanged(int value)
        {
            IsSliderEnabled = value < 80;
            SetVolumeMessage(value);
            VolumeLevel = value switch
            {
                <= 30 => "Low",
                <= 70 => "Mid",
                _ => "High"
            };
        }

        private void OnApply()
        {
            IsSliderEnabled = true;
            SetVolumeMessage(Volume, "Command");
        }

        private void SetVolumeMessage(int value, string postFix = "")
        {
            VolumeMessage = value switch
            {
                >= 0 and <= 30 => "🔈 조용한 볼륨입니다." + postFix,
                > 30 and <= 70 => "🔉 적절한 볼륨입니다." + postFix,
                > 70 => "🔊 다소 큰 볼륨입니다." + postFix,
                _ => "⚠ 볼륨 설정 오류" + postFix
            };
        }
    }
}
