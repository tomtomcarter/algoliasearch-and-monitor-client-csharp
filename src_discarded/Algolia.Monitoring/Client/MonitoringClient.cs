using Algolia.Monitoring.Exception;
using Algolia.Monitoring.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Algolia.Monitoring.Client
{
    public class MonitoringClient
    {
        /* Consider moving this in a config class */
        private readonly string _applicationId;
        private readonly string _apiKey;
        private const string AlgoliaHttpApplicationIdHeader = "X-Algolia-Application-Id";
        private const string AlgoliaHttpApiKeyHeader = "X-Algolia-API-Key";
        private const string AlgoliaRootMonitoringApiUrl = "https://status.algolia.com";

        /// <summary>
        /// The HTTP client ; we don't want this to be disposed
        /// </summary>
        private readonly HttpClient _httpClient;

        public MonitoringClient(string applicationId, string apiKey)
        {
            if (String.IsNullOrWhiteSpace(applicationId))
            {
                throw new ArgumentNullException(nameof(applicationId), $"Argument error! You must provide the {nameof(applicationId)}");
            }

            if (String.IsNullOrWhiteSpace(apiKey))
            {
                throw new ArgumentNullException(nameof(apiKey), $"Argument error! You must provide the {nameof(apiKey)}");
            }

            this._applicationId = applicationId;
            this._apiKey = apiKey;
            this._httpClient = new HttpClient();
        }

        /// <summary>
        /// Returns the server status
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ServerStatus> GetCurrentStatusServerAsync(CancellationToken cancellationToken = default)
        {
            var serverStatus = await GetCurrentStatusServersAsync(string.Empty, cancellationToken);

            return serverStatus.First().Value;
        }

        /// <summary>
        /// Gets the status asynchronous.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token. Can be used if we have an unfinished call but it is no longer needed (if for instance the call made is no longer relevent to the caller)</param>
        /// <param name="">The .</param>
        /// <returns></returns>
        public async Task<Dictionary<string, ServerStatus>> GetCurrentStatusServersAsync(string[] servers, CancellationToken cancellationToken = default)
        {
            var serversString = string.Join(",", servers);
            return await GetCurrentStatusServersAsync(serversString, cancellationToken);
        }

        /// <summary>
        /// Gets the status servers asynchronous.
        /// </summary>
        /// <param name="servers">The servers.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Task<Dictionary<string, ServerStatus>> with the key beeing the server/cluster name</returns>
        /// <exception cref="Algolia.Monitoring.Exception.MonitoringClientException"></exception>
        public async Task<Dictionary<string, ServerStatus>> GetCurrentStatusServersAsync(string servers, CancellationToken cancellationToken = default)
        {
            var serverStatus = new Dictionary<string, ServerStatus>();

            var urlApi = $"{AlgoliaRootMonitoringApiUrl}/1/status";

            urlApi = AddServersParameters(urlApi, servers);

            var httpRequestMessage = PrepareHttpRequestMessage(HttpMethod.Get, urlApi);

            try
            {
                using (httpRequestMessage)
                using (HttpResponseMessage httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage, cancellationToken))
                {
                    if (httpResponseMessage.IsSuccessStatusCode)
                    {
                        var rawStatus = await httpResponseMessage.Content.ReadAsStringAsync();

                        // deserialize to raw object > TODO : find a proper way to use deserializer to get directly proper result
                        var deserialized = JsonConvert.DeserializeObject<RawServerStatusResponseObject>(rawStatus);

                        // map to the POPO object we want
                        foreach (KeyValuePair<string, string> server in deserialized.Status)
                        {
                            serverStatus.Add(server.Key, new ServerStatus { ServerName = server.Key, StatusString = server.Value });
                        }

                        return serverStatus;
                    }
                    else
                    {
                        var rawResponse = await httpResponseMessage.Content.ReadAsStringAsync();
                        throw new MonitoringClientException(rawResponse, (int)httpResponseMessage.StatusCode);
                    }
                }
            }
            catch (TimeoutException timeOutException)
            {
                throw new MonitoringClientException("Sorry something went wrong. Your requeste timedout. Check innerException", timeOutException);
            }
        }



       

        private string AddServersParameters(string url, string parametersServers)
        {
            if (!String.IsNullOrWhiteSpace(parametersServers))
            {
                url += $"/{WebUtility.UrlEncode(parametersServers.Replace(" ", string.Empty))}";
            }

            return url;
        }

        private HttpRequestMessage PrepareHttpRequestMessage(HttpMethod method, string url)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(method, url);

            // add headers
            // fun fact note : failing to add headers will not lead to a failed query but with a result as a list of all clusters/replicas (not only the one set for you application Id)
            httpRequestMessage.Headers.Add(AlgoliaHttpApplicationIdHeader, _applicationId);
            httpRequestMessage.Headers.Add(AlgoliaHttpApiKeyHeader, _apiKey);

            return httpRequestMessage;
        }
    }
}