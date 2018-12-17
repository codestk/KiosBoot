﻿// <auto-generated />
//
// To parse this JSON data, add NuGet 'Newtonsoft.Json' then do:
//
//    using KiosBoot.Helpers.ConvertModel;
//
//    var scrollInfo = ScrollInfo.FromJson(jsonString);

namespace KiosBoot.Helpers.ConvertModel
{
    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    public partial class ScrollInfoModel
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
        [JsonProperty("PicturePath")]
        public string PicturePath { get; set; }

        [JsonProperty("Scroll")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Scroll { get; set; }

       
    }


    public partial class Fields
    {
        [JsonProperty("PicturePath")]
        public PicturePath PicturePath { get; set; }

        [JsonProperty("Scroll")]
        public PicturePath Scroll { get; set; }
    }

    public partial class PicturePath
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

    
 
    internal class ParseStringConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(long) || t == typeof(long?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            if (reader.TokenType == JsonToken.Null) return null;
            var value = serializer.Deserialize<string>(reader);
            long l;
            if (Int64.TryParse(value, out l))
            {
                return l;
            }
            throw new Exception("Cannot unmarshal type long");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            if (untypedValue == null)
            {
                serializer.Serialize(writer, null);
                return;
            }
            var value = (long)untypedValue;
            serializer.Serialize(writer, value.ToString());
            return;
        }

        public static readonly ParseStringConverter Singleton = new ParseStringConverter();
    }
}
