# Algolia Monitoring API Client for .NET

_built in**.NET Standard 2.0**_  
_note this is a first 'rough'  release_

## Introduction

This idea here was to develop an API client to handle the [4 status endpoints](https://www.algolia.com/doc/rest-api/monitoring/) of the Algolia monitoring API:

- `/1/status`
- `/1/status/{servers}`
- `/1/incidents`
- `/1/incidents/{servers}`



## Getting started

You can use the Algolia c# Search API Client library.
A new MonitoringClient class has been added.
To use the library in a project, you can clone the GitHub repo and build the `Algolia.Search` project located in the ~/src folder.
Alternatively you can find in the ~/dist folder of the GitHub repo a buit version of the libraries if you don't want to bother building the solution. Simply reference this Algolia.Search.dll  into you project.

You may use it as per sample below.


```csharp
const string applicationId = "XXXXXXXXXX";
const string monitoringApiKey = "XXXXXXXXXXXXXXXXXXXXXXXXXXX";

var client = new MonitoringClient(applicationId, monitoringApiKey);

var response1 = await client.GetServerStatusAsync();
foreach (var server in response1.Status)
{
    System.Console.WriteLine($"Server {server.Key} is running with the status {server.Value}");
}



var response2 = await client.GetServerStatusAsync();
foreach (var server in response2.Incidents)
{
    System.Console.WriteLine($"Server {server.Key} is running with {server.Value.Count } incident(s) ");
    if (server.Value.Count > 0)
    {
        int index = 1;
        foreach (var incident in server.Value)
        {
            System.Console.WriteLine($"... incident n°{index} : 't' property={incident.FriendlyNameForT}");
            System.Console.WriteLine($"........................ 'v' object property Title={incident.IncidentDetailsFriendlyNameForV.Title}");
            System.Console.WriteLine($"........................ 'v' object property Body={incident.IncidentDetailsFriendlyNameForV.Body}");
            System.Console.WriteLine($"........................ 'v' object property Status={incident.IncidentDetailsFriendlyNameForV.Status}");
            System.Console.WriteLine($"........................ 'v' object property Status(enum)={incident.IncidentDetailsFriendlyNameForV.KnownServerStatus}");

            index++;
        }
    }
}

```

The return object are POCO classes as shown below:

### Status endpoint
```csharp
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
```

### Incident endpoint

```csharp

public class ListServerIncidentsResponse
{
    /// <summary>
    /// Gets or sets the status.
    /// The key will be the server name
    /// The value will be the status ;  possible values are `operational`, `degraded_performance`, `partial_outage`, `major_outage`
    /// </summary>
    /// <value>
    /// The status.
    /// </value>
    public Dictionary<string, List<Incident>> Incidents { get; set; }
}

public class Incident
{
    [JsonProperty(PropertyName = "t")]
    public long FriendlyNameForT { get; set; }

    [JsonProperty(PropertyName = "v")]
    public IncidentDetails IncidentDetailsFriendlyNameForV { get; set; }
}

public class IncidentDetails
{
    public string Title { get; set; }

    public string Body { get; set; }

    public string Status { get; set; }

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
```

_Note_ : You may also run the Algolia.Search.MonitoringPlayground console app to try the app once you have build the solution. It covers all 4 cases

## My choises

I initialy started building a very small client* to cover the following 2 API calls
- `/1/status`
- `/1/status/{servers}`

Then I realized it would be smarter to enhance the existing Search API Client library which already had other client then the SeachClient.
I built on top of the existing library.



*You may find, build and run this client in the ~/src_discarded folder of the GitHub repo.