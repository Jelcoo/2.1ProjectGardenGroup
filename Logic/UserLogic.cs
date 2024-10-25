using DAL;
using Model.Enums;
using Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace Logic
{
    public class UserLogic
    {
        UserDao userDao = new UserDao();

        public Employee verifyLogin(string email, string password)
        {
            Employee emp = userDao.getEmployeeByEmail(email);
            if (emp == null) return null;

            string hashedLoginPassword = PasswordTools.hashPassword(emp.password_salt, password);
            if (hashedLoginPassword != emp.password_hashed) return null;

            return emp;
        }
        public Employee getEmployeeByEmail(string email)
        {
            Employee emp = userDao.getEmployeeByEmail(email);
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
        }

        // verwijder user door ID
        public void DeleteUser(string id)
        {
            userDao.DeleteEmployee(id);
        }

        public void SendResetEmail(Employee employee)
        {
            string emailContent = File.ReadAllText("..\\..\\..\\password-reset.html");
            emailContent = emailContent
                .Replace("{{name}}", employee.name)
                .Replace("{{code}}", "DEBUG");
            MailMessage mailMessage = MailTools.ConstructMailMessage(employee.email, "Password reset", emailContent);
            mailMessage.IsBodyHtml = true;
            SmtpClient smtpClient = MailTools.GetSmtpClient();
            try
            {
                smtpClient.Send(mailMessage);
            } catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}
