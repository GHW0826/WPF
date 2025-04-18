using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

namespace Example3
{
    public partial class UserInputViewModel : ObservableValidator
    {
        [ObservableProperty]
        [Required(ErrorMessage = "이름은 비어 있을 수 없습니다.")]
        [MinLength(2, ErrorMessage = "이름은 최소 2자 이상이어야 합니다.")]
        private string userName;

        partial void OnUserNameChanged(string value)
        {
            ValidateProperty(value, "UserName");
        }
    }
}
