using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DatabaseExamAPI.Model
{
    [BsonIgnoreExtraElements]
    public class ReviewModel
    {
        [BsonId]      
        public ObjectId Id { get; set; }

        [BsonElement("movieId")]
        public string MovieId { get; set; }

        [BsonElement("userId")]
        public string UserId { get; set; }
        
        [BsonElement("username")]
        public string Username { get; set; }

        [BsonElement("desc")]
        public string Desc { get; set; }

        [BsonElement("rating")]
        public int Rating { get; set; }
        
    }
}
