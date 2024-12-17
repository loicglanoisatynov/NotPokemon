using System;
using System.Windows.Media;

namespace PokeWish.MVVM.Services
{
    public class MusicService
    {
        private readonly MediaPlayer _mediaPlayer;
        public bool IsPlaying { get; private set; }

        public MusicService()
        {
            _mediaPlayer = new MediaPlayer
            {
                Volume = 0.5
            };

            _mediaPlayer.Open(new Uri("pack://siteoforigin:,,,/assets/sound/openingv3.mp3"));
        }

        public void Play()
        {
            if (!IsPlaying)
            {
                _mediaPlayer.Play();
                IsPlaying = true;
            }
        }

        public void Pause()
        {
            if (IsPlaying)
            {
                _mediaPlayer.Pause();
                IsPlaying = false;
            }
        }

        public void Stop()
        {
            _mediaPlayer.Stop();
            IsPlaying = false;
        }

        public void TogglePlayPause()
        {
            if (IsPlaying)
            {
                Pause();
            }
            else
            {
                Play();
            }
        }

        public void SetVolume(double volume)
        {
            _mediaPlayer.Volume = volume;
        }
    }
}
