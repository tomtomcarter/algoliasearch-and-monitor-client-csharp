using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Algolia.Search.Clients;
using Algolia.Search.Models.Monitoring;

namespace Algolia.Search.MonitoringPlayground
{
    class Program
    {
        static void Main(string[] args)
        {
            Task.WaitAll(Program.TestAlgoliaMonitoringClient());
        }

        public static async Task TestAlgoliaMonitoringClient()
        {
    
            System.Console.WriteLine("~~*\\#/*~~ Algolia Client API playground to test the MonitoringClient ~~*\\#/*~~");

            System.Console.WriteLine("please enter Algolia applicationId: ");
            var applicationId = System.Console.ReadLine();
            System.Console.WriteLine("please enter Algolia monitoringApiKey: ");
            var monitoringApiKey = System.Console.ReadLine();


     
            var client = new MonitoringClient(applicationId, monitoringApiKey);


            System.Console.WriteLine($"{Environment.NewLine}~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            System.Console.WriteLine("### Algolia Monitoring API - Status - Get current ###");

            GetServerStatusResponse response1 = null;
            try
            {
                response1 = await client.GetServerStatusAsync();
                System.Console.WriteLine("*** results below ***");
                Program.DisplayServerStatusResponse(response1);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Oops ! there was a problem.");
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine($".... let's keep running the program anyway !");
            }

            System.Console.WriteLine($"{Environment.NewLine}~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            System.Console.WriteLine("### Algolia Monitoring API - Status - Get current servers ###");
            Program.DisplayUsage();

            GetServerStatusResponse response2 = null;
            while (response2 == null)
            {
                System.Console.WriteLine("You turn to enter server name(s) now: ");
                // almost no input validation done here at the console level
                var serverNameInput = System.Console.ReadLine();

                try
                {
                    response2 = await client.GetServerStatusAsync(serverNameInput);
                    System.Console.WriteLine("*** results below ***");
                    Program.DisplayServerStatusResponse(response2);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"Oops ! there was a problem.");
                    System.Console.WriteLine(ex.Message);
                    System.Console.WriteLine($".... let's keep running the program anyway !");
                }

            }



            System.Console.WriteLine($"{Environment.NewLine}~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            System.Console.WriteLine("### Algolia Monitoring API - Incident - List last incidents  ###");

            ListServerIncidentsResponse response3 = null;
            try
            {
                response3 = await client.ListIncidentsAsync();
                System.Console.WriteLine("*** results below ***");
                Program.DisplayServerIncidentsResponse(response3);
            }
            catch (Exception ex)
            {
                System.Console.WriteLine($"Oops ! there was a problem.");
                System.Console.WriteLine(ex.Message);
                System.Console.WriteLine($".... let's keep running the program anyway !");
            }

            System.Console.WriteLine($"{Environment.NewLine}~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            System.Console.WriteLine("### Algolia Monitoring API - Incident - List last incidents for servers ###");
            Program.DisplayUsage();

            ListServerIncidentsResponse response4 = null;
            while (response4 == null)
            {
                System.Console.WriteLine("You turn to enter server name(s) now: ");
                // almost no input validation done here at the console level
                var serverNameInput = System.Console.ReadLine();

                try
                {
                    response4 = await client.ListIncidentsAsync(serverNameInput);
                    System.Console.WriteLine("*** results below ***");
                    Program.DisplayServerIncidentsResponse(response4);
                }
                catch (Exception ex)
                {
                    System.Console.WriteLine($"Oops ! there was a problem.");
                    System.Console.WriteLine(ex.Message);
                    System.Console.WriteLine($".... let's keep running the program anyway !");
                }
            }

            System.Console.WriteLine($"{Environment.NewLine}~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            System.Console.WriteLine($"We are done. See you soon !");

        }



        #region HidePollutingStuff

        private static void DisplayUsage()
        {
            System.Console.WriteLine(">> Usage : ");
            System.Console.WriteLine(">> \t example input 1 (good server name)  : c9-use");
            System.Console.WriteLine(">> \t example input 2 (good server names) : c9-use, c8-eu");
            System.Console.WriteLine(">> \t example input 3 (wrong server name) : c9-xxx");
            System.Console.WriteLine($">> {Environment.NewLine}\t example of server names : c9-use, c8-eu, s2-au");
        }

        private static string[] GetServerFromInput(string serverNameInput)
        {
            try
            {
                var serverArray = serverNameInput
                    .Split(',');

                return serverArray;
            }
            catch (System.Exception)
            {
                System.Console.WriteLine("Oops ! No luck with the input. Please enter coma separeted server names");
                return null;
            }
        }

        private static void DisplayServerStatusResponse(GetServerStatusResponse response)
        {
            foreach (var server in response.Status)
            {
                System.Console.WriteLine($"Server {server.Key} is running with the status {server.Value}");
            }
        }

        private static void DisplayServerIncidentsResponse(ListServerIncidentsResponse response)
        {
            foreach (var server in response.Incidents)
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
        }

        #endregion HidePollutingStuff
    }
}
