namespace Local.EventHub.DataSender
{
    public interface IAppSettings
    {
        string EmployeesDataFileName { get; }
        string EventHubConnectionString { get; }

        string EventHubName { get; }

    }
}
