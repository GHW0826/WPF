using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Prism.Common;
using System.ComponentModel.DataAnnotations;
using System.Windows;
using System.Windows.Controls;

namespace RadioButton.ViewModels
{
    public partial class RadioButtonViewModel : ObservableValidator
    {
        public enum GenderType
        {
            Male,
            Female
        }

        [ObservableProperty]
        [Required(ErrorMessage = "성별을 선택해야 합니다.")]
        private GenderType? selectedGender; // = "Male";

        [ObservableProperty]
        private string genderMessage;

        public IRelayCommand ConfirmCommand { get; }

        public RadioButtonViewModel()
        {
            ConfirmCommand = new RelayCommand(OnConfirm, CanConfirm);
            SelectedGender = GenderType.Male;
        }

        partial void OnSelectedGenderChanged(GenderType? value)
        {
            ConfirmCommand.NotifyCanExecuteChanged();
        }

        private void OnConfirm()
        {
            System.Diagnostics.Debug.WriteLine($"선택된 성별: {SelectedGender}");
            GenderMessage = SelectedGender switch
            {
                GenderType.Male => "🧔 남성을 선택하셨습니다.",
                GenderType.Female => "👩 여성을 선택하셨습니다.",
                _ => "⚠ 성별이 선택되지 않았습니다."
            };
        }
        private bool CanConfirm()
        {
            return SelectedGender != null;
        }
    }
}
