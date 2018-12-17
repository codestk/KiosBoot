using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiosBoot.Helpers.ConvertModel
{
    public partial class DisplayModel
    {
        [JsonProperty("fields")]
        public Fields Fields { get; set; }

        [JsonProperty("entries")]
        public Entry[] Entries { get; set; }

        [JsonProperty("total")]
        public long Total { get; set; }
    }

    public partial class Entry
    {
        [JsonProperty("Date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("Topic")]
        public string Topic { get; set; }

        [JsonProperty("Text")]
        public string Text { get; set; }

        [JsonProperty("Photo")]
        public Logo Photo { get; set; }

        [JsonProperty("Logo")]
        public Logo Logo { get; set; }

        [JsonProperty("VDO")]
        public Logo Vdo { get; set; }

     
    }


    public partial class Logo
    {
        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("mime")]
        public string Mime { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("tags")]
        public object[] Tags { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("image")]
        public bool Image { get; set; }

        [JsonProperty("video")]
        public bool Video { get; set; }

        [JsonProperty("audio")]
        public bool Audio { get; set; }

        [JsonProperty("archive")]
        public bool Archive { get; set; }

        [JsonProperty("document")]
        public bool Document { get; set; }

        [JsonProperty("code")]
        public bool Code { get; set; }

        [JsonProperty("created")]
        public long Created { get; set; }

        [JsonProperty("modified")]
        public long Modified { get; set; }

        [JsonProperty("_by")]
        public string By { get; set; }

        [JsonProperty("width", NullValueHandling = NullValueHandling.Ignore)]
        public long? Width { get; set; }

        [JsonProperty("height", NullValueHandling = NullValueHandling.Ignore)]
        public long? Height { get; set; }

        [JsonProperty("colors", NullValueHandling = NullValueHandling.Ignore)]
        public string[] Colors { get; set; }

        [JsonProperty("folder")]
        public string Folder { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }
    }

    public partial class Fields
    {
        [JsonProperty("Date")]
        public Date Date { get; set; }

        [JsonProperty("Topic")]
        public Date Topic { get; set; }

        [JsonProperty("Text")]
        public Date Text { get; set; }

        [JsonProperty("Photo")]
        public Date Photo { get; set; }

        [JsonProperty("Logo")]
        public Date Logo { get; set; }

        [JsonProperty("VDO")]
        public Date Vdo { get; set; }
    }

    public partial class Date
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("localize")]
        public bool Localize { get; set; }

        [JsonProperty("options")]
        public object[] Options { get; set; }
    }
}
