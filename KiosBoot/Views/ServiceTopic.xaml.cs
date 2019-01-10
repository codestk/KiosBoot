using KiosBoot.Helpers.Config;
using KiosBoot.Helpers.Server;
using KiosBoot.ViewModels;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace KiosBoot.Views
{
    public class TopicHead
    {
        public string Title { get; set; }
        public int Sort { get; set; }

        public string itemId { get; set; }
        //public int Age { get; set; }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ServiceTopic : Page
    {
        private List<TopicHead> listOfHead = new List<TopicHead>();

        public ServiceTopic()
        {
            this.InitializeComponent();


           
                ApplicationView view = ApplicationView.GetForCurrentView();
                if (!view.IsFullScreenMode)
                {
                    view.TryEnterFullScreenMode();
                }
       



            //listOfHead.Add(new TopicHead { Title = "Heading1…………………………………………", Sort = 20, itemId = "AAAA" });
            //listOfHead.Add(new TopicHead { Title = "Heading2…………………………………………", Sort = 21, itemId ="AAAA" });
            //listOfHead.Add(new TopicHead { Title = "Heading3…………………………………………", Sort = 19, itemId = "AAAA" });
            //listOfHead.Add(new TopicHead { Title = "Heading4…………………………………………", Sort = 18, itemId = "AAAA" });
            //listOfHead.Add(new TopicHead { Title = "Heading5…………………………………………", Sort = 20, itemId = "AAAA" });
            //listOfHead.Add(new TopicHead { Title = "Heading6…………………………………………", Sort = 20, itemId = "AAAA" });
            //listOfHead.Add(new TopicHead { Title = "Heading7…………………………………………", Sort = 21, itemId = "AAAA" });
            //listOfHead.Add(new TopicHead { Title = "Heading2…………………………………………", Sort = 21, itemId = "AAAA" });
            //listOfHead.Add(new TopicHead { Title = "Heading8…………………………………………", Sort = 20, itemId = "AAAA" });
            //listOfHead.Add(new TopicHead { Title = "Heading9…………………………………………", Sort = 23, itemId = "AAAA" });
            //listOfHead.Add(new TopicHead { Title = "Heading1…………………………………………", Sort = 20, itemId = "AAAA" });
            //listOfHead.Add(new TopicHead { Title = "Heading2…………………………………………", Sort = 21, itemId = "AAAA" });
            //listOfHead.Add(new TopicHead { Title = "Heading3…………………………………………", Sort = 20, itemId = "AAAA" });
            //listOfHead.Add(new TopicHead { Title = "Heading4…………………………………………", Sort = 23, itemId = "AAAA" });
            //listOfHead.Add(new TopicHead { Title = "Heading5…………………………………………", Sort = 20, itemId = "AAAA" });
            //listOfHead.Add(new TopicHead { Title = "Heading6…………………………………………", Sort = 21, itemId = "AAAA" });
            //listOfHead.Add(new TopicHead { Title = "Heading7…………………………………………", Sort = 20, itemId = "AAAA" });
            //listOfHead.Add(new TopicHead { Title = "Heading8…………………………………………", Sort = 23, itemId = "AAAA" });
            //listOfHead.Add(new TopicHead { Title = "Heading9…………………………………………", Sort = 20, itemId = "AAAA" });
            //listOfHead.Add(new TopicHead { Title = "Heading1…………………………………………", Sort = 21, itemId = "AAAA" });
            //listOfHead.Add(new TopicHead { Title = "Heading2…………………………………………", Sort = 20, itemId = "AAAA" });
            //listOfHead.Add(new TopicHead { Title = "Heading3…………………………………………", Sort = 23, itemId ="AAAA" });
            //listOfHead.Add(new TopicHead { Title = "Heading4…………………………………………", Sort = 20, itemId = "AAAA" });

            //ListView1.ItemsSource = listOfHead;
        }

        private void ListView1_ItemClick(object sender, ItemClickEventArgs e)
        {

            string id = ((KiosBoot.Views.TopicHead)e.ClickedItem).itemId.ToString();
            //this.Frame.Navigate(typeof(ServiceTopicDetail),id );

            //ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("image", ListView1);
            //this.Frame.Navigate(typeof(ServiceTopicDetail), id,new DrillInNavigationTransitionInfo());

            string Data = Topic + "|" + id;
            this.Frame.Navigate(typeof(CardPage), Data, new DrillInNavigationTransitionInfo());



            switch (e.ClickedItem.ToString())
            {
                case "Page 1":
                    //this.Frame.Navigate(typeof(Page1));
                    break;

                case "Page 2":
                    //this.Frame.Navigate(typeof(Page2));
                    break;

                case "Page 3":
                    //this.Frame.Navigate(typeof(Page3));
                    break;

                case "Page 4":
                    //this.Frame.Navigate(typeof(Page4));
                    break;

                case "Page 5":
                    //this.Frame.Navigate(typeof(Page5));
                    break;

                default:
                    break;
            }
        }
        string Topic = "";

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
           // base.OnNavigatedTo(e);

              Topic = "";
            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter))
            {
                Topic = e.Parameter.ToString();

                string[] UrlAndHead = Topic.Split('|');

                Topic = UrlAndHead[0];
                HeadTopic.Text = UrlAndHead[1];
            }



            //
            string url = DataConfig.ApiDomain() + "/api/collections/get/"+ Topic;
            var api = new ApiData();
            var result = Task.Run(() => api.GetDataFromServerAsync(url)).Result;
            TopicModel _TopicModel = JsonConvert.DeserializeObject<TopicModel>(result);


            //ListView1.ItemsSource = picture.Entries;

            foreach (var item in _TopicModel.Entries)
            {
                listOfHead.Add(new TopicHead { Title = item.TopicHeading, Sort = 20, itemId = item .Id});
            }
          

            ListView1.ItemsSource = listOfHead;

            //HeadTopic.Text = "Topic1";

            //ConnectedAnimation animation =
            //    ConnectedAnimationService.GetForCurrentView().GetAnimation("forwardAnimation");
            //if (animation != null)
            //{
            //    animation.TryStart(ListView1);
            //}

            base.OnNavigatedTo(e);
        }

        private void Back_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            //, new DrillInNavigationTransitionInfo());
            this.Frame.Navigate(typeof(Service), null, new DrillInNavigationTransitionInfo());

        }
    }
}
