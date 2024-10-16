using DAL;
using Model;
using Model.models;
using System.Collections.Generic;


namespace Logic
{
    public class UserLogic
    {
        UserDao userDao = new UserDao();

        public UserLogic() { }

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
        public void UpdateUser(string id, string name, string email, string phoneNumber, Role role)
        {
            userDao.UpdateEmployee(id, name, email, phoneNumber, role);
        }
        // verwijder user door ID
        public void DeleteUser(string id)
        {
            userDao.DeleteEmployee(id);
        }
    }
}
