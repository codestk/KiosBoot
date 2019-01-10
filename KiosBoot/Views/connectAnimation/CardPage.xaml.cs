using AppUIBasics.ControlPages;
using KiosBoot.Helpers;
using KiosBoot.Helpers.Config;
using KiosBoot.Helpers.Server;
using KiosBoot.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.UI.ViewManagement;
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
    public class MenuItem
    {
        public string IName
        {
            get;
            set;
        }

        public string image
        {
            get;
            set;
        }

        private MediaElement _video;

        public MediaElement Video
        {
            get { return _video; }
            set
            {
                _video = value;
                //NotifyChanged("Video");
            }
        }

        public Uri PathVdo
        {
            get;
            set;
        }
    }

    public sealed partial class CardPage : Page
    {
        private Object _storedItem;

        public CardPage()
        {
            this.InitializeComponent();


            ApplicationView view = ApplicationView.GetForCurrentView();
            if (!view.IsFullScreenMode)
            {
                view.TryEnterFullScreenMode();
            }



            // Populate the collection with some items.
            //var items = new List<int>();
            //for (int i = 0; i < 1; i++)
            //{
            //    items.Add(i);
            //}

            //collection.ItemsSource = items;
            //collection.ItemsSource = CustomDataObject.GetDataObjects();

            //สำหรับ Slide Bar
            //ObservableCollection<MenuItem> items000 = new ObservableCollection<MenuItem>();
            //items000.Add(new MenuItem()
            //{
            //    IName ="123456",
            //    image = "ms-appx:///Assets//mystery_image.jpg"
            //});
            //items000.Add(new MenuItem()
            //{
            //    IName = "123456",
            //    image = "ms-appx:///Assets//PlaceholderImage.png"
            //});
            //items000.Add(new MenuItem()
            //{
            //    IName = "bbbbb",
            //    image = "ms-appx:///Assets//Square150x150Logo.scale-200.png"
            //});
            //items000.Add(new MenuItem()
            //{
            //    IName = "xxxxxxxxx",
            //    image = "ms-appx:///Assets//Square44x44Logo.targetsize-24_altform-unplated.png"
            //});
            //items000.Add(new MenuItem()
            //{
            //    IName = "ssssssssss",
            //    image = "ms-appx:///Assets//mystery_image.jpg"
            //});
            //items000.Add(new MenuItem()
            //{
            //    IName = "aaaaaaaaaaa",
            //    image = "ms-appx:///Assets//mystery_image.jpg"
            //});

            //Tile1.ItemsSource = items000;
            //Tile1.ItemsSource = items;
            //collection.ItemsSource = items000;
        }

        private async void BackButton_Click(object sender, RoutedEventArgs e)
        {
            ConnectedAnimation animation = ConnectedAnimationService.GetForCurrentView().PrepareToAnimate("backwardsAnimation", destinationElement);

            // Collapse the smoke when the animation completes.
            animation.Completed += Animation_Completed;

            // If the connected item appears outside the viewport, scroll it into view.
            collection.ScrollIntoView(_storedItem, ScrollIntoViewAlignment.Default);
            collection.UpdateLayout();

            // Use the Direct configuration to go back (if the API is available).
            //if (ApiInformation.IsApiContractPresent("Windows.Foundation.UniversalApiContract", 7))
            //{
            //    animation.Configuration = new DirectConnectedAnimationConfiguration();
            //}

            // Play the second connected animation.
            await collection.TryStartConnectedAnimationAsync(animation, _storedItem, "connectedElement");
        }

        private void Animation_Completed(ConnectedAnimation sender, object args)
        {
            SmokeGrid.Visibility = Visibility.Collapsed;
        }

        private void TipsGrid_ItemClick(object sender, ItemClickEventArgs e)
        {
            ConnectedAnimation animation = null;

            // Get the collection item corresponding to the clicked item.
            var container = collection.ContainerFromItem(e.ClickedItem) as GridViewItem;
            if (container != null)
            {
                // Stash the clicked item for use later. We'll need it when we connect back from the detailpage.
                // _storedItem = Convert.ToInt32(container.Content);
                _storedItem = container.Content;

                // Prepare the connected animation.
                // Notice that the stored item is passed in, as well as the name of the connected element.
                // The animation will actually start on the Detailed info page.
                animation = collection.PrepareConnectedAnimation("forwardAnimation", _storedItem, "connectedElement");
            }

            SmokeGrid.Visibility = Visibility.Visible;

            animation.TryStart(destinationElement);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            //Call Data

            //if (e.NavigationMode == NavigationMode.Back)
            //    EntranceAnimation.Edge = EdgeTransitionLocation.Right;

            //ConnectedAnimation animation =
            //  ConnectedAnimationService.GetForCurrentView().GetAnimation("backAnimation");
            //if (animation != null)
            //{
            //    animation.TryStart(SourceImage);
            //}
            base.OnNavigatedTo(e);
            try
            {
                bindData(e);
            }
            catch (Exception)
            {
                On_BackRequested();

            }

          
        }

        // Handles system-level BackRequested events and page-level back button Click events
        private bool On_BackRequested()
        {
            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
                return true;
            }
            return false;
        }


        void bindData(NavigationEventArgs e)
        {

            var api = new ApiData();

            string Data = "";
            if (e.Parameter is string && !string.IsNullOrWhiteSpace((string)e.Parameter))
            {
                Data = e.Parameter.ToString();
            }
            string[] dataValue = Data.Split("|");
            string Topic = dataValue[0];
            string id = dataValue[1];
            //string url = DataConfig.ApiDomain() + "/cockpit/api/collections/get/GameTypeA";
            ////string url = DataConfig.ApiDomain() + "/cockpit/api/collections/get/GameTypeB";
            //string url = DataConfig.ApiDomain() + "/api/collections/get/TopicX1";
            string url = DataConfig.ApiDomain() + "/api/collections/get/" + Topic;

            var result = Task.Run(() => api.GetDataFromServerAsync(url)).Result;
            TopicModel picture = JsonConvert.DeserializeObject<TopicModel>(result);

            foreach (var item in picture.Entries)
            {
                if (item.Id == id)
                {




                    var items = new List<int>();
                    for (int i = 0; i < 1; i++)
                    {
                        items.Add(i);
                    }

                    collection.ItemsSource = items;

                    List<CustomDataObject> objects = new List<CustomDataObject>();

                    objects.Add(new CustomDataObject()
                    {
                        //Title = $"Item {i + 1}",
                        Title = item.TopicHeading,
                        //ImageLocation = $"/Assets/SampleMedia/LandscapeImage{i + 1}.jpg",
                        //Views = rand.Next(100, 999).ToString(),
                        //Likes = rand.Next(10, 99).ToString(),
                        // Description =  item.TopicText, //<< Html
                    });

                    //Head Tile
                    collection.ItemsSource = objects;

                    ///////////////////////////////////////////////////////////////////////////////////////
                    // var baseUri = new Uri("http://test.com");
                    StringHelper st = new StringHelper();
                    var adjustedHtml = st.HtmlAddServer(item.TopicText);
                    HtmlPopupContent.Html = adjustedHtml;
                    //Html Popup
                    //HtmlPopup.ItemsSource = objects;
                    //------------------------------------------------
                    ObservableCollection<MenuItem> items000 = new ObservableCollection<MenuItem>();


                    if ((item.ShowVdo) && (item.TopicVdo != null))
                    {
                        //BorderPic.Visibility = Visibility.Collapsed;
                        //BorderVdo.Visibility = Visibility.Visible;



                        var a = new MenuItem();
                        string vdoPAth = DataConfig.StorageUploadsUrl() + item.TopicVdo.Path;
                        Uri pathUri = new Uri(vdoPAth);
                        a.PathVdo = pathUri;

                        items000.Add(a);

                        mpe.Source = pathUri;
                        mpe.AutoPlay = true;
                        mpe.IsLooping = true;
                        mpe.Play();
                        //a.Video = mpe;

                        mpe.Visibility = Visibility.Visible;
                        Tile1.Visibility = Visibility.Collapsed;

                    }
                    else
                    {
                        foreach (var imageList in item.TopicPhoto)
                        {
                            //BorderPic.Visibility = Visibility.Visible;
                            // BorderVdo.Visibility = Visibility.Collapsed;

                            mpe.Visibility = Visibility.Collapsed;

                            items000.Add(new MenuItem()
                            {
                                IName = imageList.Meta.Title,
                                image = imageList.Path.ToString()
                            });


                        }
                    }

                    Tile1.ItemsSource = items000;

                    //collection.ItemsSource = items000;
                }
            }

        }


        private void Back_Click(object sender, RoutedEventArgs e)
        {
            //this.Frame.Navigate(typeof(Views.TopicHead));
            // this.Frame.Navigate(typeof(Service), null, new EntranceNavigationTransitionInfo());

            if (this.Frame.CanGoBack)
            {
                this.Frame.GoBack();
                //return true;
            }
        }
    }
}
