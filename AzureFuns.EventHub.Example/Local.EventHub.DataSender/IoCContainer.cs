namespace Local.EventHub.DataSender
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.IO;

    public class IoCContainer
    {
        private static IServiceProvider _provider;

        public static IServiceProvider Create()
        {
            return _provider ?? (_provider = ConfigureServices());
        }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var settings = GetSettings();

            services.AddSingleton<IAppSettings>(settings);

            services.AddTransient<ISourceReader, SourceReader>();
            services.AddTransient<IEventHubMessageSender, EventHubMessageSender>();
            services.AddTransient<IDataSynchronizer, DataSynchronizer>();

            return services.BuildServiceProvider();
        }


        private static IAppSettings GetSettings()
        {
            var configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
               .AddEnvironmentVariables();

            var settings = new AppSettings();
            configuration.Build().Bind(settings);
            return settings;
        }
    }
}
