using KiosBoot.Models;
using ServiceHelpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

using Microsoft.Toolkit.Uwp.UI.Controls;
using Windows.UI.Composition;
using Microsoft.Toolkit.Uwp.UI.Animations;
using KiosBoot.ViewModels;
using Newtonsoft.Json;
using KiosBoot.Helpers.Config;
using KiosBoot.Helpers.Server;
// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace KiosBoot.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Wellcome : Page
    {
        public Wellcome()
        {
            this.InitializeComponent();

            Hello();


    

           


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


        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {





            base.OnNavigatingFrom(e);
        }


        private void Hello()
        {
            if (FaceObjct.MyFace != null)
            {
                //face.FaceAttributes.Gender
                string gender = FaceObjct.MyFace.FaceAttributes.Gender;

                ImageAnalyzer img = this.DataContext as ImageAnalyzer;
                if (img != null)
                {
                    img.UpdateDecodedImageSize(100, 100);
                }

                if (string.Compare(gender, "male", true) == 0)
                {
                    this.SourceImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Animate/male.png"));
                    Greeting.Text = "สวัสดีครับ";
                }
                else if (string.Compare(gender, "female", true) == 0)
                {
                    //this.SourceImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Animate/female.png"));

                    this.SourceImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Animate/main@2x.png"));
                    Greeting.Text = "สวัสดีค่ะ";
                    //
                }

                //if มี Name
                if (FaceObjct.faceIdIdentification != null)
                {
                    string Name = FaceObjct.faceIdIdentification.Person.Name;
                    //NameInformation.Text = Name;


                    var api = new ApiData();


                    string url = DataConfig.ApiDomain() + "/api/collections/get/FaceRecognition";
                    var result = Task.Run(() => api.GetDataFromServerAsync(url)).Result;
                    FaceModel faceSet = JsonConvert.DeserializeObject<FaceModel>(result);


                    foreach (var item in faceSet.Entries)
                    {
                        if (item.Id == Name)
                        {
                            //

                            FaceObjct.faceIdIdentification.Person.Name = item.Name;
                            NameInformation.Text = item.Name;
                            //Play Sounde
                            string urlSound = DataConfig.StorageUploadsUrl()+  item.Sound.Path;
                            SoundManager.PlaySound(urlSound);
                        }
                    }
                 
                }






            }





        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await SourceImage.Fade(value: 0.1f, duration: 40, delay: 5).StartAsync();
            //await Task.Delay(TimeSpan.FromSeconds(3));

            Frame.Navigate(typeof(Menu));
        }
    }
}
