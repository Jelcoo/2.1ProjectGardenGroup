using DAL;
using Model.Enums;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace Logic
{
    public class UserLogic
    {
        UserDao userDao = new UserDao();
        private readonly TicketDao ticketDao = new TicketDao();


        public Employee verifyLogin(string email, string password)
        {
            Employee emp = userDao.getEmployeeByEmail(email);
            if (emp == null) return null;

            string hashedLoginPassword = PasswordTools.hashPassword(emp.password_salt, password);
            if (hashedLoginPassword != emp.password_hashed) return null;

            return emp;
        }

        //methode om user te creeeren
        public void CreateUser(string name, string email, string phoneNumber, string password, Role role)
        {
            userDao.CreateUser(name, email, phoneNumber, password, role);
        }

        //methode o,m alle users te retrieve (read)
        public List<Employee> GetAllUsers()
        {
            return userDao.GetAllEmployees();
        }
        public void UpdateUser(PartialUser partialUser, Role role)
        {
            userDao.UpdateEmployee(partialUser, role);

            UpdateEmployeeNameInTickets(partialUser);

        }

        private void UpdateEmployeeNameInTickets(PartialUser partialUser)
        {
            ticketDao.UpdateEmployeeNameInTickets(partialUser.Id, partialUser.name);
        }

        // verwijder user door ID
        public void DeleteUser(string id)
        {
            userDao.DeleteEmployee(id);
        }
    }
}
