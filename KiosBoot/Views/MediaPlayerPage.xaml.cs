using KiosBoot.Helpers.Config;
using KiosBoot.Helpers.ConvertModel;
using KiosBoot.Helpers.Server;
using Newtonsoft.Json;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.System.Display;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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

            //Set 1st Vdo
            SetFirstVdeo();

            mpe.MediaPlayer.MediaEnded += MediaPlayer_MediaEnded;
        }

        private void SetFirstVdeo()
        {
            string vdoPAth = DataConfig.StorageUploadsUrl() + MediaDisplay.Entries[0].Vdo.Path;

            string PhotoPAth = DataConfig.StorageUploadsUrl() + MediaDisplay.Entries[0].Photo.Path;

            string Logo = DataConfig.StorageUploadsUrl() + MediaDisplay.Entries[0].Logo.Path;

            string text = DataConfig.StorageUploadsUrl() + MediaDisplay.Entries[0].Text;

            string Topic = DataConfig.StorageUploadsUrl() + MediaDisplay.Entries[0].Topic;

            DateTimeOffset Date = MediaDisplay.Entries[0].Date;

            mpe.PosterSource = new BitmapImage(new Uri("ms-appx:///Assets/Animate/light.png"));
            Uri pathUri = new Uri(vdoPAth);
            // Uri pathUri = new Uri("ms-appx:///Assets/Mp4/demo.mp4");
            //mediaPlayer.Source = MediaSource.CreateFromUri(pathUri);
            mpe.Source = MediaSource.CreateFromUri(pathUri);
            mpe.MediaPlayer.Play();

            CurrentSet++;
        }

        private void setCorrentRender(int rowSet, MediaPlayer _MediaPlayer)
        {
            string vdoPAth = DataConfig.StorageUploadsUrl() + MediaDisplay.Entries[rowSet].Vdo.Path;

            string PhotoPAth = DataConfig.StorageUploadsUrl() + MediaDisplay.Entries[rowSet].Photo.Path;

            string Logo = DataConfig.StorageUploadsUrl() + MediaDisplay.Entries[rowSet].Logo.Path;

            string text = DataConfig.StorageUploadsUrl() + MediaDisplay.Entries[rowSet].Text;

            string Topic = DataConfig.StorageUploadsUrl() + MediaDisplay.Entries[rowSet].Topic;

            DateTimeOffset Date = MediaDisplay.Entries[rowSet].Date;

            //Reder แต่ละ item
            try
            {
              
                textSlide.Text = text;


                Uri pathLogo = new Uri(Logo);
                DestinationImage.Source = new BitmapImage(pathLogo);


                Uri pathPhoto = new Uri(PhotoPAth);
                PhotoImage.Source = new BitmapImage(pathPhoto);

                //Set Vdo
                //present loading
                //mpe.PosterSource = new BitmapImage(new Uri("ms-appx:///Assets/Animate/light.png"));
                Uri pathUri = new Uri(vdoPAth);
                // Uri pathUri = new Uri("ms-appx:///Assets/Mp4/demo.mp4");
                //mediaPlayer.Source = MediaSource.CreateFromUri(pathUri);
                _MediaPlayer.Source = MediaSource.CreateFromUri(pathUri);
                _MediaPlayer.Play();
                //Set Logo ======================================================
            }
            catch (Exception ex)
            {
                if (ex is FormatException)
                {
                    // handle exception.
                    // For example: Log error or notify user problem with file
                }
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

            MaxSet = (int)MediaDisplay.Total;
        }

        #region SlideShow

        //ScrollBar
        private DispatcherTimer timer;

        private void scrollViewer_Loaded(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();

            timer.Tick += (ss, ee) =>
            {
                if (timer.Interval.Ticks == 300)
                {
                    //each time set the offset to scrollviewer.HorizontalOffset + 5
                    scrollviewer.ScrollToHorizontalOffset(scrollviewer.HorizontalOffset + 2);
                    //if the scrollviewer scrolls to the end, scroll it back to the start.
                    if (scrollviewer.HorizontalOffset == (scrollviewer.ScrollableWidth))
                    {
                        scrollviewer.ScrollToHorizontalOffset(0);
                    }
                }
            };
            timer.Interval = new TimeSpan(300);
            timer.Start();
        }

        private void scrollviewer_Unloaded(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

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
        }
    }
}
