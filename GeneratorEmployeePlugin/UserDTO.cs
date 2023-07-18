using PhoneApp.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneratorEmployeePlugin
{
    public class UserDTO : DataTransferObject
    {
        public int id { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
    }
}
