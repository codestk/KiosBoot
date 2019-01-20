//
// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license.
//
// Microsoft Cognitive Services: http://www.microsoft.com/cognitive
//
// Microsoft Cognitive Services Github:
// https://github.com/Microsoft/Cognitive
//
// Copyright (c) Microsoft Corporation
// All rights reserved.
//
// MIT License:
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED ""AS IS"", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

using KiosBoot.Controls;
using KiosBoot.Models;
using Microsoft.ProjectOxford.Face.Contract;
using ServiceHelpers;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;


 
using System.Linq;
using KiosBoot.Helpers.Profile;
using KiosBoot.Helpers.Server;
using KiosBoot.ViewModels;
using KiosBoot.Helpers.Config;
using KiosBoot.Views.Face;



// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace KiosBoot.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    [KioskExperience(Title = "Automatic Photo Capture", ImagePath = "ms-appx:/Assets/camera.png", ExperienceType = ExperienceType.Kiosk)]
    public sealed partial class AutomaticPhotoCapturePage : Page
    {
        public AutomaticPhotoCapturePage()
        {
            this.InitializeComponent();
            Window.Current.CoreWindow.CharacterReceived += CoreWindow_CharacterReceived;

            this.cameraControl.EnableAutoCaptureMode = true;
            this.cameraControl.FilterOutSmallFaces = true;
            this.cameraControl.AutoCaptureStateChanged += CameraControl_AutoCaptureStateChanged;
            this.cameraControl.CameraAspectRatioChanged += CameraControl_CameraAspectRatioChanged;

            Window.Current.Activated += CurrentWindowActivationStateChanged;
            FaceObjct.Clear();



            App.Current.idleTimer.Start();
            App.Current.IsIdleChanged += onIsIdleChanged;

            //ScrollGame.AddScrollInfo(555, "00444");
        }




        private void CoreWindow_CharacterReceived(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.CharacterReceivedEventArgs args)
        {

            if (args.KeyCode == 27) //esc
            {
                this.Frame.Navigate(typeof(SettingsPage), null, new EntranceNavigationTransitionInfo());
            }

            if (args.KeyCode == 9) //tab
            {
                // your code here fore Escape key
                this.Frame.Navigate(typeof(ManageFace), null, new EntranceNavigationTransitionInfo());
            }



            if (args.KeyCode == 113) //q
            {
                // your code here fore Escape key
                ppup.Height = Window.Current.Bounds.Height;
                ppup.IsOpen = true;
            }

         
           
        }




        private void CameraControl_CameraAspectRatioChanged(object sender, EventArgs e)
        {
            this.UpdateCameraHostSize();
        }

        private async void CurrentWindowActivationStateChanged(object sender, Windows.UI.Core.WindowActivatedEventArgs e)
        {
            if ((e.WindowActivationState == Windows.UI.Core.CoreWindowActivationState.CodeActivated ||
                e.WindowActivationState == Windows.UI.Core.CoreWindowActivationState.PointerActivated) &&
                this.cameraControl.CameraStreamState == Windows.Media.Devices.CameraStreamState.Shutdown)
            {
                // When our Window loses focus due to user interaction Windows shuts it down, so we
                // detect here when the window regains focus and trigger a restart of the camera.
                await this.cameraControl.StartStreamAsync();
            }
        }

        private async void CameraControl_AutoCaptureStateChanged(object sender, AutoCaptureState e)
        {


            App.Current.idleTimer.Stop();
            App.Current.idleTimer.Start();
            App.Current.isIdle = false;
            switch (e)
            {




                case AutoCaptureState.WaitingForFaces:
                    this.cameraGuideBallon.Opacity = 1;
                    this.cameraGuideText.Text = "ก้าวไปข้างหน้ากล้องเพื่อเริ่มต้น!";
                    this.cameraGuideHost.Opacity = 1;
                    break;

                case AutoCaptureState.WaitingForStillFaces:
                    this.cameraGuideText.Text = "กรุณา ยืนอยู่กับที่...";
                    break;

                case AutoCaptureState.ShowingCountdownForCapture:

                

                    this.cameraGuideText.Text = "";
                    this.cameraGuideBallon.Opacity = 0;

                    this.cameraGuideCountdownHost.Opacity = 1;
                    this.countDownTextBlock.Text = "3";
                    await Task.Delay(750);
                    this.countDownTextBlock.Text = "2";
                    await Task.Delay(750);
                    this.countDownTextBlock.Text = "1";
                    await Task.Delay(750);
                    this.cameraGuideCountdownHost.Opacity = 0;

                    this.ProcessCameraCapture(await this.cameraControl.TakeAutoCapturePhoto());

                    break;

                case AutoCaptureState.ShowingCapturedPhoto:
                    this.cameraGuideHost.Opacity = 0;
                    break;

                default:
                    break;
            }
        }


        private async Task<ImageAnalyzer> GetPrimaryFaceFromCameraCaptureAsync(ImageAnalyzer img)
        {
            if (img == null)
            {
                return null;
            }

            await img.DetectFacesAsync();

            if (img.DetectedFaces == null || !img.DetectedFaces.Any())
            {
                return null;
            }

            // Crop the primary face and return it as the result
            FaceRectangle rect = img.DetectedFaces.First().FaceRectangle;
            double heightScaleFactor = 1.6;
            double widthScaleFactor = 1.6;
            FaceRectangle biggerRectangle = new FaceRectangle
            {
                Height = Math.Min((int)(rect.Height * heightScaleFactor), img.DecodedImageHeight),
                Width = Math.Min((int)(rect.Width * widthScaleFactor), img.DecodedImageWidth)
            };
            biggerRectangle.Left = Math.Max(0, rect.Left - (int)(rect.Width * ((widthScaleFactor - 1) / 2)));
            biggerRectangle.Top = Math.Max(0, rect.Top - (int)(rect.Height * ((heightScaleFactor - 1) / 1.4)));


 

           StorageFolder appInstalledFolder = Windows.ApplicationModel.Package.Current.InstalledLocation;
            //StorageFolder assetsFolder = await appInstalledFolder.GetFolderAsync("Temp");
            //StorageFolder Temp = await Task.Run(() => appInstalledFolder.GetFolderAsync("Temp")).Result;

             StorageFolder Temp = await Task.Run(() => appInstalledFolder.GetFolderAsync("Assets")).Result;

            StorageFolder PictureProfile = await Temp.GetFolderAsync("PictureProfile");


            string nameImage = DateTime.Now.ToString("yyyyMMddHHmmss") + ".jpg";
            StorageFile tempFile = await PictureProfile.CreateFileAsync(nameImage, CreationCollisionOption.GenerateUniqueName);





            await Util.CropBitmapAsync(img.GetImageStreamCallback, biggerRectangle, tempFile);

          

            string imagePath = tempFile.Path;
            Uri uri = new Uri(imagePath, UriKind.RelativeOrAbsolute);
            ImageSource imgSource = new BitmapImage(uri);
            captureImage.Source = imgSource;

            //Save image for Game
            UserProfile.PictureProfile = imgSource;
            UserProfile.PicturePath = DataConfig.ProfileFolder() + tempFile.Name;
            //Assets\PictureProfile tempFile.hName


            return new ImageAnalyzer(tempFile.OpenStreamForReadAsync, tempFile.Path);
        }



        private void ProcessCameraCapture(ImageAnalyzer e)
        {


            App.Current.idleTimer.Stop();
            App.Current.idleTimer.Start();
            App.Current.isIdle = false;

            this.photoCaptureBalloonHost.Opacity = 0;
            if (e == null)
            {
                this.cameraControl.RestartAutoCaptureCycle();
                return;
            }

            this.imageFromCameraWithFaces.DataContext = e;

            e.FaceRecognitionCompleted += async (s, args) =>
            {

                if (e.DetectedFaces.Count() == 0)
                    return;

                this.photoCaptureBalloonHost.Opacity = 0;

             
                
                    await GetPrimaryFaceFromCameraCaptureAsync(e);
                    if (FaceObjct.faceIdIdentification != null)
                    {
                        UserProfile.Name = FaceObjct.faceIdIdentification.Person.Name;
                    }
                    await Task.Delay(1000);

                    Frame.Navigate(typeof(Wellcome), null, new SuppressNavigationTransitionInfo());
                    return;
             
 

            };

            e.FaceRecognitionUnCompleted += async (s, args) =>
            {
                if (e.DetectedFaces.Count() > 0)
                    return;

                this.photoCaptureBalloonHost.Opacity = 1;

                int photoDisplayDuration = 10;
                double decrementPerSecond = 100.0 / photoDisplayDuration;
                for (double i = 100; i >= 0; i -= decrementPerSecond)
                {
                    this.resultDisplayTimerUI.Value = i;
                  //  await Task.Delay(1000);
                }

                this.photoCaptureBalloonHost.Opacity = 0;
                this.imageFromCameraWithFaces.DataContext = null;

                this.cameraControl.RestartAutoCaptureCycle();
            };


        




        }

        private void onIsIdleChanged(object sender, EventArgs e)
        {
            App.Current.idleTimer.Stop();
            System.Diagnostics.Debug.WriteLine($"IsIdle: {App.Current.IsIdle}");
            Frame.Navigate(typeof(ScreenServer), null, new SuppressNavigationTransitionInfo());

        }

        


        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
         
            FaceObjct.Clear();

            EnterKioskMode();

            if (string.IsNullOrEmpty(SettingsHelper.Instance.FaceApiKey))
            {
                await new MessageDialog("Missing Face API Key. Please enter a key in the Settings page.", "Missing Face API Key").ShowAsync();
            }
            else
            {
                await this.cameraControl.StartStreamAsync();
            }


            //App.Current.IsIdleChanged += onIsIdleChanged;


            base.OnNavigatedTo(e);
        }

        private void EnterKioskMode()
        {
            ApplicationView view = ApplicationView.GetForCurrentView();
            if (!view.IsFullScreenMode)
            {
                view.TryEnterFullScreenMode();
            }
        }

        protected override async void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {

            App.Current.IsIdleChanged -= onIsIdleChanged;

            Window.Current.Activated -= CurrentWindowActivationStateChanged;
            this.cameraControl.AutoCaptureStateChanged -= CameraControl_AutoCaptureStateChanged;
            this.cameraControl.CameraAspectRatioChanged -= CameraControl_CameraAspectRatioChanged;

            await this.cameraControl.StopStreamAsync();
            

            base.OnNavigatingFrom(e);
        }

        private void OnPageSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.UpdateCameraHostSize();
        }

        private void UpdateCameraHostSize()
        {
            this.cameraHostGrid.Width = this.cameraHostGrid.ActualHeight * (this.cameraControl.CameraAspectRatio != 0 ? this.cameraControl.CameraAspectRatio : 1.777777777777);
        }
    }
}
