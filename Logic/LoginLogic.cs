using DAL;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Logic
{
    public class LoginLogic
    {
        private LoginDao dao;
        public LoginLogic()
        {
            dao = new LoginDao();
        }

        public Employee verifyLogin(string email, string password)
        {
            Employee emp = dao.getEmployeeByEmail(email);
            if (emp == null) return null;

            string hashedLoginPassword = hashPassword(emp.password_salt, password);
            if (hashedLoginPassword != emp.password_hashed) return null;

            return emp;
        }

        private string hashPassword(string salt, string password)
        {
            string salty = salt + password;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(salty));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
