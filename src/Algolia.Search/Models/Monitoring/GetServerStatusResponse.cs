using System;
using System.Collections.Generic;
using System.Text;

namespace Algolia.Search.Models.Monitoring
{
    /// <summary>
    /// The response object
    /// </summary>
    public class GetServerStatusResponse
    {
        /// <summary>
        /// Gets or sets the status.
        /// The key will be the server name
        /// The value will be the status ;  possible values are `operational`, `degraded_performance`, `partial_outage`, `major_outage`
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public Dictionary<string, string> Status { get; set; }
    }
}
