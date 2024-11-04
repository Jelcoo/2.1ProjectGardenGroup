using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using Model;
using MongoDB.Bson.Serialization;
using System.Configuration;

namespace DAL
{
    public class DAO
    {
        protected MongoClient client;
        private string connectionString = "mongodb+srv://716588:Marijke%4002@cluster0.rnouj.mongodb.net/";
        private string dbName = "TuinDB";
        private IMongoDatabase db;
        protected IMongoDatabase Db { get { return db; } }

        public DAO()
        {
            client = new MongoClient(connectionString);
            db = client.GetDatabase(dbName);
        }
    }
}