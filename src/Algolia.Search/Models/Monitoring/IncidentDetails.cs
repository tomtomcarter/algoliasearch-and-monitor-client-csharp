using System;
using Algolia.Search.Models.Enums;

namespace Algolia.Search.Models.Monitoring
{
    /// <summary>
    /// The details for an incident
    /// Corresponds to the json
    /// {
    ///     "title": "Degraded performance of primary DNS provider",
    ///     "body": "Due to the ongoing DDoS attack on our primary DNS provider you might experience fallback to secondary provider and temporary increased latency at the connection establishment. Service availability is not impacted.",
    ///     "status": "degraded_performance"
    /// }
    /// </summary>
    public class IncidentDetails
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the body.
        /// </summary>
        /// <value>
        /// The body.
        /// </value>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public string Status { get; set; }

        /// <summary>
        /// Typed (Enum) version of the Status.
        /// </summary>
        /// <remarks>ServerStatus.unsupported_api_server_status will be returned in case of a new unsupported the Algolia Web API status beeing introduced in the futur</remarks>
        /// <value>
        /// The status.
        /// </value>
        public KnownServerStatus KnownServerStatus
        {
            get
            {
                KnownServerStatus status;

                if (!Enum.TryParse(Status, true, out status))
                {
                    status = KnownServerStatus.unsupported_api_server_status;
                }

                return status;
            }
        }
    }
}
