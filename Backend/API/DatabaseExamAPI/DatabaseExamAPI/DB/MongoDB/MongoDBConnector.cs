using MongoDB.Bson;
using MongoDB.Driver;

namespace DatabaseExamAPI.DB.MongoDB
{

    public class MongoDBConnector
    {
        private static MongoDBConnector? intance;
        private readonly MongoClient _client;

        private static readonly string _connectionString = "TODO";
        private static readonly string _databaseName = "admin";
        private static readonly string _collectionName = "reviews";

        private MongoDBConnector(string connectionString) 
        { 
            _client = new MongoClient(connectionString); 
        }

        public static MongoDBConnector Instance 
        { get
            {
                if (intance == null)
                {
                    intance = new MongoDBConnector(_connectionString); 
                }
                return intance; 
            } 
        } 


       public MongoClient GetMongoClient()
        {
            return _client;
        }

    }
}
