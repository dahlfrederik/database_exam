using ServiceStack.Redis;
using System;

namespace DatabaseExamAPI.DB.Redis
{
    public class RedisConnector
    {
        private static RedisConnector? instance;
        private static readonly string _uri = "localhost:6379";
        private RedisClient redisClient;
      
        public RedisConnector()
        {
            redisClient = new RedisClient(_uri);

        }

        public RedisClient getClient()
        {
            return redisClient;
        }


    }
}

