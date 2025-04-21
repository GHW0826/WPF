using Prism.Commands;
using Prism.Mvvm;
using System.Windows.Input;

namespace Example3.Modules
{
    public class HomeViewModel : BindableBase
    {
        private string _welcomeMessage = "🎉 Shell is ready!";
        public string WelcomeMessage
        {
            get => _welcomeMessage;
            set => SetProperty(ref _welcomeMessage, value);
        }

        public ICommand ChangeMessageCommand { get; }

        public HomeViewModel()
        {
            ChangeMessageCommand = new DelegateCommand(() =>
            {
                WelcomeMessage = "✔ ViewModel‑to‑View 바인딩 OK!";
            });
        }
    }
}
