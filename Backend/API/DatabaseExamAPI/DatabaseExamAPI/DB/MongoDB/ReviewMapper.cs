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

        /*
        public ReviewModel GetReviewByUserId(string userId)
        {
            IMongoCollection<ReviewModel> collection = _connector.GetMongoCollection();
            
            var filter = Builders<ReviewModel>.Filter.Eq(x => x.UserId, userId);
            ReviewModel review =  collection.Find(filter).FirstOrDefault();

             return review;
        }
        */

        public List<ReviewModel> GetReviewsByUserId(string userId)
        {
            IMongoCollection<ReviewModel> collection = _connector.GetMongoCollection();


            BsonDocument pipelineStage1 = new BsonDocument {
                {
                    "$match", new BsonDocument
                    {
                        {"userId", userId }
                    }
                }
            };


            BsonDocument[] pipline = new BsonDocument[] {pipelineStage1};

            List<ReviewModel> results = collection.Aggregate<ReviewModel>(pipline).ToList();

            return results;

        }

        public List<ReviewModel> GetReviewsByMovieId(string movieId)
        {
            IMongoCollection<ReviewModel> collection = _connector.GetMongoCollection();


            BsonDocument pipelineStage1 = new BsonDocument {
                {
                    "$match", new BsonDocument
                    {
                        {"movieId", movieId }
                    }
                }
            };

            BsonDocument[] pipline = new BsonDocument[] { pipelineStage1 };

            List<ReviewModel> results = collection.Aggregate<ReviewModel>(pipline).ToList();

            return results;

        }

        // TODO
        public BsonDocument GetAvgRatingByMovieId(string movieId)
        {
            IMongoCollection<ReviewModel> collection = _connector.GetMongoCollection();

             BsonDocument results = collection.Aggregate()
                       .Group(
                           x => x.MovieId == movieId,
                           g => new {
                               Result = g.Select(
                                          x => x.Rating
                                          ).Average()
                           }
                       ).ToBsonDocument();

            return results;

        }


        public IEnumerable<BsonDocument> GetTopReviews(string movieId)
        {
            IMongoCollection<ReviewModel> collection = _connector.GetMongoCollection();

            List<BsonDocument> aggregate = collection.Aggregate()
                                       .Group(new BsonDocument { { "movieId", movieId }, { "count", new BsonDocument("$sum", 1) } })
                                       .Sort(new BsonDocument { { "count", -1 } })
                                       .Limit(10).ToList();
            return aggregate;

        }



        public void AddReview(string movieId, string userId,string username, string desc, int rating)
        {
            IMongoCollection<ReviewModel> collection = _connector.GetMongoCollection();

            ReviewModel review = new ReviewModel
            {
                Id = ObjectId.GenerateNewId(),
                MovieId = movieId,
                UserId = userId,
                Username = username,
                Desc = desc,
                Rating = rating
            };

            collection.InsertOne(review);

        }    
    }
}
