using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFuns.EventHub.Example
{
    public class AppSettings : IAppSettings
    {
       public string  StorageConnectionString { get; set; }

        public AppSettings(Func<string, string> getter)
        {
            StorageConnectionString = getter("AzureWebJobsStorage");
        }
    }
}
