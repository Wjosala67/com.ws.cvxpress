using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace com.ws.cvxpress.Models
{
    [JsonObject]
    public class Target
    {
        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("devices")]
        public IEnumerable<string> Devices { get; set; }
    }
}
