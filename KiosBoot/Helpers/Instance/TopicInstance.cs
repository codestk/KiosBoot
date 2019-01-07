using KiosBoot.Helpers.Config;
using KiosBoot.Helpers.Server;

namespace KiosBoot.Helpers.Instance
{
    public class TopicInstance
    {
        public static int CurrentTopic { get; set; }

        public static string GetCollectionDefinationUrl(string item)

        {
            string url = DataConfig.ApiDomain() + "/api/collections/collection/TopicX" + item;
            var api = new ApiData();

            return url;
        }

        public static string GetDataCollectionUrl()

        {
            string url = DataConfig.ApiDomain() + "/api/collections/get/TopicX" + CurrentTopic;

            return url;
        }
    }
}
