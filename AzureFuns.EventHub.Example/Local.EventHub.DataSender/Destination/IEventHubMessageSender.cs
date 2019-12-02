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
