using Prism.DryIoc;
using Prism.Ioc;
using Prism.Regions;
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

namespace TabControl.Views
{
    public partial class AdvancedTabView : UserControl
    {
        private readonly IRegionManager _regionManager;

        public AdvancedTabView()
        {
            InitializeComponent();

            //var regionManager = ((PrismApplication)Application.Current)
            //               .Container.Resolve<IRegionManager>();

            //regionManager.RequestNavigate("AdvancedRegion", "HistoryView");
           // this.Loaded += Window_Loaded;
        }

        //private void Window_Loaded(object sender, RoutedEventArgs e)
        //{
        //    _regionManager.RequestNavigate("AdvancedRegion", nameof(HistoryView), result =>
        //    {
        //        if (result.Result == false)
        //            MessageBox.Show("Navigation 실패: HistoryView 못 찾음");
        //    });
        //}
    }
}
