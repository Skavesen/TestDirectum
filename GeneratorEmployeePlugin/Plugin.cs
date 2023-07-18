using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using PhoneApp.Domain;
using PhoneApp.Domain.Attributes;
using PhoneApp.Domain.DTO;
using PhoneApp.Domain.Interfaces;

namespace GeneratorEmployeePlugin
{
    [Author(Name = "Vadim Bondarenko")]
    public class Plugin : IPluggable
    {
        private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        public IEnumerable<DataTransferObject> Run(IEnumerable<DataTransferObject> args)
        {
            logger.Info("Generating employees");

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                HttpResponseMessage response = client.GetAsync("https://jsonplaceholder.typicode.com/users").Result;
                if (response.IsSuccessStatusCode)
                {
                    string json = response.Content.ReadAsStringAsync().Result;
                    var usersList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UserDTO>>(json);

                    var employeesList = usersList.Select(u => new EmployeesDTO
                    {
                        Name = u.name,
                    }).ToList();

                    foreach (var employee in employeesList)
                    {
                        employee.AddPhone(employee.Phone);
                    }
                    logger.Info($"Generated {employeesList.Count()} employees");
                    return employeesList.Cast<DataTransferObject>();
                }
                else
                {
                    logger.Error($"API request failed: {response.ReasonPhrase}");
                    return null;
                }
            }
        }
    }
}