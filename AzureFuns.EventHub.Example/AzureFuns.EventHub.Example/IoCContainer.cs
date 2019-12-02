namespace AzureFuns.EventHub.Example
{
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using AzureFuns.Common;
    using AzureFuns.Data.Models;

    public  class IoCContainer
    {
        private static IServiceProvider _provider;


        public static IServiceProvider Create()
        {
            return _provider ?? (_provider = ConfigureServices());
        }

        private static IServiceProvider ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();

            var settings = new AppSettings(s => Environment.GetEnvironmentVariable(s, EnvironmentVariableTarget.Process));

            services.AddSingleton<IAppSettings>(settings);
            services.AddScoped<IAzureBlobWriter>(s => new AzureBlobWriter(settings.StorageConnectionString)); 
            services.AddScoped<IParser<Employee>, EmployeeDataParser>();  

            return services.BuildServiceProvider();
        }
    }
}
