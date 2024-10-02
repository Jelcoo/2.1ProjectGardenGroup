using MongoDB.Driver;
using Model.models;
using System.Linq;
using System.Collections.Generic;

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
