namespace Algolia.Monitoring.Model
{
    /// <summary>
    /// Server status recognized by this Monitoring API Client
    ///
    /// Note - option names :
    ///    - the enum options names are note following the .net generaly accepted PascalCasing convention.
    ///    - the reason for this is to better stay aligned with the Algolia Web API naming convension for server status
    ///    - see https://www.algolia.com/doc/rest-api/monitoring/
    ///
    /// Note - special option unsupported_api_server_status :
    ///     - this is to allow any possible further new server status names returned by the Algolia Web API
    ///     - any new status names returned by the Algolia Web API unknown to this .net Monitoring API Client will be set to ServerStatus.unsupported_api_server_status
    ///     - in such as case, you may use the (string) Server.StatusString instead of the (ServerStatus) Server.Status to see what the Algolia Web API returned
    /// </summary>
    public enum WellKnownServerStatus
    {
        operational,
        degraded_performance,
        partial_outage,
        major_outage,

        unsupported_api_server_status // special  - see comment
    }
}