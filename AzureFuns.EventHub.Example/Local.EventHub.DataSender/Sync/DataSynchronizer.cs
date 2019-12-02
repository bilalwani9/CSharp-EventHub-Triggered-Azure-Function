namespace Local.EventHub.DataSender
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class DataSynchronizer : IDataSynchronizer
    {
        private readonly ISourceReader _dataSourceReader;
        private readonly IEventHubMessageSender _eventHubMessageSender;

        public DataSynchronizer(ISourceReader sourceReader, IEventHubMessageSender eventHubMessageSender)
        {
            _dataSourceReader = sourceReader;
            _eventHubMessageSender = eventHubMessageSender;
        }
        public async Task Run()
        {
            var employees = await _dataSourceReader.ReadFromJsonFiles();
            Console.WriteLine($"Employee Data Raw Alarms Count {employees.Count()}");
            await _eventHubMessageSender.SendMessages(employees);
        }
    }
}
