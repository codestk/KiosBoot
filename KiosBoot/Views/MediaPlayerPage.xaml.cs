using KiosBoot.Helpers.Config;
using KiosBoot.Helpers.ConvertModel;
using KiosBoot.Helpers.Server;
using MarqueeText;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.System.Display;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace KiosBoot.Views
{
    public sealed partial class MediaPlayerPage : Page, INotifyPropertyChanged
    {
        // TODO WTS: Set your default video and image URIs
        // For more on the MediaPlayer and adjusting controls and behavior see https://docs.microsoft.com/en-us/windows/uwp/controls-and-patterns/media-playback
        //private const string DefaultSource = "https://sec.ch9.ms/ch9/db15/43c9fbed-535e-4013-8a4a-a74cc00adb15/C9L12WinTemplateStudio_high.mp4";

        // The poster image is displayed until the video is started
        //private const string DefaultPoster = "https://sec.ch9.ms/ch9/db15/43c9fbed-535e-4013-8a4a-a74cc00adb15/C9L12WinTemplateStudio_960.jpg";

        // The DisplayRequest is used to stop the screen dimming while watching for extended periods
        private DisplayRequest _displayRequest = new DisplayRequest();

        private bool _isRequestActive = false;

        private DisplayModel MediaDisplay;

        public MediaPlayerPage()
        {
            InitializeComponent();

            //ApplicationView view = ApplicationView.GetForCurrentView();
            //if (!view.IsFullScreenMode)
            //{
            //    view.TryEnterFullScreenMode();
            //}
            //mpe.Source = MediaSource.CreateFromUri(new Uri(DefaultSource));
            LoadDataFromServer();
            //LoadEmbeddedAppFile();


            if (MediaDisplay.Total > 0)
            {
                //Set 1st Vdo
                SetFirstVdeo();

                mpe.MediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
            }


            ApplicationView view = ApplicationView.GetForCurrentView();
            if (!view.IsFullScreenMode)
            {
                view.TryEnterFullScreenMode();
            }
        }

        private void SetFirstVdeo()
        {
            string vdoPAth="";
            if (MediaDisplay.Entries[0].Vdo!=null)
              vdoPAth = DataConfig.StorageUploadsUrl() + MediaDisplay.Entries[0].Vdo.Path;

            //string PhotoPAth = DataConfig.StorageUploadsUrl() + MediaDisplay.Entries[0].Photo.Path;

            string Logo="";
            if (MediaDisplay.Entries[0].Logo!=null)
              Logo = DataConfig.StorageUploadsUrl() + MediaDisplay.Entries[0].Logo.Path;



    
             string text = MediaDisplay.Entries[0].Text;

            string Topic = "";
            if (MediaDisplay.Entries[0].Topic!=null)
              Topic = DataConfig.StorageUploadsUrl() + MediaDisplay.Entries[0].Topic;



             

            ObservableCollection<MenuItem> items000 = new ObservableCollection<MenuItem>();
            if (MediaDisplay.Entries[0].Photo.Length > 0)
            {
                foreach (var imageList in MediaDisplay.Entries[0].Photo)
                {
                    items000.Add(new MenuItem()
                    {
                        IName = imageList.Meta.Title,
                        image = imageList.Path.ToString()
                    });
                }

                Tile1.ItemsSource = items000;

            }


            if (Logo != "")
            {
                Uri pathLogo = new Uri(Logo);
                DestinationImage.Source = new BitmapImage(pathLogo);
            }
            //Uri pathPhoto = new Uri(PhotoPAth);
            //PhotoImage.Source = new BitmapImage(pathPhoto);

            mpe.PosterSource = new BitmapImage(new Uri("ms-appx:///Assets/icon/loading.gif"));
            Uri pathUri = new Uri(vdoPAth);
            // Uri pathUri = new Uri("ms-appx:///Assets/Mp4/demo.mp4");
            //mediaPlayer.Source = MediaSource.CreateFromUri(pathUri);
            mpe.Source = MediaSource.CreateFromUri(pathUri);
            mpe.MediaPlayer.Play();

            CurrentSet++;

            //textSlide.Text = text;
            //textSlide.KeyTipHorizontalOffset = -150.00;
            //textSlide.StartBringIntoView();

            //< !--< local:MarqueeTextControl x:Name = "textSlide"  Background = "Green"   Foreground = "White" AnimationDuration = "0:2:9" IsTicker = "True"   FontSize = "60" Height = "110" >

            //                      < local:MarqueeTextControl.Text >

            //                    </ local:MarqueeTextControl.Text >

            //                    </ local:MarqueeTextControl >
            //AnimationDuration="0:2:8" FontSize="50"   Height="100" IsTicker="True"   Padding="20,10,20,0"
            //MarqueeTextControl converter = new MarqueeTextControl();
            //converter.Text= text;
            //converter.FontSize = 50;
            //converter.IsTicker = true;
            //converter.Padding = new Thickness(20, 10, 20, 0);
            //converter.Height = 100;
            // textSlide.Text = text;

            maqeru.Children.Add(GenMaqueer(text));
         
        }
          MarqueeTextControl GenMaqueer(string text)
        {
            MarqueeTextControl converter = new MarqueeTextControl();
            converter.Text = text;
            converter.FontSize = 50;
            converter.IsTicker = true;
            converter.Padding = new Thickness(20, 10, 20, 0);
            converter.Height = 100;
             
            converter.Foreground = new SolidColorBrush(Colors.White);
            converter.AnimationDuration = new TimeSpan(0, 2, 8);
            return converter;
        }

 
        private void setCorrentRender(int rowSet, MediaPlayer _MediaPlayer)
        {
            //string vdoPAth = DataConfig.StorageUploadsUrl() + MediaDisplay.Entries[rowSet].Vdo.Path;
            //string PhotoPAth = DataConfig.StorageUploadsUrl() + MediaDisplay.Entries[rowSet].Photo.Path;


            //string Logo = DataConfig.StorageUploadsUrl() + MediaDisplay.Entries[rowSet].Logo.Path;

            //string text = MediaDisplay.Entries[rowSet].Text;

            //string Topic = MediaDisplay.Entries[rowSet].Topic;

            //DateTimeOffset Date = MediaDisplay.Entries[rowSet].Date;
            string vdoPAth = "";
            if (MediaDisplay.Entries[rowSet].Vdo != null)
                vdoPAth = DataConfig.StorageUploadsUrl() + MediaDisplay.Entries[rowSet].Vdo.Path;

            //string PhotoPAth = DataConfig.StorageUploadsUrl() + MediaDisplay.Entries[0].Photo.Path;

            string Logo = "";
            if (MediaDisplay.Entries[rowSet].Logo != null)
                Logo = DataConfig.StorageUploadsUrl() + MediaDisplay.Entries[rowSet].Logo.Path;




            string text = MediaDisplay.Entries[rowSet].Text;

            string Topic = "";
            if (MediaDisplay.Entries[rowSet].Topic != null)
                Topic = DataConfig.StorageUploadsUrl() + MediaDisplay.Entries[rowSet].Topic;






            ObservableCollection<MenuItem> items000 = new ObservableCollection<MenuItem>();

            foreach (var imageList in MediaDisplay.Entries[rowSet].Photo)
            {
                items000.Add(new MenuItem()
                {
                    IName = imageList.Meta.Title,
                    image = imageList.Path.ToString()
                });
            }


            
            //Reder แต่ละ item
            try
            {
                Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
() =>
{
    // Your UI update code goes here!

    // textSlide.StartBringIntoView();

   

    Uri pathLogo = new Uri(Logo);
    DestinationImage.Source = new BitmapImage(pathLogo);

    //Uri pathPhoto = new Uri(PhotoPAth);
    // PhotoImage.Source = new BitmapImage(pathPhoto);

    //Set Vdo
    //present loading
    //mpe.PosterSource = new BitmapImage(new Uri("ms-appx:///Assets/Animate/light.png"));
    Uri pathUri = new Uri(vdoPAth);
    // Uri pathUri = new Uri("ms-appx:///Assets/Mp4/demo.mp4");
    //mediaPlayer.Source = MediaSource.CreateFromUri(pathUri);
    _MediaPlayer.Source = MediaSource.CreateFromUri(pathUri);
    _MediaPlayer.Play();
    //Set Logo ======================================================



   

    Tile1.ItemsSource = items000;


   // MarqueeTextControl converter = new MarqueeTextControl();
    //converter.Text = text;
    // textSlide.Text = text;
    maqeru.Children.Clear();
    maqeru.Children.Add(GenMaqueer(text));

    // var a = new BringIntoViewOptions();
    //  a.AnimationDesired = true;
    //textSlide.Text = text;
    // textSlide.StartBringIntoView(a);

}
);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            CurrentSet++;
            if (CurrentSet == MaxSet)
            {
                CurrentSet = 0;
            }
        }

        private void MediaPlayer_MediaEnded(MediaPlayer sender, object args)
        {
            setCorrentRender(CurrentSet, sender);
        }

        private int CurrentSet = 0;
        private int MaxSet = 0;

        public void LoadDataFromServer()
        {
            ApiData api = new ApiData();

            //string url = DataConfig.ApiDomain() + "/cockpit/api/collections/get/GameTypeA";
            ////string url = DataConfig.ApiDomain() + "/cockpit/api/collections/get/GameTypeB";
            string url = DataConfig.ApiDomain() + "/api/collections/get/DISPLAY";

            var result = Task.Run(() => api.GetDataFromServerAsync(url)).Result;

            MediaDisplay = JsonConvert.DeserializeObject<DisplayModel>(result);

            DateTime dt = System.DateTime.Today;

           
            int a = 0;
            foreach (var item in MediaDisplay.Entries)
            {
                if ((item.DateFrom <= dt) && (item.DateTo >= dt))
                {
                    a = a + 1;
                }
        
                
            }

            DisplayModel dtnew = new DisplayModel();

            dtnew.Entries = new Entry[a];
            int i = 0;
            foreach (var item in MediaDisplay.Entries)
            {
                if ((item.DateFrom <= dt) && (item.DateTo >= dt))
                {
                    dtnew.Entries[i] = item;
                    i = i + 1;
                }


            }

        

            //MediaDisplay.Entries = dtnew.Entries.OrderBy(x => x.DateTo).ToArray();

            MediaDisplay.Entries = dtnew.Entries;






            MaxSet = (int)dtnew.Entries.Length;
        }

        #region SlideShow

        //ScrollBar
        private DispatcherTimer timer;

        //private void scrollViewer_Loaded(object sender, RoutedEventArgs e)
        //{
        //    timer = new DispatcherTimer();

        //    timer.Tick += (ss, ee) =>
        //    {
        //        if (timer.Interval.Ticks == 300)
        //        {
        //            //each time set the offset to scrollviewer.HorizontalOffset + 5
        //            scrollviewer.ScrollToHorizontalOffset(scrollviewer.HorizontalOffset + 2);
        //            //if the scrollviewer scrolls to the end, scroll it back to the start.
        //            if (scrollviewer.HorizontalOffset == (scrollviewer.ScrollableWidth))
        //            {
        //                scrollviewer.ScrollToHorizontalOffset(0);
        //            }
        //        }
        //    };
        //    timer.Interval = new TimeSpan(300);
        //    timer.Start();
        //}

        //private void scrollviewer_Unloaded(object sender, RoutedEventArgs e)
        //{
        //    timer.Stop();
        //}

        #endregion SlideShow

        private void LoadEmbeddedAppFile()
        {
            try
            {
                //present loading
                mpe.PosterSource = new BitmapImage(new Uri("ms-appx:///Assets/Animate/light.png"));
                Uri pathUri = new Uri("ms-appx:///Assets/Mp4/demo.mp4");
                //mediaPlayer.Source = MediaSource.CreateFromUri(pathUri);
                mpe.Source = MediaSource.CreateFromUri(pathUri);
            }
            catch (Exception ex)
            {
                if (ex is FormatException)
                {
                    // handle exception.
                    // For example: Log error or notify user problem with file
                }
            }
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            mpe.MediaPlayer.Pause();
            mpe.MediaPlayer.PlaybackSession.PlaybackStateChanged -= PlaybackSession_PlaybackStateChanged;
        }

        #region Effecft

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            if (e.NavigationMode == NavigationMode.Back)
            {
                ConnectedAnimationService.GetForCurrentView()
                    .PrepareToAnimate("backAnimation", DestinationImage);

                // Use the recommended configuration for back animation.
                //animation.Configuration = new DirectConnectedAnimationConfiguration();
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            //if (e.NavigationMode == NavigationMode.Back)
            //    EntranceAnimation.Edge = EdgeTransitionLocation.Top;

            ConnectedAnimation animation =
                ConnectedAnimationService.GetForCurrentView().GetAnimation("forwardAnimation");
            if (animation != null)
            {
                animation.TryStart(DestinationImage);
            }

            base.OnNavigatedTo(e);
            mpe.MediaPlayer.PlaybackSession.PlaybackStateChanged += PlaybackSession_PlaybackStateChanged;
        }

        #endregion Effecft

        private async void PlaybackSession_PlaybackStateChanged(MediaPlaybackSession sender, object args)
        {
            if (sender is MediaPlaybackSession playbackSession && playbackSession.NaturalVideoHeight != 0)
            {
                if (playbackSession.PlaybackState == MediaPlaybackState.Playing)
                {
                    if (!_isRequestActive)
                    {
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            _displayRequest.RequestActive();
                            _isRequestActive = true;
                        });
                    }
                }
                else
                {
                    if (_isRequestActive)
                    {
                        await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                        {
                            _displayRequest.RequestRelease();
                            _isRequestActive = false;
                        });
                    }
                }
            }
        }

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

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            // Frame.GoBack();
            this.Frame.Navigate(typeof(Menu), null, new DrillInNavigationTransitionInfo());
        }
    }
}
