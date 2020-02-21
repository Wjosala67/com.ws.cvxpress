using System;
using System.Collections.Generic;

using System.Globalization;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
namespace com.ws.cvxpress.Models
{
    public partial class App_Con
    {

        [JsonProperty("Description")]
        public string Description { get; set; }

        [JsonProperty("IDConfiguration")]
        public int IdConfiguration { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Value")]
        public string Value { get; set; }

    }

    public partial class App_Con
    {
        public static IList<App_Con> FromJson(string json) => JsonConvert.DeserializeObject<IList<App_Con>>(json, Converter.Settings);
    }

    public static class Serialize
    {
        public static string ToJson(this App_Con self) => JsonConvert.SerializeObject(self, Converter.Settings);
    }

    internal static class Converter
    {
        public static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
            Converters =
            {
                new IsoDateTimeConverter { DateTimeStyles = DateTimeStyles.AssumeUniversal }
            },
        };
    }
}
