using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using KiosBoot.Helpers.Faces;
using KiosBoot.Services;
using KiosBoot.Views;
using ServiceHelpers;
using Windows.ApplicationModel.Activation;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Notifications;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;

namespace KiosBoot
{
    public sealed partial class App : Application
    {
        #region IDel Tiem

        public static new App Current => (App)Application.Current;

        public event EventHandler IsIdleChanged;

        private DispatcherTimer idleTimer;

        private bool isIdle;
        public bool IsIdle
        {
            get
            {
                return isIdle;
            }

            private set
            {
                if (isIdle != value)
                {
                    isIdle = value;
                    IsIdleChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        private void onIdleTimerTick(object sender, object e)
        {
          
            idleTimer.Stop();
            IsIdle = true;
        }

        private void onCoreWindowPointerMoved(CoreWindow sender, PointerEventArgs args)
        {
            idleTimer.Stop();
            idleTimer.Start();
            IsIdle = false;
        }

        #endregion



        private Lazy<ActivationService> _activationService;

        private ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        public App()
        {
            InitializeComponent();

 

            // Deferred execution until used. Check https://msdn.microsoft.com/library/dd642331(v=vs.110).aspx for further info on Lazy<T> class.
            _activationService = new Lazy<ActivationService>(CreateActivationService);

            //FaceTain ft = new FaceTain();
            //Task.Run(() => ft.LoadPersonGroupsFromService());
            //ft.AddPerson("Momojojoj000");

            //CreateProfile


           

        }

 



        private void EnterKioskMode()
        {
            ApplicationView view = ApplicationView.GetForCurrentView();
            if (!view.IsFullScreenMode)
            {
                view.TryEnterFullScreenMode();
            }
        }


        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {

            idleTimer = new DispatcherTimer();
            idleTimer.Interval = TimeSpan.FromSeconds(10);  // 10s idle delay
            idleTimer.Tick += onIdleTimerTick;
            Window.Current.CoreWindow.PointerMoved += onCoreWindowPointerMoved;







            if (!args.PrelaunchActivated)
            {

                SettingsHelper.Instance.SettingsChanged += (target, argsA) =>
                {
                    FaceServiceHelper.ApiKey = SettingsHelper.Instance.FaceApiKey;
                    FaceServiceHelper.ApiKeyRegion = SettingsHelper.Instance.FaceApiKeyRegion;
                    VisionServiceHelper.ApiKey = SettingsHelper.Instance.VisionApiKey;
                    VisionServiceHelper.ApiKeyRegion = SettingsHelper.Instance.VisionApiKeyRegion;
                    BingSearchHelper.SearchApiKey = SettingsHelper.Instance.BingSearchApiKey;
                    BingSearchHelper.AutoSuggestionApiKey = SettingsHelper.Instance.BingAutoSuggestionApiKey;
                    TextAnalyticsHelper.ApiKey = SettingsHelper.Instance.TextAnalyticsKey;
                    TextAnalyticsHelper.ApiKeyRegion = SettingsHelper.Instance.TextAnalyticsApiKeyRegion;
                    TextAnalyticsHelper.ApiKey = SettingsHelper.Instance.TextAnalyticsKey;
                    ImageAnalyzer.PeopleGroupsUserDataFilter = SettingsHelper.Instance.WorkspaceKey;
                    FaceListManager.FaceListsUserDataFilter = SettingsHelper.Instance.WorkspaceKey;
                    CoreUtil.MinDetectableFaceCoveragePercentage = SettingsHelper.Instance.MinDetectableFaceCoveragePercentage;
                };

                // callbacks for core library
                FaceServiceHelper.Throttled = () => ShowThrottlingToast("Face");
                VisionServiceHelper.Throttled = () => ShowThrottlingToast("Vision");
                ErrorTrackingHelper.TrackException = (ex, msg) => LogException(ex, msg);
                ErrorTrackingHelper.GenericApiCallExceptionHandler = Util.GenericApiCallExceptionHandler;

                SettingsHelper.Instance.Initialize();




                EnterKioskMode();
                await ActivationService.ActivateAsync(args);
            }

        }

        private static void LogException(Exception ex, string message)
        {
            Debug.WriteLine("Error detected! Exception: \"{0}\", More info: \"{1}\".", ex.Message, message);
        }



        private static void ShowThrottlingToast(string api)
        {
            ToastTemplateType toastTemplate = ToastTemplateType.ToastText02;
            Windows.Data.Xml.Dom.XmlDocument toastXml = ToastNotificationManager.GetTemplateContent(toastTemplate);
            Windows.Data.Xml.Dom.XmlNodeList toastTextElements = toastXml.GetElementsByTagName("text");
            toastTextElements[0].AppendChild(toastXml.CreateTextNode("Intelligent Kiosk"));
            toastTextElements[1].AppendChild(toastXml.CreateTextNode("The " + api + " API is throttling your requests. Consider upgrading to a Premium Key."));

            ToastNotification toast = new ToastNotification(toastXml);
            ToastNotificationManager.CreateToastNotifier().Show(toast);
        }





        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        private ActivationService CreateActivationService()
        {



            //return new ActivationService(this, typeof(Views.RecognitionPage));

            //return new ActivationService(this, typeof(Views.SettingsPage));
            //return new ActivationService(this, typeof(Views.FaceIdentificationSetup));

            //return new ActivationService(this, typeof(Views.Game.MainWindow));

            //return new ActivationService(this, typeof(Views.Game.gameLv10));

            //return new ActivationService(this, typeof(Views.Game.gameLv10));

            // return new ActivationService(this, typeof(Views.MediaPlayerPage));


            return new ActivationService(this, typeof(Views.Game.gameLv10));
           // return new ActivationService(this, typeof(Views.FaceIdentificationSetup));

        }



 




    }
}
