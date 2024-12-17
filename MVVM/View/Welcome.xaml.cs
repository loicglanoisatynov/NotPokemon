using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace PokeWish.MVVM.View
{
    public partial class Welcome : UserControl
    {
        private bool _animationCompleted = false; // Indique si l'animation est terminée
        private bool _keyPressed = false;
        public Welcome()
        {
            InitializeComponent();
            StartAnimation(); // Démarre l'animation à l'ouverture de la view

            this.Loaded += (s, e) =>
            {
                var parentWindow = Window.GetWindow(this);
                if (parentWindow != null)
                {
                    parentWindow.KeyDown += Window_KeyDown;
                }
            };
        }

        /// <summary>
        /// Démarre l'animation de descente du logo.
        /// </summary>
        private void StartAnimation()
        {
            // Animation pour faire descendre le logo
            var slideDownAnimation = new ThicknessAnimation
            {
                From = new Thickness(0, -300, 0, 0), // Point de départ (hors de l'écran, en haut)
                To = new Thickness(0, 100, 0, 0),   // Point final (position désirée)
                Duration = TimeSpan.FromSeconds(5), // Durée de 2 secondes
                EasingFunction = new QuadraticEase() // Animation fluide
            };

            // Action déclenchée à la fin de l'animation
            slideDownAnimation.Completed += (sender, e) =>
            {
                _animationCompleted = true; // L'animation est terminée
                WelcomeText.Visibility = Visibility.Visible; // Rendre le texte visible
                StartFloatingAnimation();   // Lancer l'animation de flottement
                StartTextBlinkingAnimation(); // Lancer le clignotement du texte
            };


            // Appliquer l'animation au logo
            Logo.BeginAnimation(MarginProperty, slideDownAnimation);
        }

        private void StartTextBlinkingAnimation()
        {
            var blinkAnimation = new DoubleAnimation
            {
                From = 1.0,
                To = 0.0,
                Duration = TimeSpan.FromSeconds(0.8),
                AutoReverse = true,
                RepeatBehavior = RepeatBehavior.Forever
            };

            WelcomeText.BeginAnimation(OpacityProperty, blinkAnimation);
        }

        /// <summary>
        /// Animation de flottement du logo une fois descendu (optionnel).
        /// </summary>
        private void StartFloatingAnimation()
        {
            var floatAnimation = new ThicknessAnimation
            {
                From = new Thickness(0, 100, 0, 0),
                To = new Thickness(0, 80, 0, 0),
                Duration = TimeSpan.FromSeconds(1.5),
                AutoReverse = true, // Reviens à la position initiale
                RepeatBehavior = RepeatBehavior.Forever // Boucle infinie
            };

            Logo.BeginAnimation(MarginProperty, floatAnimation);
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (_keyPressed) return; // Empêche plusieurs appuis
            if (!_animationCompleted) return; // Attendre la fin de l'animation

            _keyPressed = true;

            // Navigation vers la vue "Settings" (dans la même fenêtre)
            var parentWindow = Window.GetWindow(this); // Récupère la fenêtre parente
            if (parentWindow != null && parentWindow is MainWindow mainWindow)
            {
                mainWindow.MainContent.Content = new Settings(); // Remplace le contenu par la vue "Settings"
            }
        }
    }
}
