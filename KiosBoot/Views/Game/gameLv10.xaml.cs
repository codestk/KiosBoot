using KiosBoot.Helpers.Profile;
using KiosBoot.Models;
using KiosBoot.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace KiosBoot.Views.Game
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class gameLv10 : Page
    {
        public gameLv10()
        {
            this.InitializeComponent();
            DataContext = new StartMenuViewModel(this);

            var startMenu = DataContext as StartMenuViewModel;
            //0 lv10
            //1 lv16
            //2 lv26
            startMenu.StartNewGame(0, Scrollinfo, GameMainBlock);

            //Menu.Visibility = Visibility.Collapsed;
            Game.Visibility = Visibility.Visible;
           
            ImageBrush imgBrush = new ImageBrush();
            imgBrush.ImageSource = UserProfile.PictureProfile;
            userPicture.Fill = imgBrush;
            //userPicture.Source = UserProfile.PictureProfile;

            //ScrollGame.AddScrollInfo(255, "http://www.javascriptthai.com/wp-content/uploads/2013/08/JavaScriptThai174_131-1.png");
        }






         



        private void Slide_Clicked(object sender, RoutedEventArgs e)
        {
            var game = DataContext as GameViewModel;
            var button = sender as Button;
            game.ClickedSlide(button.DataContext);


        }

        private void PlayAgain_C(object sender, RoutedEventArgs e)
        {
            var game = DataContext as GameViewModel;

            GameInstance.CurrentGame = game;


            game.Restart();


        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            //GameInstance.CurrentGame = null;

            SoundManager._mediaPlayer.MediaPlayer.Pause();
            this.Frame.Navigate(typeof(Menu));
        }


        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
           
            GameInstance.CurrentGame.StopGame();
            base.OnNavigatingFrom(e);
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
    

            
            base.OnNavigatedTo(e);
        }



    }
}
