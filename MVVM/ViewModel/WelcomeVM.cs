using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace PokeWish.MVVM.ViewModel
{
    public class WelcomeVM : BaseVM
    {
        private bool _isAnimationSkipped;

        public bool IsAnimationSkipped
        {
            get => _isAnimationSkipped;
            set
            {
                _isAnimationSkipped = value;
                OnPropertyChanged(nameof(IsAnimationSkipped));
            }
        }

        public ICommand SkipAnimationCommand { get; }

        public WelcomeVM()
        {
            SkipAnimationCommand = new RelayCommand(OnSkipAnimation);
        }

        private void OnSkipAnimation()
        {
            IsAnimationSkipped = true;
        }
    }
}
