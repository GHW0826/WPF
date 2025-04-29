using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prism.Common;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;

namespace ComboBox1.ViewModels
{
    public enum LanguageType
    {
        [Display(Name = "한국어")] ko,
        [Display(Name = "영어")] en,
        [Display(Name = "일본어")] ja,
        [Display(Name = "중국어")] ch
    }
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());
            return field?.GetCustomAttribute<DisplayAttribute>()?.Name ?? value.ToString();
        }
    }

    public class LanguageOption
    {
        public string Code { get; set; }
        public string DisplayName { get; set; }
    }

    public partial class ComboBoxViewModel : ObservableValidator
    {
        [ObservableProperty]
        [Required(ErrorMessage = "언어를 선택해야 합니다.")]
        private LanguageOption selectedLanguage;

        [ObservableProperty] private string languageMessage;

        public IRelayCommand ConfirmCommand { get; }
        public IRelayCommand AddLanguageCommand { get; }


        public ComboBoxViewModel()
        {
            ConfirmCommand = new RelayCommand(OnConfirm, CanConfirm);
            AddLanguageCommand = new RelayCommand(OnAddLanguage);
            selectedLanguage = LanguageOptions.FirstOrDefault();

            // Enum 기반 항목 바인딩
            foreach (var lang in Enum.GetValues<LanguageType>())
            {
                LanguageOptions.Add(new LanguageOption
                {
                    Code = lang.ToString().ToLower(),
                    DisplayName = lang.GetDisplayName()
                });
            }
        }

        public ObservableCollection<LanguageOption> LanguageOptions { get; } = new();

        partial void OnSelectedLanguageChanged(LanguageOption value)
        {
            ValidateProperty(value, nameof(SelectedLanguage));
            Debug.WriteLine($"선택됨: {value?.DisplayName} ({value?.Code})");
            ConfirmCommand.NotifyCanExecuteChanged();
        }

        private void OnConfirm()
        {
            Debug.WriteLine($"✔ 언어 선택 확정: {SelectedLanguage?.DisplayName}");
            LanguageMessage = SelectedLanguage?.Code switch
            {
                "ko" => "🇰🇷 한국어를 선택하셨습니다.",
                "en" => "🇺🇸 English selected.",
                "ja" => "🇯🇵 日本語が選ばれました。",
                _ => "알 수 없는 언어입니다."
            };
        }
        private void OnAddLanguage()
        {
            var newItem = new LanguageOption
            {
                Code = $"custom{LanguageOptions.Count + 1}",
                DisplayName = $"사용자 정의 언어 {LanguageOptions.Count + 1}"
            };
            LanguageOptions.Add(newItem);
            SelectedLanguage = newItem;
        }

        private bool CanConfirm()
        {
            return SelectedLanguage != null && !HasErrors;
        }
    }
}
