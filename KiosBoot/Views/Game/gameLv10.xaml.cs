using KiosBoot.Helpers.Profile;
using KiosBoot.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
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

            Scrol.Visibility = Visibility.Collapsed;
            Game.Visibility = Visibility.Collapsed;
            Information.Visibility = Visibility.Collapsed;
            Game.Visibility = Visibility.Collapsed;

            //GameOverPanel.Visibility = Visibility.Collapsed;

            if (UserProfile.PictureProfile != null)
            {
                ImageBrush imgBrush = new ImageBrush();
                imgBrush.ImageSource = UserProfile.PictureProfile;
                userPicture.Fill = imgBrush;
            }
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
            //this.Frame.Navigate(typeof(Menu));
            if (GameInstance.CurrentGame.GameInfo != null)
            { GameInstance.CurrentGame.StopGame(); }
            this.Frame.Navigate(typeof(Menu), null, new SlideNavigationTransitionInfo());
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            GameInstance.CurrentGame.StopGame();
            base.OnNavigatingFrom(e);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            ConnectedAnimation animation =
      ConnectedAnimationService.GetForCurrentView().GetAnimation("forwardAnimation");
            if (animation != null)
            {
                animation.TryStart(userPicture);
            }
        }

        #region Btn

        private void BtnPause_Click(object sender, RoutedEventArgs e)
        {
            GameInstance.CurrentGame.Timer.Stop();
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            GameInstance.CurrentGame.Timer.Start();
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            GameInstance.CurrentGame.StopGame();
        }

        #endregion Btn

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            PanelStartGame.Visibility = Visibility.Collapsed;

            //ได้มาตอน Start Game

            GameInstance.CurrentGame.Restart();

            Scrol.Visibility = Visibility.Visible;
            Game.Visibility = Visibility.Visible;
            Information.Visibility = Visibility.Visible;
            Game.Visibility = Visibility.Visible;
        }
    }
}
