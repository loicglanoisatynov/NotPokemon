using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using PokeWish.MVVM.Services;

namespace PokeWish.MVVM.ViewModel
{
    public class MusicControlVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;



        private readonly MusicService _musicService;

        public ICommand ToggleMusicCommand { get; }

        private string _playPauseText = string.Empty;
        public string PlayPauseText
        {
            get => _playPauseText;
            set
            {
                _playPauseText = value;
                OnPropertyChanged(nameof(PlayPauseText));
            }
        }

        private string _playPauseImage = "/assets/images/sound-on-music.png";
        public string PlayPauseImage
        {
            get => _playPauseImage;
            set
            {
                _playPauseImage = value;
                OnPropertyChanged(nameof(PlayPauseImage));
            }
        }

        private double _volume;
        public double Volume
        {
            get => _volume;
            set
            {
                _volume = value;
                _musicService.SetVolume(value);
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Volume)));
                //MessageBox.Show("Volume: " + value);
            }
        }

        public MusicControlVM()
        {
            _musicService = App.MusicService;
            ToggleMusicCommand = new RelayCommand(ToggleMusic);

            PlayPauseText = _musicService.IsPlaying ? "Pause" : "Play";

            Volume = 0.5;
        }

        private void ToggleMusic()
        {
            _musicService.TogglePlayPause();
            PlayPauseText = _musicService.IsPlaying ? "Pause" : "Play";
            PlayPauseImage = _musicService.IsPlaying


                ? "/assets/images/sound-on-music.png" // Chemin de l'icône "Pause"
                : "/assets/images/mute-audio-play.png"; // Chemin de l'icône "Play"

            
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
