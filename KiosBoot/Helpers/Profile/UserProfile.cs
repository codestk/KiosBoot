using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media;

namespace KiosBoot.Helpers.Profile
{
    public static class UserProfile
    {

        /// <summary>
        ///   ImageSource imgSource = new BitmapImage(uri);
        //    captureImage.Source = imgSource;
       /// </summary>
        public static ImageSource PictureProfile { get; set; }
        public static string PicturePath { get; set; }


        public static string Name { get; set; }

        public static int Scroll { get; set; }


        public static void ClearPRofile()
        {
            PictureProfile = null;

        }

    }
}
