using KiosBoot.Helpers.Profile;
using System;
using Windows.UI.Xaml;

namespace KiosBoot.ViewModels
{
    public class TimerViewModel : ObservableObject
    {
        private DispatcherTimer _playedTimer;
        private TimeSpan _timePlayed;

        //private const int _playSeconds = 1;

        public TimeSpan Time
        {
            get
            {
                return _timePlayed ;
            }
            set
            {
                _timePlayed = value;
                OnPropertyChanged("Time");
            }
        }

        public TimerViewModel(TimeSpan time)
        {
            _playedTimer = new DispatcherTimer();
            _playedTimer.Interval = time;
            _playedTimer.Tick += PlayedTimer_Tick;
            _timePlayed = new TimeSpan();
        }

        public void Start()
        {
            _playedTimer.Start();
        }

        public void Stop()
        {


            
           _playedTimer.Stop();
            _playedTimer = new DispatcherTimer();
        }

        private void PlayedTimer_Tick(object sender, object e)
        {
            Time = _timePlayed.Add(new TimeSpan(0, 0, 1));

            //Set Time
            if  (Time.Seconds == 30)
            {

                _timePlayed = new TimeSpan(0, 0,0);
                Time = _timePlayed;
                //OnPropertyChanged("LostMessage");
                //GameInfo.GameStatus(false);
                //Slides.RevealUnmatched();
                //Timer.Stop();

                GameInstance.CurrentGame.StopGame();
            }
        }
    }
}
