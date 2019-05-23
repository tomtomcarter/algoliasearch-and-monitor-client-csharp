using System;
using System.Collections.Generic;
using System.Text;

namespace Algolia.Monitoring.Exception
{
    public class MonitoringClientException : System.Exception
    {
        /// <summary>
        /// Gets or sets the HTTP status code.
        /// </summary>
        /// <remarks>Set as int to prevent the caller of the API to add a ref. to System.Net.HttpStatusCode</remarks>
        /// <value>
        /// The HTTP status code.
        /// </value>
        public int HttpStatusCode { get; set; }
        public MonitoringClientException() { }

        public MonitoringClientException(string message) : base(message) { }

        public MonitoringClientException(string message, System.Exception innerException) : base(message, innerException) { }

        public MonitoringClientException(string message, int httpStatusCode) : base(message)
        {
            HttpStatusCode = httpStatusCode;
        }
        public MonitoringClientException(string message, System.Exception innerException, int httpStatusCode) : base(message, innerException)
        {
            HttpStatusCode = httpStatusCode;
        }
    }
}
