﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Windows.UI.Xaml;

namespace KiosBoot.ViewModels
{
    public class GameInfoViewModel : ObservableObject
    {
        private const int _maxAttempts = 10;   
        private const int _pointAward = 1;
        private const int _pointDeduction = 1;

        private int _matchAttempts;
        private int _score;

        private bool _gameLost;
        private bool _gameWon;

        public int MatchAttempts
        {
            get
            {
                return _matchAttempts;
            }
            private set
            {
                _matchAttempts = value;
                OnPropertyChanged("MatchAttempts");
            }
        }

        public int Score
        {
            get
            {
                return _score;
            }
            private set
            {
                _score = value;
                OnPropertyChanged("Score");
            }
        }

        public Visibility LostMessage
        {
            get
            {
                if (_gameLost)
                    return Visibility.Visible;

                return Visibility.Collapsed;
            }
        }

        public Visibility WinMessage
        {
            get
            {
                if (_gameWon)
                    return Visibility.Visible;

                return Visibility.Collapsed;
            }
        }


        public Visibility ShowFrame
        {
            get
            {
                if ((_gameWon)||(_gameLost))
                    return Visibility.Visible;

                return Visibility.Collapsed;
            }
        }


        public void GameStatus(bool win)
        {
          
            if (!win)
            {
  
                _gameLost = true;
                OnPropertyChanged("LostMessage");
               
            }

            if (win)
            {
                _gameWon = true;
                OnPropertyChanged("WinMessage");
               
            }

            OnPropertyChanged("ShowFrame");
        }

        public void ClearInfo()
        {
            Score = 0;
            MatchAttempts = _maxAttempts;
            _gameLost = false;
            _gameWon = false;
            OnPropertyChanged("LostMessage");
            OnPropertyChanged("WinMessage");
        }

        public void Award()
        {
            Score += _pointAward;
            SoundManager.PlayCorrect();
        }

        public void Penalize()
        {
            Score -= _pointDeduction;
            MatchAttempts--;
            SoundManager.PlayIncorrect();
        }
    }
}
