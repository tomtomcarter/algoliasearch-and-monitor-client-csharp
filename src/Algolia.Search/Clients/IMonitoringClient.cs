using System.Threading;
using System.Threading.Tasks;
using Algolia.Search.Http;
using Algolia.Search.Models.Monitoring;
using Algolia.Search.Models.Personalization;

namespace Algolia.Search.Clients
{

    /// <summary>
    /// Monitoring Client interface
    /// </summary>
    public interface IMonitoringClient
    {
        /// <summary>
        /// Gets the server status.
        /// </summary>
        /// <param name="requestOptions">The request options.</param>
        /// <returns></returns>
        GetServerStatusResponse GetServerStatus(RequestOptions requestOptions = null);

        /// <summary>
        /// Gets the server status asynchronous.
        /// </summary>
        /// <param name="requestOptions">The request options.</param>
        /// <param name="ct">The ct.</param>
        /// <returns></returns>
        Task<GetServerStatusResponse> GetServerStatusAsync(RequestOptions requestOptions = null,
            CancellationToken ct = default);

        /// <summary>
        /// Gets the server status.
        /// </summary>
        /// <param name="servers">The servers. A comma-separated list of the servers (ex: c4-fr,c3-eu)</param>
        /// <param name="requestOptions">The request options.</param>
        /// <returns></returns>
        GetServerStatusResponse GetServerStatus(string servers, RequestOptions requestOptions = null);

        /// <summary>
        /// Gets the server status asynchronous.
        /// </summary>
        /// <param name="servers">The servers. A comma-separated list of the servers (ex: c4-fr,c3-eu)</param>
        /// <param name="requestOptions">The request options.</param>
        /// <param name="ct">The ct.</param>
        /// <returns></returns>
        Task<GetServerStatusResponse> GetServerStatusAsync(string servers, RequestOptions requestOptions = null,
            CancellationToken ct = default);

        /// <summary>
        /// Lists the incidents.
        /// </summary>
        /// <param name="requestOptions">The request options.</param>
        /// <returns></returns>
        ListServerIncidentsResponse ListIncidents(RequestOptions requestOptions = null);


        /// <summary>
        /// Lists the incidents asynchronous.
        /// </summary>
        /// <param name="requestOptions">The request options.</param>
        /// <param name="ct">The ct.</param>
        /// <returns></returns>
        Task<ListServerIncidentsResponse> ListIncidentsAsync(RequestOptions requestOptions = null,
            CancellationToken ct = default);


        /// <summary>
        /// Lists the incidents.
        /// </summary>
        /// <param name="servers">The servers. A comma-separated list of the servers (ex: c4-fr,c3-eu)</param>
        /// <param name="requestOptions">The request options.</param>
        /// <returns></returns>
        ListServerIncidentsResponse ListIncidents(string servers, RequestOptions requestOptions = null);


        /// <summary>
        /// Lists the incidents asynchronous.
        /// </summary>
        /// <param name="servers">The servers. A comma-separated list of the servers (ex: c4-fr,c3-eu)</param>
        /// <param name="requestOptions">The request options.</param>
        /// <param name="ct">The ct.</param>
        /// <returns></returns>
        Task<ListServerIncidentsResponse> ListIncidentsAsync(string servers, RequestOptions requestOptions = null,
            CancellationToken ct = default);
    }
}
