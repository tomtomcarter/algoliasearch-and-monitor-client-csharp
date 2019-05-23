using Newtonsoft.Json;

namespace Algolia.Search.Models.Monitoring
{
    /// <summary>
    /// An incident object
    /// </summary>
    public class Incident
    {
        /// <summary>
        /// Gets or sets the friendly name for t.
        /// </summary>
        /// <value>
        /// The friendly name for t.
        /// </value>
        [JsonProperty(PropertyName = "t")]
        public long FriendlyNameForT { get; set; }

        /// <summary>
        /// Gets or sets the incident details friendly name for v.
        /// </summary>
        /// <value>
        /// The incident details friendly name for v.
        /// </value>
        [JsonProperty(PropertyName = "v")]
        public IncidentDetails IncidentDetailsFriendlyNameForV { get; set; }

    }
}
