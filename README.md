# Event Hub Triggered Azure Function Example

This Project is an example shows how you can send messages to Azure Event Hub, and how the Event Hub Triggered azure function Listens to Event Hub and writes the message to a Blob Container.

## Arrchitectural High Level Design 

![sampleazurefunc](https://user-images.githubusercontent.com/58431251/69970714-aacc2680-1559-11ea-9c9a-082e1f723878.png)


## Prerequisites
To run this project, make sure that you have:

Azure subscription. If you don't have one, [create a free account](https://azure.microsoft.com/en-us/free/) before you begin.

1. Visual Studio 2019) or later.
2. .NET Standard SDK, version 2.0 or later.

## Create following on Azure Portal for run this Project
  a) Event Hub Namespace and Event Hub
  b) Blog Storage
  
*Note: Create all resource under same Resource Group*

## Create a resource group

1. Sign in to the Azure portal.

2. In the left navigation, click Resource groups. Then click Add.
![ResourceGroups](https://docs.microsoft.com/en-us/azure/event-hubs/media/event-hubs-quickstart-portal/resource-groups1.png)

3. For Subscription, select the name of the Azure subscription in which you want to create the resource group.

4. Type a unique name for the resource group. The system immediately checks to see if the name is available in the currently selected Azure subscription.

5. Select a region for the resource group.

6. Select Review + Create.

 ![ResourceGroupsReview](https://docs.microsoft.com/en-us/azure/event-hubs/media/event-hubs-quickstart-portal/resource-groups2.png)

7. On the Review + Create page, select Create.

## Create an Event Hubs namespace
Follow documentation on Microsoft [Create an event hub using Azure Portal] (https://docs.microsoft.com/en-us/azure/event-hubs/event-hubs-create)


## Create a Blob Storage Container
Follow documentation on Microsoft [Create a container blob](https://docs.microsoft.com/en-us/azure/storage/blobs/storage-quickstart-blobs-portal)

## Replace Connection Strings
Once You have all resources created replace below class with actual Names and Connection Strings.
```
{
  public static class Constants
    {
        public const string EventHubName = "<EVENT HUB NAME>";
        public const string ConsumerGroupName = "<EVENT HUB CONSUMER GROUP NAME>";
        public const string EventHubConnectionStringName = "<EVENT HUB CONNECTION STRING NAME>";
        public const string BlobContainerName = "<BLOB CONTAINER NAME>";
    }
}
```

# Publish Event Hub Triggered Function to Azure
Right Click On Project **AzureFuns.EventHub.Example** and Click on **Publish** 
Follow documentation on Microsoft [How to Publish Azure Function from Visual Studio](https://tutorials.visualstudio.com/first-azure-function/publish)

## Update Azure Function App Setting with below Key Values
            a) AzureWebJobsStorage
            b) EventHubConnectionString

## To Run this project
1. To Sent Events/Alarms to Azure: Run Local.EventHub.DataSender
2. Check Blob Storage container for blobs uploaded by Azure Function.


# Project Details

The Solution contains 4 Projects
1. **Local.EventHub.DataSender:** This project reads the data from json files as source and send the data to event hub, update *appsettings.json* as follows before running this project and make sure your azure function is deployed.

```
{
  "EmployeesDataFileName": "EmployeesData.json",
  "EventHubConnectionString": "<ADD YOUR EVENT HUB CONNECTIONSTRING HERE>",
  "EventHubName": "<ADD YOUR EVENTHUBNAME HERE>"
}

```

EventHubMessageSender sends the messages to event hub implmenting interface IEventHubMessageSender

```
namespace Local.EventHub.DataSender
{
    using AzureFuns.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IEventHubMessageSender
    {
        Task SendMessages(IEnumerable<Employee> employees);
    }
}


```

2. **AzureFuns.EventHub.Example:** This project contains the Azure Function which is *EventHubTrigger* it needs to be published to Azure before you run project *Local.EventHub.DataSender*, this function listens to your event hub messages as employee data and uploads it to Blob Container. 


```
  public static class EventHubTriggeredFuncExample
    {
        [FunctionName(nameof(EventHubTriggeredFuncExample))]
        public static async Task Run([EventHubTrigger(EventHubName, Connection = EventHubConnectionStringName,
             ConsumerGroup = ConsumerGroupName)] EventData[] events, ILogger log)
        {


```

3. **AzureFuns.Common:** This Project is common used and it contains *IAzureBlobWriter*, *IParser* 

 ```
 
 namespace AzureFuns.Common
{
    using System; 
    using System.Threading.Tasks;

    public interface IAzureBlobWriter
    {
        Task<Uri> WriteAsync(string containerName, string blobName, string data);
    }
}

```

# Key Concepts used
1. Design pattern
2. Dependency Injection and IoC Containers
3. Separation of Concern
4. SOLID Principles
5. Azure Functions (Event Hub Triggered)
6. Blob Containers
7. Azure Event Hub
