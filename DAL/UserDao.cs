using Model.Enums;
using Model.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserDao : DAO
    {
        public UserDao() : base()
        {
        }
        public void CreateUser(string name, string email, string phoneNumber, string password, Role role)
        {
            string salt = GenerateSalt();

            string hashedPassword = HashPassword(salt, password);


            Employee newEmployee = new Employee
            {
                Id = ObjectId.GenerateNewId().ToString(),
                name = name,
                email = email,
                phone_number = phoneNumber,
                role = role,
                password_hashed = hashedPassword,
                password_salt = salt
            };

            Db.GetCollection<Employee>("employees").InsertOne(newEmployee);
        }


        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }
        private string HashPassword(string salt, string password)
        {
            string saltedPassword = salt + password;
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        //methode om alle users op te halen en een lijst van users terug te geven
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = Db.GetCollection<Employee>("employees")
                .Aggregate()
                .Match(Builders<Employee>.Filter.Empty)
                .ToList();

            return employees;
        }

        //methode om user informatie te updaten
        public void UpdateEmployee(PartialUser partialUser, Role role)
        {
            //update definition maken en de query uitvoeren
            //UpdateDefinition<Employee> updateDefinition = Builders<Employee>.Update
            var updateDefinition = Builders<Employee>.Update
                .Set(u => u.name, partialUser.name)
                .Set(u => u.email, partialUser.email)
                .Set(u => u.phone_number, partialUser.phone_number)
                .Set(e => e.role, role);

            Db.GetCollection<Employee>("employees")
                .UpdateOne(u => u.Id == partialUser.Id, updateDefinition);
        }

        //methode om user te deleten met ID (delete)
        public void DeleteEmployee(string id)
        {
            Db.GetCollection<Employee>("employees")
                .DeleteOne(Builders<Employee>.Filter.Eq(e => e.Id, id));
        }

        //public List<Employee> GetEmployeesWithTickets()
        //{
        //  var pipeline = Db.GetCollection<Employee>("employees")
        //    .Aggregate()
        //  .Lookup<Employee, Ticket, Employee>(
        //    "tickets",          // The collection you're joining with
        //  "Id",               // Local field in employees
        //"EmployeeId",       // Field in tickets collection
        //@as: "Tickets"      // The name of the new array field in the result
        // )
        //.ToList();

        //return pipeline;
        // }
    }
}
