using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class ApplicationStore
    {
        private Employee loggedInUser;
        private static ApplicationStore instance;

        public static ApplicationStore GetInstance()
        {
            if (instance == null)
                instance = new ApplicationStore();

            return instance;
        }

        public Employee getLoggedInUser()
        {
            return loggedInUser;
        }
        public void setLoggedInUser(Employee employee)
        {
            loggedInUser = employee;
        }
    }
}
