using DatabaseExamAPI.Model;
using MongoDB.Driver;
using MongoDB.Bson;
using DatabaseExamAPI.DB;

namespace DatabaseExamAPI.DB.MongoDB
{
    public class ReviewMapper
    {
        private ILogger<ReviewMapper> _logger;
        private MongoDBConnector _connector;


        public ReviewMapper(ILoggerFactory lf)
        {
            _logger = lf.CreateLogger<ReviewMapper>();
            _connector = MongoDBConnector.Instance;
        }

        public ReviewModel GetReview(string userId)
        {
            IMongoCollection<ReviewModel> collection = _connector.GetMongoCollection();

            var filter = Builders<ReviewModel>.Filter.Eq(x => x.UserId, userId);
            ReviewModel review =  collection.Find(filter).FirstOrDefault();

            return review;

        }

        public void AddReview(string userId,string username, string desc, int rating)
        {
            IMongoCollection<ReviewModel> collection = _connector.GetMongoCollection();

            ReviewModel review = new ReviewModel
            {
                Id = ObjectId.GenerateNewId(),
                UserId = userId,
                Username = username,
                Desc = desc,
                Rating = rating
            };

            collection.InsertOne(review);

        }    
    }
}
