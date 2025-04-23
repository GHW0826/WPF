using ListBox.ViewModels;
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

namespace ListBox.Views
{
    /// <summary>
    /// ListBoxView.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class ListBoxView : UserControl
    {
        public ListBoxView()
        {
            InitializeComponent();

            this.Loaded += (s, e) =>
            {
                if (this.DataContext is ListBoxViewModel vm)
                {
                    LB1.SelectionChanged += (sender, args) =>
                    {
                        vm.SelectedHobbies ??= new();
                        vm.SelectedHobbies.Clear();
                        foreach (HobbyItem item in LB1.SelectedItems)
                            vm.SelectedHobbies.Add(item);
                        vm.RemoveSelectedCommand.NotifyCanExecuteChanged();
                    };
                }
            };
        }
    }
}
