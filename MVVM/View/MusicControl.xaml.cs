using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using PokeWish.MVVM.ViewModel;

namespace PokeWish.MVVM.View
{
    public partial class MusicControl : UserControl
    {
        public MusicControl()
        {
            InitializeComponent();
            DataContext = new MusicControlVM();
        }

        // Gestionnaire d'événements pour le clic gauche sur le Border
        private void OnToggleMusicClick(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is MusicControlVM viewModel)
            {
                // Exécuter la commande pour basculer la musique
                if (viewModel.ToggleMusicCommand.CanExecute(null))
                {
                    viewModel.ToggleMusicCommand.Execute(null);
                }
            }

            // Empêcher le Border de verrouiller la sélection utilisateur
            //Keyboard.ClearFocus();
            //Mouse.Capture(null);
        }
    }
}
