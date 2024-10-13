using Model;
using Model.models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;


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
        public List<Employee> GetAllUsers()
        {
            List<Employee> users = Db.GetCollection<Employee>("employees")
                .Aggregate()
                .ToList();

            return users;
        }

        //methode om user informatie te updaten
        public void UpdateUser(string id, string name, string email, string phoneNumber)
        {
            //update definition maken en de query uitvoeren
            UpdateDefinition<Employee> updateDefinition = Builders<Employee>.Update
                .Set(u => u.name, name)
                .Set(u => u.email, email)
                .Set(u => u.phone_number, phoneNumber);

            Db.GetCollection<Employee>("employees")
                .UpdateOne(u => u.Id == id, updateDefinition);
        }

        //methode om user te deleten met ID (delete)
        public void DeleteUser(string id)
        {
            Db.GetCollection<Employee>("employees")
                .DeleteOne(u => u.Id == id);
        }
    }
}
