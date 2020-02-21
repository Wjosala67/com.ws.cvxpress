using System;
using Newtonsoft.Json;

namespace com.ws.cvxpress.Models
{
    [JsonObject]
    public class mPush
    {
        [JsonProperty("notification_target")]
        public Target Target { get; set; }

        [JsonProperty("notification_content")]
        public Content Content { get; set; }
    }
}
