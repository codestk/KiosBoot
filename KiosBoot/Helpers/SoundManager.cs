using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage.Pickers;
using Windows.UI.Xaml.Controls;

namespace KiosBoot
{
    public static class SoundManager
    {
        //private static MediaPlayer _mediaPlayer = new MediaPlayer();
        //private static MediaPlayer _effectPlayer = new MediaPlayer();
        public static MediaPlayerElement _mediaPlayer = new MediaPlayerElement();
        private static MediaPlayerElement _effectPlayer = new MediaPlayerElement();
       
        public static async void OpenMusicAsync(string relativePath)
        {
           // OpenFileDialog openFileDialog = new OpenFileDialog();

            FileOpenPicker openFileDialog =new Windows.Storage.Pickers.FileOpenPicker();
            //openFileDialog.FileTypeFilter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";
            openFileDialog.FileTypeFilter.Add(".wmv");
            openFileDialog.FileTypeFilter.Add(".mp4");
            openFileDialog.FileTypeFilter.Add(".wma");
            openFileDialog.FileTypeFilter.Add(".mp3");

            var file = await openFileDialog.PickSingleFileAsync();

            // mediaPlayer is a MediaPlayerElement defined in XAML
            if (file != null)
            {
                //_mediaPlayer.Open(new Uri(openFileDialog.f));
                //_mediaPlayer.Play();
                _mediaPlayer.Source = MediaSource.CreateFromStorageFile(file);
                _mediaPlayer.MediaPlayer.Play();
            }
            //if (openFileDialog() == true)
            //{
            //    _mediaPlayer.Open(new Uri(openFileDialog.f));
            //    _mediaPlayer.Play();
            //}
        }

        public static void PlayBackgroundMusic()
        {

            //new Uri(Path.Combine(Environment.CurrentDirectory, "Assets/background_music.mp3"))
            Uri pathUri = new Uri("ms-appx:///Assets/background_music.mp3");
            _mediaPlayer.Source = MediaSource.CreateFromUri(pathUri);
            _mediaPlayer.MediaPlayer.IsLoopingEnabled = true;
            _mediaPlayer.MediaPlayer.Play();

        }

        public static void PlayCardFlip()
        {
            PlayEffect("card_flip.mp3");
        }

        public static void PlayCorrect()
        {
            PlayEffect("correct_match.mp3");
        }

        public static void PlayIncorrect()
        {
            PlayEffect("incorrect_match.mp3");
        }

        private static void PlayEffect(string fileName)
        {
            //_effectPlayer.Open(new Uri(Path.Combine(Environment.CurrentDirectory, "Assets/SoundEffects/" + fileName)));
            //_effectPlayer.Play();

            Uri pathUri = new Uri("ms-appx:///Assets/SoundEffects/"+ fileName);
            _effectPlayer.Source = MediaSource.CreateFromUri(pathUri);
            _effectPlayer.MediaPlayer.Play();
        }
    }
}
