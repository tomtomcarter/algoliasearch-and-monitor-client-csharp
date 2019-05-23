using System;
using System.Collections.Generic;
using System.Text;

namespace Algolia.Monitoring.Model
{
    public abstract class StatusBase
    {
        /// <summary>
        /// Gets or sets the status string.
        /// </summary>
        /// <value>
        /// Possible values are `operational`, `degraded_performance`, `partial_outage`, `major_outage`
        /// </value>
        public string StatusString { get; set; }

        /// <summary>
        /// Typed (Enum) version of the Status.
        /// </summary>
        /// <remarks>ServerStatus.unsupported_api_server_status will be returned in case of a new unsupported the Algolia Web API status beeing introduced in the futur</remarks>
        /// <value>
        /// The status.
        /// </value>
        public WellKnownServerStatus Status
        {
            get
            {
                WellKnownServerStatus status;

                if (!Enum.TryParse(StatusString, true, out status))
                {
                    status = WellKnownServerStatus.unsupported_api_server_status;
                }

                return status;
            }
        }
    }
}
