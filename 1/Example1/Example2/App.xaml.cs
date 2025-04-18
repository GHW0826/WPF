using Example2.Views;
using Prism.Ioc;
using System.Windows;

namespace Example2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // containerRegistry.RegisterForNavigation<HomeView, HomeViewModel>(); // 유지
            containerRegistry.RegisterForNavigation<LoginView, LoginViewModel>(); // ✅ 추가
        }
    }
}
