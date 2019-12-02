using AzureFuns.Data.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AzureFuns.Common
{
    public class EmployeeDataParser : IParser<Employee>
    {
        private readonly JsonSerializerSettings _settings;

        public EmployeeDataParser()
        {
            var resolver = new DefaultContractResolver
            {
                NamingStrategy = new CamelCaseNamingStrategy()
            };

            _settings = new JsonSerializerSettings
            {
                ContractResolver = resolver, 
                Formatting = Formatting.Indented
            };
        }

        public Employee Parse(string input)
        {
            return JsonConvert.DeserializeObject<Employee>(input, _settings);
        }

        public Employee ReadAndParse(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                var input = reader.ReadToEnd();
                return Parse(input);
            }
        }
    }
}
