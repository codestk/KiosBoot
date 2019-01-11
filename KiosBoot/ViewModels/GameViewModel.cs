using KiosBoot.Helpers.Profile;
using KiosBoot.Models;
using System;
using Windows.UI.Xaml.Controls;

namespace KiosBoot.ViewModels
{
    public enum SlideCategories
    {
        Lv10,
        Lv16,
        Lv24

        //Animals,
        //Cars,
        //Foods
    }

    public class GameViewModel : ObservableObject
    {
        //Collection of slides we are playing with
        public SlideCollectionViewModel Slides { get; private set; }

        //Game information scores, attempts etc
        public GameInfoViewModel GameInfo { get; private set; }

        //Game timer for elapsed time
        public TimerViewModel Timer { get; private set; }

        private ListView _Scrollinfo;

        private Canvas _GameBox;

        //Category we are playing in
        public SlideCategories Category { get; private set; }

        public GameViewModel(SlideCategories category, ListView Scrollinfo, Canvas GameBox)
        {
            Category = category;
            _Scrollinfo = Scrollinfo;
            _Scrollinfo.ItemsSource = ScrollGame.GetScrollInfo();
            _GameBox = GameBox;
            //SetupGame(category); //Recover
        }

        //Initialize game essentials
        private void SetupGame(SlideCategories category)
        {
            Slides = new SlideCollectionViewModel();
            Timer = new TimerViewModel(new TimeSpan(0, 0, 1));
            GameInfo = new GameInfoViewModel();

            //Set attempts to the maximum allowed
            GameInfo.ClearInfo();

            //Create slides from image folder then display to be memorized
            //Slides.CreateSlides("Assets/Game/" + category.ToString());

            Slides.CreateSlides(category);

            Slides.Memorize();

            //Game has started, begin count.

            Timer.Start();

            foreach (Control ctrl in _GameBox.Children)
            {
                ctrl.IsEnabled = true;
            }

            //Slides have been updated
            OnPropertyChanged("Slides");
            OnPropertyChanged("Timer");
            OnPropertyChanged("GameInfo");

            //foreach (Control ctrl in _GameBox.Children)
            //{
            //    ctrl.IsEnabled = true;
            //}
        }

        //Slide has been clicked
        public void ClickedSlide(object slide)
        {
            if (Slides.canSelect)
            {
                var selected = slide as PictureViewModel;
                Slides.SelectSlide(selected);
            }

            if (!Slides.areSlidesActive)
            {
                if (Slides.CheckIfMatched())
                    GameInfo.Award(); //Correct match
                else
                    GameInfo.Penalize();//Incorrect match
            }

            GameStatus();
        }

        //Status of the current game
        private void GameStatus()
        {
            if (GameInfo.MatchAttempts == 0)
            {
                GameInfo.GameStatus(false);
                Slides.RevealUnmatched();
                Timer.Stop();

                foreach (Control ctrl in _GameBox.Children)
                {
                    ctrl.IsEnabled = false;
                }
            }

            if (Slides.AllSlidesMatched)
            {
                GameInfo.GameStatus(true);
                Timer.Stop();
                foreach (Control ctrl in _GameBox.Children)
                {
                    ctrl.IsEnabled = false;
                }

                //Add คะแนนใหม่

                string Path = UserProfile.PicturePath;

                // var result = Task.Run(() => api.GetDataFromServerAsync(url)).Result;

                ScrollGame.AddScrollInfo(GameInfo.Score, Path);
                _Scrollinfo.ItemsSource = ScrollGame.GetScrollInfo();
            }
        }

        //Restart game
        public void Restart()
        {
            SoundManager.PlayIncorrect();
            SetupGame(Category);
        }

        public void StopGame()
        {
            GameInfo.GameStatus(false);
            Slides.RevealUnmatched();
            Timer.Stop();

            foreach (Control ctrl in _GameBox.Children)
            {
                ctrl.IsEnabled = false;
            }

            //Timer.Stop();
            //GameInfo.ClearInfo();
        }
    }
}
