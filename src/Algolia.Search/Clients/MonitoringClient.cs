using Algolia.Search.Exceptions;
using Algolia.Search.Http;
using Algolia.Search.Models.ApiKeys;
using Algolia.Search.Models.Batch;
using Algolia.Search.Models.Common;
using Algolia.Search.Models.Enums;
using Algolia.Search.Models.Mcm;
using Algolia.Search.Models.Monitoring;
using Algolia.Search.Models.Personalization;
using Algolia.Search.Models.Search;
using Algolia.Search.Transport;
using Algolia.Search.Utils;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Algolia.Search.Clients
{
    /// <summary>
    /// Algolia monitoring client implementation of <see cref="IMonitoringClient"/>
    /// </summary>
    public class MonitoringClient : IMonitoringClient
    {
        private readonly HttpTransport _transport;
        private readonly AlgoliaConfig _config;

        /// <summary>
        /// Create a new search client for the given appID
        /// </summary>
        /// <param name="applicationId">Your application</param>
        /// <param name="monitoringApiKey">Your API key</param>
        public MonitoringClient(string applicationId, string monitoringApiKey) : this(
            new MonitoringConfig(applicationId, monitoringApiKey), new AlgoliaHttpRequester())
        {
        }

        /// <summary>
        /// Initialize a client with custom config
        /// </summary>
        /// <param name="config">Algolia configuration</param>
        public MonitoringClient(MonitoringConfig config) : this(config, new AlgoliaHttpRequester())
        {
        }

        public MonitoringClient(MonitoringConfig config, IHttpRequester httpRequester)
        {
            if (httpRequester == null)
            {
                throw new ArgumentNullException(nameof(httpRequester), "An httpRequester is required");
            }

            if (config == null)
            {
                throw new ArgumentNullException(nameof(config), "A config is required");
            }

            if (string.IsNullOrWhiteSpace(config.AppId))
            {
                throw new ArgumentNullException(nameof(config.AppId), "Application ID is required");
            }

            if (string.IsNullOrWhiteSpace(config.ApiKey))
            {
                throw new ArgumentNullException(nameof(config.ApiKey), "An API key is required");
            }

            _config = config;
            _transport = new HttpTransport(config, httpRequester);
        }


        public GetServerStatusResponse GetServerStatus(RequestOptions requestOptions = null) =>
            AsyncHelper.RunSync(() => GetServerStatusAsync(requestOptions));

        public async Task<GetServerStatusResponse> GetServerStatusAsync(RequestOptions requestOptions = null, CancellationToken ct = default)
        {
            return await _transport.ExecuteRequestAsync<GetServerStatusResponse>(HttpMethod.Get,
                   "/1/status", CallType.Read,
                   requestOptions, ct)
               .ConfigureAwait(false);
        }

        public GetServerStatusResponse GetServerStatus(string servers, RequestOptions requestOptions = null) =>
            AsyncHelper.RunSync(() => GetServerStatusAsync(servers, requestOptions));

        public async Task<GetServerStatusResponse> GetServerStatusAsync(string servers, RequestOptions requestOptions = null, CancellationToken ct = default)
        {
            return await _transport.ExecuteRequestAsync<GetServerStatusResponse>(HttpMethod.Get,
                   $"/1/status/{WebUtility.UrlEncode(servers)}", CallType.Read,
                   requestOptions, ct)
               .ConfigureAwait(false);
        }

        public ListServerIncidentsResponse ListIncidents(RequestOptions requestOptions = null) =>
            AsyncHelper.RunSync(() => ListIncidentsAsync(requestOptions));

        public async Task<ListServerIncidentsResponse> ListIncidentsAsync(RequestOptions requestOptions = null, CancellationToken ct = default)
        {
            return await _transport.ExecuteRequestAsync<ListServerIncidentsResponse>(HttpMethod.Get,
                  $"/1/incidents", CallType.Read,
                  requestOptions, ct)
              .ConfigureAwait(false);
        }

        public ListServerIncidentsResponse ListIncidents(string servers, RequestOptions requestOptions = null) =>
            AsyncHelper.RunSync(() => ListIncidentsAsync(servers, requestOptions));

        public async Task<ListServerIncidentsResponse> ListIncidentsAsync(string servers, RequestOptions requestOptions = null, CancellationToken ct = default)
        {
            return await _transport.ExecuteRequestAsync<ListServerIncidentsResponse>(HttpMethod.Get,
                  $"/1/incidents/{WebUtility.UrlEncode(servers)}", CallType.Read,
                  requestOptions, ct)
              .ConfigureAwait(false);
        }
    }
}
