using Newtonsoft.Json;
using System.Collections.Generic;

namespace Algolia.Monitoring.Model
{
    public class RawServerStatusResponseObject
    {
        [JsonProperty("status")]
        public Dictionary<string, string> Status { get; set; }
    }
}