using MongoDB.Bson;
using MongoDB.Driver;
using DatabaseExamAPI.Model;

namespace DatabaseExamAPI.DB.MongoDB
{

    public class MongoDBConnector
    {
        private static MongoDBConnector? intance;
        private readonly MongoClient _client;
        private readonly IMongoCollection<ReviewModel> _reviewCollection;

        private static readonly string _connectionString = "mongodb://localhost:27017/?readPreference=primary&appname=MongoDB%20Compass&directConnection=true&ssl=false";
        private static readonly string _databaseName = "admin";
        private static readonly string _collectionName = "reviews";

        private MongoDBConnector(string connectionString) 
        { 
            _client = new MongoClient(connectionString);
            var database = _client.GetDatabase(_databaseName);
            _reviewCollection = database.GetCollection<ReviewModel>(_collectionName);
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


       public IMongoCollection<ReviewModel> GetMongoCollection()
        {
            return _reviewCollection;
        }

    }
}
