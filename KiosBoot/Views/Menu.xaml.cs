using KiosBoot.Models;
using KiosBoot.Views.Game;
using ServiceHelpers;
using System;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace KiosBoot.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Menu : Page
    {
        public Menu()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = Windows.UI.Xaml.Navigation.NavigationCacheMode.Enabled;

            ApplicationView view = ApplicationView.GetForCurrentView();
            if (!view.IsFullScreenMode)
            {
                view.TryEnterFullScreenMode();
            }


            //if (FaceObjct.MyFace != null)
            //{
            //    //face.FaceAttributes.Gender
            //    string gender = FaceObjct.MyFace.FaceAttributes.Gender;

            //    ImageAnalyzer img = this.DataContext as ImageAnalyzer;
            //    if (img != null)
            //    {
            //        img.UpdateDecodedImageSize(100, 100);
            //    }

            //    if (string.Compare(gender, "male", true) == 0)
            //    {
            //        this.SourceImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Animate/male.png"));
            //        Greeting.Text = "ยินดีต้อนรับครับ";
            //    }
            //    else if (string.Compare(gender, "female", true) == 0)
            //    {
            //        //this.SourceImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Animate/female.png"));

            //        this.SourceImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Animate/main@2x.png"));
            //        Greeting.Text = "ยินดีต้อนรับค่ะ";
                
            //    }

            //    //if มี Name
            //    if (FaceObjct.faceIdIdentification != null)
            //    {
            //        string Name = FaceObjct.faceIdIdentification.Person.Name;
            //        Greeting.Text = "ยินดีต้อนรับคุณ " + Name + " ";
            //    }
            //}
        }

        private void onIsIdleChanged(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"IsIdle: {App.Current.IsIdle}");
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //if (e.NavigationMode == NavigationMode.Back)
            //    EntranceAnimation.Edge = EdgeTransitionLocation.Right;
            App.Current.IsIdleChanged += onIsIdleChanged;



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
            App.Current.IsIdleChanged -= onIsIdleChanged;
            //ConnectedAnimationService.GetForCurrentView()
            //    .PrepareToAnimate("forwardAnimation", SourceImage);
            // You don't need to explicitly set the Configuration property because
            // the recommended Gravity configuration is default.
            // For custom animation, use:
            // animation.Configuration = new BasicConnectedAnimationConfiguration();

            ConnectedAnimationService.GetForCurrentView()
      .PrepareToAnimate("forwardAnimation", SourceImage);

        }

        private void btn_Screen_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(MediaPlayerPage), null, new DrillInNavigationTransitionInfo());
        }

        private void btn_Game_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(gameLv10), null, new SuppressNavigationTransitionInfo());

          //  this.Frame.Navigate(typeof(gameLv10));
        }

        private void btn_Product_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Service), null, new EntranceNavigationTransitionInfo());
        }

        private void btn_SettingFace_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(FaceIdentificationSetup), null, new EntranceNavigationTransitionInfo());
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AutomaticPhotoCapturePage), null, new EntranceNavigationTransitionInfo());
            //this.Frame.Navigate(typeof(AutomaticPhotoCapturePage), null, new EntranceNavigationTransitionInfo());
        }
    }
}
