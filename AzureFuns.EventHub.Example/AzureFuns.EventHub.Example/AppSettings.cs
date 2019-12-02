namespace AzureFuns.EventHub.Example
{
    using System; 

    public class AppSettings : IAppSettings
    {
       public string  StorageConnectionString { get; set; }

        public AppSettings(Func<string, string> getter)
        {
            StorageConnectionString = getter("AzureWebJobsStorage");
        }
    }
}
