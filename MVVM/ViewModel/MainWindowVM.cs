using System.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using PokeWish.MVVM.View;

namespace PokeWish.MVVM.ViewModel
{
    public class MainWindowVM : BaseVM
    {
        private object _currentView;

        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }



        public ICommand NavigateToWelcomeCommand { get; }
        public ICommand NavigateToSettingsCommand { get; }
        public ICommand NavigateToLoginCommand { get; }
        public ICommand NavigateToMainCommand { get; }

        public MainWindowVM()
        {
            // Initialisation de la première vue
            CurrentView = new Welcome();

            // Initialisation des commandes
            NavigateToWelcomeCommand = new RelayCommand(() => CurrentView = new Welcome());
            NavigateToSettingsCommand = new RelayCommand(() => CurrentView = new Settings());
            NavigateToLoginCommand = new RelayCommand(() => CurrentView = new LoginRegister());
            NavigateToMainCommand = new RelayCommand(() => CurrentView = new Second()); // Vue principale
        }
    }
}
