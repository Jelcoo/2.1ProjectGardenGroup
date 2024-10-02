using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Employees_Model
    {
        public string name { get; set; }
        public Role_Enum role { get; set; }
        public string email { get; set; }
        public string phone_number { get; set; }
        public string password_hashed { get; set; }
        public string password_salt { get; set; }
    }
}
