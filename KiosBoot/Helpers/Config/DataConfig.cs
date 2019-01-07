using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiosBoot.Helpers.Config
{
   public static class DataConfig
    {

        static string rootPAth = "cockpit";

        /// <summary>
        ///   return "http://localhost:8080";
        /// </summary>
        /// <returns></returns>
        /// 
        public static string ApiDomain()
        {

            return "http://localhost:8080/"+ rootPAth;
        }
        /// <summary>
        ///     //return "http://localhost:808/cockpit/storage/uploads/";
        /// </summary>
        /// <returns></returns>
        public static string StorageUploadsUrl()
        {
            return ApiDomain() + "/storage/uploads/";
            //return "http://localhost:808/cockpit/storage/uploads/";
        }


        public static string token()
        {
            return "a12c7b4c6d27fd9d204588d1ede416";
        }

       
        public static string ProfileFolder()
        {
            return "ms-appx:///Assets/PictureProfile/";
        }



    }
}
