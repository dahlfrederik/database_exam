﻿using DatabaseExamAPI.Model;
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

        public List<ReviewModel> GetReviewsByUserId(string userId)
        {
            IMongoCollection<ReviewModel> collection = _connector.GetMongoCollection();

            var filter = Builders<ReviewModel>.Filter.Eq(review => review.UserId, userId);
            List<ReviewModel> reviews = collection.Find(filter).ToList();

            return reviews;
        }

        public List<ReviewModel> GetReviewsByMovieId(string movieId)
        {
            IMongoCollection<ReviewModel> collection = _connector.GetMongoCollection();

            var filter = Builders<ReviewModel>.Filter.Eq(review => review.MovieId, movieId);
            List<ReviewModel> reviews = collection.Find(filter).ToList();

            return reviews;
        }

        public double GetAvgRatingByMovieId(string movieId)
        {
            IMongoCollection<ReviewModel> collection = _connector.GetMongoCollection();

            var result = collection.Aggregate()
                .Match(b => b.MovieId == movieId)
                .Group(b => b.MovieId == movieId, g =>
                new
                {
                    AverageRating = g.Average(p => p.Rating),
                })
            .ToList();

            double average = result.Select(_ => _.AverageRating).FirstOrDefault();

            return average;
        }


        /*
         * TODO
        public List<ReviewModel> GetTopReviews(string movieId)
        {
            IMongoCollection<ReviewModel> collection = _connector.GetMongoCollection();

           // List<ReviewModel> reviews = collection.Find().Sort({ '_id' : -1}.limit(N)


            return aggregate;
        }
        */


        public void AddReview(string movieId, string userId, string username, string desc, int rating)
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
