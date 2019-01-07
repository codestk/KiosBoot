using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KiosBoot.Models
{

    using System;
    using System.Collections.Generic;

    using System.Globalization;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    public partial class Topic
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("_id")]
        public string Id { get; set; }

        [JsonProperty("fields")]
        public Field[] Fields { get; set; }

        [JsonProperty("sortable")]
        public bool Sortable { get; set; }

        [JsonProperty("in_menu")]
        public bool InMenu { get; set; }

        [JsonProperty("_created")]
        public long Created { get; set; }

        [JsonProperty("_modified")]
        public long Modified { get; set; }

        [JsonProperty("color")]
        public string Color { get; set; }

        [JsonProperty("acl")]
        public Acl Acl { get; set; }

        [JsonProperty("rules")]
        public Rules Rules { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("itemsCount")]
        public long ItemsCount { get; set; }
    }

    public partial class Acl
    {
        [JsonProperty("")]
        public Empty Empty { get; set; }

        [JsonProperty("public")]
        public Public Public { get; set; }
    }

    public partial class Empty
    {
        [JsonProperty("collection_edit")]
        public bool CollectionEdit { get; set; }
    }

    public partial class Public
    {
        [JsonProperty("entries_view")]
        public bool EntriesView { get; set; }

        [JsonProperty("entries_edit")]
        public bool EntriesEdit { get; set; }

        [JsonProperty("entries_create")]
        public bool EntriesCreate { get; set; }

        [JsonProperty("entries_delete")]
        public bool EntriesDelete { get; set; }
    }

    public partial class Field
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("label")]
        public string Label { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("default")]
        public string Default { get; set; }

        [JsonProperty("info")]
        public string Info { get; set; }

        [JsonProperty("group")]
        public string Group { get; set; }

        [JsonProperty("localize")]
        public bool Localize { get; set; }

        [JsonProperty("options")]
        public object[] Options { get; set; }

        [JsonProperty("width")]
        public string Width { get; set; }

        [JsonProperty("lst")]
        public bool Lst { get; set; }

        [JsonProperty("acl")]
        public object[] Acl { get; set; }
    }

    public partial class Rules
    {
        [JsonProperty("create")]
        public Create Create { get; set; }

        [JsonProperty("read")]
        public Create Read { get; set; }

        [JsonProperty("update")]
        public Create Update { get; set; }

        [JsonProperty("delete")]
        public Create Delete { get; set; }
    }

    public partial class Create
    {
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
    }

}
