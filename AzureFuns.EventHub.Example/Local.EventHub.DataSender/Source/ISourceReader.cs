namespace Local.EventHub.DataSender
{
    using AzureFuns.Data.Models; 
    using System.Collections.Generic; 
    using System.Threading.Tasks;

    public interface ISourceReader
    {
        Task<IEnumerable<Employee>> ReadFromJsonFiles();
    }
}
