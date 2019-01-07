using KiosBoot.Helpers.Config;
using KiosBoot.Helpers.Instance;
using KiosBoot.Helpers.Server;
using KiosBoot.Models;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace KiosBoot.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Product : Page
    {
        public Product()
        {
            this.InitializeComponent();

            myStoryboard.Begin();

       
        }
 


        public void CallData()
        {


            //string url = DataConfig.ApiDomain() + "/cockpit/api/collections/get/GameTypeA";
            ////string url = DataConfig.ApiDomain() + "/cockpit/api/collections/get/GameTypeB";
            ////string url = DataConfig.ApiDomain() + "/cockpit/api/collections/get/GameTypeC";

            //for 1
           
            ShowTopic(btnTopic1, "1");
            //ShowTopic(btnTopic2, "2");
            //ShowTopic(btnTopic3, "3");
            //ShowTopic(btnTopic4, "4");
            //ShowTopic(btnTopic5, "5");
            //ShowTopic(btnTopic6, "6");
            //ShowTopic(btnTopic7, "7");
            //ShowTopic(btnTopic8, "8");
        }

        private void ShowTopic(Button btnTopic, string item)
        {
            var api = new ApiData();
            string url = TopicInstance.GetCollectionDefinationUrl(item);
            var result = Task.Run(() => api.GetDataFromServerAsync(url)).Result;
            Topic topic = JsonConvert.DeserializeObject<Topic>(result);

            if (topic.ItemsCount == 0)
            {
                btnTopic.Visibility = Visibility.Collapsed;
            }
            else
            {
                btnTopic.Content = topic.Description;
                btnTopic.Visibility = Visibility.Visible;
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Menu), null, new DrillInNavigationTransitionInfo());
        }

        #region Effecft

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                ConnectedAnimationService.GetForCurrentView()
                    .PrepareToAnimate("backAnimation", circle);

                // Use the recommended configuration for back animation.
                //animation.Configuration = new DirectConnectedAnimationConfiguration();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            CallData();
            //if (e.NavigationMode == NavigationMode.Back)
            //    EntranceAnimation.Edge = EdgeTransitionLocation.Top;

            ConnectedAnimation animation =
                ConnectedAnimationService.GetForCurrentView().GetAnimation("forwardAnimation");
            if (animation != null)
            {
                animation.TryStart(circle);
            }

            base.OnNavigatedTo(e);
        }

        #endregion Effecft

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case "btnTopic1":

                    TopicInstance.CurrentTopic = 1;

                    DemoRedirect();
                    break;
                case "btnTopic2":
                    TopicInstance.CurrentTopic = 2;
                    DemoRedirect();
                    break;
                case "btnTopic3":
                    TopicInstance.CurrentTopic = 3;
                    DemoRedirect();
                    break;
                case "btnTopic4":
                    TopicInstance.CurrentTopic = 4;
                    DemoRedirect();
                    break;
                case "btnTopic5":
                    TopicInstance.CurrentTopic = 5;
                    DemoRedirect();
                    break;
                case "btnTopic6":
                    TopicInstance.CurrentTopic = 6;
                    DemoRedirect();
                    break;
                case "btnTopic7":
                    TopicInstance.CurrentTopic = 7;
                    DemoRedirect();
                    break;
                case "btnTopic8":
                    TopicInstance.CurrentTopic = 8;
                    DemoRedirect();
                    break;
                default:
                    break;
            }
        }

        private void DemoRedirect()
        {
            this.Frame.Navigate(typeof(MasterDetailSelection), null, new DrillInNavigationTransitionInfo());
        }
    }
}
