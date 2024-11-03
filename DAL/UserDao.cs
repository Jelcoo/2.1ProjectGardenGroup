﻿using Model.Enums;
using Model.Models;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Tools;

namespace DAL
{
    public class UserDao : DAO
    {
        public UserDao() : base()
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

        public void CreateUser(string name, string email, string phoneNumber, string password, Role role)
        {
            string salt = PasswordTools.GenerateSalt();
            string hashedPassword = PasswordTools.hashPassword(salt, password);

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

        //methode om alle users op te halen en een lijst van users terug te geven
        public List<Employee> GetAllEmployees()
        {
            List<Employee> employees = Db.GetCollection<Employee>("employees")
                .Aggregate()
                .Project(employee => new Employee
                {
                    Id = employee.Id,
                    name = employee.name,
                    email = employee.email,
                    phone_number = employee.phone_number,
                    role = employee.role,
                    password_hashed = employee.password_hashed,
                    password_salt = employee.password_salt
                })
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
        //    "tickets",         
        //  "Id",               
        //"EmployeeId",       
        //@as: "Tickets"      
        // )
        //.ToList();

        //return pipeline;
        // }
    }
}
