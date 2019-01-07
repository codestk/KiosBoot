using KiosBoot.Helpers.Config;
using KiosBoot.Helpers.ConvertModel;
using KiosBoot.Helpers.Server;
using KiosBoot.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiosBoot.ViewModels
{
    public class ScrollGame: ObservableObject
    {
        static ObservableCollection<ScrollInfo> sc = new ObservableCollection<ScrollInfo>();

        //Image = "ms-appx:///Assets/black_background.jpg" 
        public static void AddScrollInfo(int Scroll,String imageUrl)
        {
            //string url = DataConfig.ApiDomain() + "/cockpit/api/collections/save/ScrollList?token=a12c7b4c6d27fd9d204588d1ede416";
            //http://localhost:8080/cockpit/SaveApi/CollectionScroll.html?PicturePath=66&Scroll=99

            //string url = DataConfig.ApiDomain() + "/cockpit/api/collections/save/ScrollList?token=account-e19e9ca6f60d33e270f5bdfc460480";


            //string url = DataConfig.ApiDomain() + "/api/collections/save/ScrollList?token="+DataConfig.token();
            string url = DataConfig.ApiDomain() + "/api/collections/save/ScrollList";

            ApiData api = new ApiData();

            string jsonString = "{\"data\":{\"PicturePath\":\""+imageUrl+"\",\"Scroll\":\"" + Scroll + "\"}}";
            var result = Task.Run(() => api.RequestDataFromServerAsync(url, jsonString)).Result;
           
        }


    
        public static IOrderedEnumerable<ScrollInfo> GetScrollInfo()
        {
            sc.Clear();

           string  url = DataConfig.ApiDomain() + "/api/collections/get/ScrollList";
        
            ApiData api = new ApiData();
            //var result = Task.Run(() => api.GetDataFromServerAsync(url)).Result;
            //var result = api.GetDataFromServerAsync(url);

            var result = Task.Run(() => api.GetDataFromServerAsync(url)).Result;
            ScrollInfoModel ScrollInfoSSS = JsonConvert.DeserializeObject<ScrollInfoModel>(result);

            
            foreach (var item in ScrollInfoSSS.Entries)
            {


                int a = Convert.ToInt32(item.Scroll);
                //string imageUrl = DataConfig.StorageUploadsUrl() + i.Image.Path;
                sc.Add(new ScrollInfo { Scroll = a, Name = "Hello", Image = item.PicturePath });
              
            }

            //sc.Add(new ScrollInfo { Scroll = 1, Name = "A", Image = "ms-appx:///Assets/black_background.jpg" });
            //sc.Add(new ScrollInfo { Scroll = 2, Name = "A", Image = "ms-appx:///Assets/black_background.jpg" });
            //sc.Add(new ScrollInfo { Scroll = 3, Name = "A", Image = "ms-appx:///Assets/black_background.jpg" });
            //sc.Add(new ScrollInfo { Scroll = 4, Name = "A", Image = "ms-appx:///Assets/black_background.jpg" });
            var ScrollInfoReorder = sc.OrderByDescending(a => a.Scroll);

            int i = 1;
            foreach (var item in ScrollInfoReorder)
            {
                item.ranking = i;

                i++;
            }
            return ScrollInfoReorder;
        }


       
    }
}


