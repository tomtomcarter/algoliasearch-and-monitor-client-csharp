using Algolia.Monitoring.Client;
using Algolia.Monitoring.Exception;
using Algolia.Monitoring.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Algolia.Monitoring.Console
{
    internal class Program
    {
        private static void Main(string[] args)
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

            System.Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            System.Console.WriteLine("### Algolia Monitoring API - Status - Get current ###");
            System.Console.WriteLine("*** results below ***");
            ServerStatus server = null;
            try
            {
                server = await client.GetCurrentStatusServerAsync();
                System.Console.WriteLine(server.ToString());
            }
            catch (MonitoringClientException ex)
            {
                System.Console.WriteLine(ex.Message);
            }

            System.Console.WriteLine("~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~");
            System.Console.WriteLine("### Algolia Monitoring API - Status - Get current servers ###");
            Program.DisplayStatusUsage();

            string[] servers = null;
            while (servers == null)
            {
                System.Console.WriteLine("You turn to enter server name(s) now: ");
                // almost no input validation done here at the console level
                var serverNameInput = System.Console.ReadLine();

                servers = Program.GetServerFromInput(serverNameInput);
            }
            System.Console.WriteLine("*** results below ***");

            Dictionary<string, ServerStatus> serversDic = null;
            try
            {
                serversDic = await client.GetCurrentStatusServersAsync(servers);
                Program.DisplayStatus(serversDic);
            }
            catch (MonitoringClientException ex)
            {
                System.Console.WriteLine($"An exception was thrown by the Monitoring Client API. The exception message is : {ex.Message}");
            }




        }

        #region HidePollutingStuff

        public static void DisplayStatusUsage()
        {
            System.Console.WriteLine(">> Usage : ");
            System.Console.WriteLine(">> \t example input 1 (good server name)  : c9-use");
            System.Console.WriteLine(">> \t example input 2 (good server names) : c9-use, c8-eu");
            System.Console.WriteLine(">> \t example input 3 (wrong server name) : c9-xxx");
            System.Console.WriteLine($">> {Environment.NewLine}\t example of server names : c9-use, c8-eu, s2-au");
        }

        public static string[] GetServerFromInput(string serverNameInput)
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

        public static void DisplayStatus(Dictionary<string, ServerStatus> servers)
        {
            foreach (var server in servers.Values)
            {
                System.Console.WriteLine(server.ToString());
            }
        }

        #endregion HidePollutingStuff
    }
}
