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
        const int CODE_LENGTH = 6;

        public Employee VerifyLogin(string email, string password)
        {
            Employee emp = userDao.getEmployeeByEmail(email);
            if (emp == null) return null;

            string hashedLoginPassword = PasswordTools.HashPassword(emp.password_salt, password);
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
            if (employee == null) return;

            string code = GetResetCode(employee);
            string emailContent = File.ReadAllText("..\\..\\..\\password-reset.html");
            emailContent = emailContent
                .Replace("{{name}}", employee.name)
                .Replace("{{code}}", code);
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

        private string GetResetCode(Employee employee)
        {
            string code = PasswordTools.GenerateVerifyCode(CODE_LENGTH);
            string codeSalt = PasswordTools.GenerateSalt();
            string codeHashed = PasswordTools.HashPassword(codeSalt, code);

            employee.password_reset_salt = codeSalt;
            employee.password_reset_hashed = codeHashed;
            userDao.UpdateEmployeeResetCode(employee);

            return code;
        }

        public void ResetPassword(Employee employee, string newPassword)
        {
            string hashedPassword = PasswordTools.HashPassword(employee.password_salt, newPassword);
            userDao.UpdateEmployeeResetPassword(employee, hashedPassword);
        }

        public void ClearResetCode(Employee employee)
        {
            userDao.UpdateEmployeeClearCode(employee);
        }
    }
}
