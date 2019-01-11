using System;
using System.Collections.Generic;
using System.Linq;
using KiosBoot.Helpers.Profile;
using KiosBoot.Views.Game;
using Windows.UI.Xaml.Controls;

namespace KiosBoot.ViewModels
{
    public class StartMenuViewModel
    {
        private Page _mainWindow;
        public StartMenuViewModel(Page main)
        {
            _mainWindow = main;
            SoundManager.PlayBackgroundMusic();
        }

        public void StartNewGame(int categoryIndex,ListView Scrollinfo,Canvas gamebox)
        {
            //0 = 5
            //1 = 8
            //2 = 26
            var category = (SlideCategories)categoryIndex;
            GameViewModel newGame = new GameViewModel(category, Scrollinfo, gamebox);

            GameInstance.CurrentGame = newGame;
            //GameInstance.CurrentGame.StopGame();




            _mainWindow.DataContext = newGame;

            
        }
    }
}
