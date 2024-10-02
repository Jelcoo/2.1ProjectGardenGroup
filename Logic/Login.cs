using DAL;
using Model.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class Login
    {
        private LoginDao dao;
        public Login()
        {
            dao = new LoginDao();
        }

        public Employee verifyLoginUser(string username, string password)
        {
            Employee usernameMatchedEmployee = dao.getEmployeeByUsername(username);
            return null;
        }
    }
}
