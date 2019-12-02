namespace Local.EventHub.DataSender
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.Threading.Tasks;

    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Local.EventHub.DataSender job started.");

            var container = IoCContainer.Create();
            var syncService = container.GetRequiredService<IDataSynchronizer>();
            await syncService.Run();

            Console.WriteLine("Local.EventHub.DataSender job has been completed.");
            Console.WriteLine("======================================================");
            Console.WriteLine("PRESS ANY KEY TO EXIT!!");
            Console.WriteLine("======================================================");
            Console.ReadKey();
        }
    }
}
