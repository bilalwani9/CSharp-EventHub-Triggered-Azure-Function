namespace Local.EventHub.DataSender
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;
    using AzureFuns.Data.Models;
    using Newtonsoft.Json;

    public class SourceReader : ISourceReader
    {
        private string _employeeJsonSourceFilePath;
        public SourceReader(IAppSettings appSettings)
        {
            _employeeJsonSourceFilePath = Path.Combine(AppContext.BaseDirectory, $"Data/{appSettings.EmployeesDataFileName}");
        }
        public async Task<IEnumerable<Employee>> ReadFromJsonFiles()
        {
            var jsonSource = await File.ReadAllTextAsync(_employeeJsonSourceFilePath);
            return JsonConvert.DeserializeObject<List<Employee>>(jsonSource);
        }
    }
}
