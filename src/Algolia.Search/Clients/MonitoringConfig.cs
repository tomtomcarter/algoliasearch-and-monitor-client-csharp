/*
* Copyright (c) 2018 Algolia
* http://www.algolia.com/
*
* Permission is hereby granted, free of charge, to any person obtaining a copy
* of this software and associated documentation files (the "Software"), to deal
* in the Software without restriction, including without limitation the rights
* to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
* copies of the Software, and to permit persons to whom the Software is
* furnished to do so, subject to the following conditions:
*
* The above copyright notice and this permission notice shall be included in
* all copies or substantial portions of the Software.
*
* THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
* IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
* FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
* AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
* LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
* OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
* THE SOFTWARE.
*/

using Algolia.Search.Models.Enums;
using Algolia.Search.Transport;
using Algolia.Search.Utils;
using System;
using System.Collections.Generic;

namespace Algolia.Search.Clients
{
    /// <summary>
    /// Search client configuration
    /// </summary>
    public class MonitoringConfig : AlgoliaConfig
    {
        /// <summary>
        /// The configuration of the Monitoring client
        /// A client should have it's own configuration ie on configuration per client instance
        /// </summary>
        /// <param name="applicationId">Your application ID</param>
        /// <param name="monitoringApiKey">Your Monitoring API Key</param>
        public MonitoringConfig(string applicationId, string monitoringApiKey) : base(applicationId, monitoringApiKey)
        {
            List<StatefulHost> hosts = new List<StatefulHost>
            {
                new StatefulHost
                {
                    Url = "status.algolia.com",
                    Up = true,
                    LastUse = DateTime.UtcNow,
                    Accept = CallType.Read
                },
            };

            DefaultHosts = hosts;
        }
    }
}
