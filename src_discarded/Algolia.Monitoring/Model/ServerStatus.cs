using System;

namespace Algolia.Monitoring.Model
{
    public class ServerStatus : StatusBase
    {
        /// <summary>
        /// Gets or sets the name of the Server/Cluster.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string ServerName { get; set; }

        public override string ToString()
        {
            return $"{ServerName} : {StatusString}";
        }
    }
}