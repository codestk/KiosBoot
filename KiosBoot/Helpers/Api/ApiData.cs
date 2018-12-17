using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace KiosBoot.Helpers.Server
{
   public  class ApiData
    {

        public async Task<string> GetDataFromServerAsync(string url)
        {
            //http://localhost:8080/cockpit/api/collections/get/GameTypeA
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("http://localhost:8080");
            //HttpResponseMessage response = client.GetAsync("/cockpit/api/collections/get/GameTypeA").Result;


            //Create an HTTP client object
            Windows.Web.Http.HttpClient httpClient = new Windows.Web.Http.HttpClient();

            //Add a user-agent header to the GET request. 
            var headers = httpClient.DefaultRequestHeaders;

            //The safe way to add a header value is to use the TryParseAdd method and verify the return value is true,
            //especially if the header value is coming from user input.
            string header = "ie";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }

            header = "Mozilla/5.0 (compatible; MSIE 10.0; Windows NT 6.2; WOW64; Trident/6.0)";
            if (!headers.UserAgent.TryParseAdd(header))
            {
                throw new Exception("Invalid header value: " + header);
            }

            //Uri requestUri = new Uri("http://localhost:8080/cockpit/api/collections/get/GameTypeA");
            Uri requestUri = new Uri(url);

            //Send the GET request asynchronously and retrieve the response as a string.
            Windows.Web.Http.HttpResponseMessage httpResponse = new Windows.Web.Http.HttpResponseMessage();
            string httpResponseBody = "";

            try
            {
               
                   //Send the GET request
                httpResponse = await httpClient.GetAsync(requestUri);
                httpResponse.EnsureSuccessStatusCode();
                httpResponseBody = await httpResponse.Content.ReadAsStringAsync();

                //dynamic collection = JsonConvert.DeserializeObject(httpResponseBody);




                return httpResponseBody;
            }
            catch (Exception ex)
            {
                httpResponseBody = "Error: " + ex.HResult.ToString("X") + " Message: " + ex.Message;
            }

            return null;
        }



        /// <summary>
        ///  //string myJson = "{'Username': 'myusername','Password':'pass'}";
        /// </summary>
        /// <param name="url"></param>
        /// <param name="data"> "{\"data\":{\"PicturePath\":\"466\",\"Scroll\":\"499\"}}"</param>
        /// <returns></returns>
        public async Task<string> RequestDataFromServerAsync(string url,string data)
        {
            //string jsonString = "{\"data\":{\"PicturePath\":\"466\",\"Scroll\":\"499\"}}";

            string jsonString = data;


            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri("http://localhost:64195/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PutAsync(url, content);
            return "Save";


        }

    }
}
