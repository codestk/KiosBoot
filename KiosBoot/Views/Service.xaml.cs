using KiosBoot.Helpers.Config;
using KiosBoot.Helpers.Server;
using KiosBoot.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace KiosBoot.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Service : Page
    {
        public Service()
        {
            this.InitializeComponent();
            ApplicationView view = ApplicationView.GetForCurrentView();
            if (!view.IsFullScreenMode)
            {
                view.TryEnterFullScreenMode();
            }


        }

        void CustomButon(string topicID,Button btn)
        {
            var api = new ApiData();
           


        
            string url2 = DataConfig.ApiDomain() + "/api/collections/get/"+ topicID;

            var result2 = Task.Run(() => api.GetDataFromServerAsync(url2)).Result;
            TopicModel topicitem = JsonConvert.DeserializeObject<TopicModel>(result2);


            if (topicitem.Total == 0)
            {
                //Set Logo
                // < Button.Background >
                // < ImageBrush ImageSource = "ms-appx:///Assets/Service/circle1.png" Stretch = "UniformToFill" />

                //</ Button.Background >
                ImageBrush brush1 = new ImageBrush();
                brush1.ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/logo/dgaLogo.png"));
                brush1.Stretch = Stretch.None;
                btn.Background = brush1;


                //Style redButtonStyle = (Style)this.Resources["ButtonBorderThemeThickness"];
                //btn.IsEnabled = false;
                //ButtonStyleNoHighlighting
                Style ButtonStyleNoHighlighting = (Style)Application.Current.Resources["ButtonStyleNoHighlighting"];
                btn.Style = ButtonStyleNoHighlighting;
               
                btn.Content = "";


            }
            else
            {
                string url1 = DataConfig.ApiDomain() + "/api/collections/collection/" + topicID;

                var result1 = Task.Run(() => api.GetDataFromServerAsync(url1)).Result;
                TopicCollectionModel collectionTopic1 = JsonConvert.DeserializeObject<TopicCollectionModel>(result1);

                btn.Content = collectionTopic1.Label.ToString() ;

              
                Style redButtonStyle = (Style)Application.Current.Resources["CircleButton"];
                btn.Style = redButtonStyle;

                btn.IsEnabled = true;
               
            }

        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //if (e.NavigationMode == NavigationMode.Back)
            //  EntranceAnimation.Edge = EdgeTransitionLocation.Right;
            //var api = new ApiData();
            //string url1 = DataConfig.ApiDomain() + "/api/collections/collection/TopicX1";

            //var result1 = Task.Run(() => api.GetDataFromServerAsync(url1)).Result;
            //TopicCollectionModel collectionTopic1 = JsonConvert.DeserializeObject<TopicCollectionModel>(result1);

            //if ()
            //Topic1.Content = collectionTopic1.Label;


            //string url2 = DataConfig.ApiDomain() + "/api/collections/collection/TopicX2";

            //var result2 = Task.Run(() => api.GetDataFromServerAsync(url2)).Result;
            //TopicCollectionModel picture2 = JsonConvert.DeserializeObject<TopicCollectionModel>(result2);


            //string url3 = DataConfig.ApiDomain() + "/api/collections/collection/TopicX3";

            //var result3 = Task.Run(() => api.GetDataFromServerAsync(url3)).Result;
            //TopicCollectionModel picture3 = JsonConvert.DeserializeObject<TopicCollectionModel>(result3);

            //Set ตุ่ม
            CustomButon("TopicX1", Topic1);
            CustomButon("TopicX2", Topic2);
            CustomButon("TopicX3", Topic3);

            EntranceAnimation.Edge = EdgeTransitionLocation.Left;
            base.OnNavigatedTo(e);

            ConnectedAnimation animation =
      ConnectedAnimationService.GetForCurrentView().GetAnimation("forwardAnimation");
            if (animation != null)
            {
                animation.TryStart(Topic3);
            }
        }


        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            //ConnectedAnimationService.GetForCurrentView()
            //    .PrepareToAnimate("forwardAnimation", Topic1);
            // You don't need to explicitly set the Configuration property because
            // the recommended Gravity configuration is default.
            // For custom animation, use:
            // animation.Configuration = new BasicConnectedAnimationConfiguration();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Menu), null, new EntranceNavigationTransitionInfo());
        }

        private void Topic1_Click(object sender, RoutedEventArgs e)
        {

            if (Topic1.Content == "")
                return;

            this.Frame.Navigate(typeof(ServiceTopic), "TopicX1|" + Topic1.Content, new DrillInNavigationTransitionInfo());
           // Frame.Navigate(typeof(AlbumPage), albumId, new DrillInNavigationTransitionInfo());
        }

        private void Topic2_Click(object sender, RoutedEventArgs e)
        {
            if (Topic2.Content == "")
                return;

            this.Frame.Navigate(typeof(ServiceTopic), "TopicX2|"+ Topic2.Content, new DrillInNavigationTransitionInfo());
        }

        private void Topic3_Click(object sender, RoutedEventArgs e)
        {
            if (Topic3.Content == "")
                return;
            this.Frame.Navigate(typeof(ServiceTopic), "TopicX3|" + Topic3.Content, new DrillInNavigationTransitionInfo());
        }
    }
}
