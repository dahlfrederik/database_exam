using DatabaseExamAPI.Model;
using MongoDB.Driver;
using MongoDB.Bson;

namespace DatabaseExamAPI.DB.MongoDB
{
    public class ReviewMapper
    {
        // TODO
        private ILogger<ReviewMapper> _logger;
        private MongoDBConnector _connector;


        public ReviewMapper(ILoggerFactory lf)
        {
            _logger = lf.CreateLogger<ReviewMapper>();
            _connector = MongoDBConnector.Instance;
        }
        /*

        public ReviewModel TestGet()
        {
            return new ReviewModel("1", "2", "test", "tester", 5);
        }

        
        public async Task<ReviewModel> AddReview()
        { using (var db = _connector.GetMongoClient().GetDatabase("admin").GetCollection<BsonDocument>("reviews")
                {
                BsonDocument document = new BsonDocument { { "test", 1000 } };
                await db.InsertOneAsync(document);
            }
         }
        */
                   
    }
}
