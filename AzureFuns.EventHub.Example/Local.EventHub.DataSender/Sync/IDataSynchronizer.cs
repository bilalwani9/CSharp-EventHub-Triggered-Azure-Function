namespace Local.EventHub.DataSender
{ 
    using System.Threading.Tasks;

    public interface IDataSynchronizer
    {
        Task Run();
    }
}
