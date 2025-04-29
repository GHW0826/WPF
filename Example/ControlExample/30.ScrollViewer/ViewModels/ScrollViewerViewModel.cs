using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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

namespace ScrollViewer.Views
{
    public partial class ScrollViewerViewModel : ObservableObject
    {
		[ObservableProperty]
		private double scrollPosition;

		public IRelayCommand ScrollToTopCommand { get; }
		public IRelayCommand ScrollToBottomCommand { get; }

		public ScrollViewerViewModel()
		{
			ScrollToTopCommand = new RelayCommand(() => ScrollPosition = 0);
			ScrollToBottomCommand = new RelayCommand(() => ScrollPosition = double.MaxValue);
		}
	}
}
