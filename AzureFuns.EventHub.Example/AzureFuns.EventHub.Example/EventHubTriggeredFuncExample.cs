namespace AzureFuns.EventHub.Example
{
    using System; 
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Azure.EventHubs;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.DependencyInjection;
    using AzureFuns.Common;
    using AzureFuns.Data.Models;
    using static AzureFuns.Common.Constants;

    public static class EventHubTriggeredFuncExample
    {
        [FunctionName(nameof(EventHubTriggeredFuncExample))]
        public static async Task Run([EventHubTrigger(EventHubName, Connection = EventHubConnectionStringName,
             ConsumerGroup = ConsumerGroupName)] EventData[] events, ILogger log)
        {

            try
            {
                var container = IoCContainer.Create();
                var azureBlobWriter = container.GetRequiredService<IAzureBlobWriter>();
                var employeeDataParser = container.GetRequiredService<IParser<Employee>>();

                foreach (EventData eventData in events)
                {
                    try
                    {
                        string messageBody = Encoding.UTF8.GetString(eventData.Body.Array, eventData.Body.Offset, eventData.Body.Count);

                        log.LogInformation($"C# Event Hub trigger function processed a message: {messageBody}");

                        var employeeData = employeeDataParser.Parse(messageBody);

                        log.LogInformation($"DeserializeObject Employee Data Success Id: {employeeData.Id}");

                        var blobName = $"{employeeData.Id}.json";

                        await azureBlobWriter.WriteAsync(BlobContainerName, blobName, messageBody.ToString());

                        log.LogInformation($"blob upload success blobName: {blobName}");
                    }
                    catch (Exception e)
                    {
                        log.LogError($"Loop Error: {e.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                log.LogError($"Main Function Error: {ex.Message}");
            }
        }
    }
}
