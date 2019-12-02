using System;
using System.Collections.Generic;
using System.Text;

namespace AzureFuns.EventHub.Example
{
    interface IAppSettings
    {
        string StorageConnectionString { get; }
    }
}
