using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AzureFuns.Data.Models;
using Microsoft.Azure.EventHubs;
using Newtonsoft.Json;

namespace Local.EventHub.DataSender
{
    public class EventHubMessageSender : IEventHubMessageSender
    {
        private EventHubClient _eventHubClient;
        public EventHubMessageSender(IAppSettings appSettings)
        {
            var connectionStringBuilder = new EventHubsConnectionStringBuilder(appSettings.EventHubConnectionString)
            {
                EntityPath = appSettings.EventHubName
            };
            _eventHubClient = EventHubClient.CreateFromConnectionString(connectionStringBuilder.ToString());
        }

        public async Task SendMessages(IEnumerable<Employee> employees)
        {
            foreach (var employee in employees)
            {
                var jsonMessage = JsonConvert.SerializeObject(employee);
                Console.WriteLine($"Sending Message to Event Hub");
                Console.WriteLine($"MESSAGE: {jsonMessage} ");
                await _eventHubClient.SendAsync(new EventData(Encoding.UTF8.GetBytes(jsonMessage)));
            }
            Console.WriteLine($"EventHub Client Closing!");
            await _eventHubClient.CloseAsync();
            Console.WriteLine($"EventHub Client Closed!");
        }
    }
}
