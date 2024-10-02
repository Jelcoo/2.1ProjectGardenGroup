using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using Model;
using MongoDB.Bson.Serialization;
using System.Xml.Linq;

namespace DAL
{
	public class DAO
	{
		protected MongoClient client;
		private string connectionString = "mongodb+srv://716588:Marijke%4002@cluster0.rnouj.mongodb.net/";
		private string dbName = "projectTuin";
		private IMongoDatabase db;
		protected IMongoDatabase Db { get { return db; } }

		public DAO()
		{
			client = new MongoClient(connectionString);
			db = client.GetDatabase(dbName);
		}

		public List<Databases_Model> GetDatabases()
		{
			List<Databases_Model> all_databases = new List<Databases_Model>();

			foreach (BsonDocument db in client.ListDatabases().ToList())
			{
				all_databases.Add(BsonSerializer.Deserialize<Databases_Model>(db));
			}
			return all_databases;
		}
	}
}