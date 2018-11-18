using KiosBoot.Views.Game;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace KiosBoot.Views
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {

        public MainPage()
        {
            this.InitializeComponent();

            // Handling Page Back navigation behaviors
            SystemNavigationManager.GetForCurrentView().BackRequested +=
                SystemNavigationManager_BackRequested;
        }

        private void SystemNavigationManager_BackRequested(
            object sender,
            BackRequestedEventArgs e)
        {
            if (!e.Handled)
            {
                e.Handled = this.BackRequested();
            }
        }

        public Frame AppFrame { get { return this.Frame; } }

        private bool BackRequested()
        {
            // Get a hold of the current frame so that we can inspect the app back stack
            if (this.AppFrame == null)
                return false;

            // Check to see if this is the top-most page on the app back stack
            if (this.AppFrame.CanGoBack)
            {
                // If not, set the event to handled and go back to the previous page in the
                // app.
                this.AppFrame.GoBack();
                return true;
            }
            return false;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //if (e.NavigationMode == NavigationMode.Back)
            //    EntranceAnimation.Edge = EdgeTransitionLocation.Right;

            ConnectedAnimation animation =
              ConnectedAnimationService.GetForCurrentView().GetAnimation("backAnimation");
            if (animation != null)
            {
                animation.TryStart(SourceImage);
            }


            base.OnNavigatedTo(e);
        }



        private void SourceImage_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            // Navigate to detail page.
            // Suppress the default animation to avoid conflict with the connected animation.
            Frame.Navigate(typeof(MediaPlayerPage), null, new SuppressNavigationTransitionInfo());
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            ConnectedAnimationService.GetForCurrentView()
                .PrepareToAnimate("forwardAnimation", SourceImage);
            // You don't need to explicitly set the Configuration property because
            // the recommended Gravity configuration is default.
            // For custom animation, use:
            // animation.Configuration = new BasicConnectedAnimationConfiguration();
        }






        private void btn_Screen_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MediaPlayerPage), null, new DrillInNavigationTransitionInfo());
        }

        private void btn_Game_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MainWindow), null, new SuppressNavigationTransitionInfo());
        }

        private void btn_Product_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Product), null, new EntranceNavigationTransitionInfo());
        }




        //public MainPage()
        //{
        //    InitializeComponent();
        //}

        public event PropertyChangedEventHandler PropertyChanged;

        private void Set<T>(ref T storage, T value, [CallerMemberName]string propertyName = null)
        {
            if (Equals(storage, value))
            {
                return;
            }

            storage = value;
            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
