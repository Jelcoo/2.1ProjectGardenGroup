using Model.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class LoginDao : DAO
    {
        public LoginDao() : base()
        {
        }

        public Employee getEmployeeByEmail(string email)
        {
            List<Employee> emp = Db.GetCollection<Employee>("employees")
                .Aggregate()
                .Match(e => e.email == email)
                .Limit(1).ToList();
            if (emp.Count() == 0) return null;
            return emp.First();
        }
    }
}
