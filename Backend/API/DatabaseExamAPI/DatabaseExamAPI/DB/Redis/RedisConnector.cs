using ServiceStack.Redis;
using System;

namespace DatabaseExamAPI.DB.Redis
{
    public class RedisConnector
    {
        private static RedisConnector? instance;
        private static readonly string _uri = "localhost:6379";
        private readonly RedisClient? _redisClient;
      
        public RedisConnector(RedisClient? redisClient)
        {
            _redisClient = redisClient;
        }

        public RedisClient getClient()
        {
            return _redisClient;
        }

        public static RedisConnector Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new RedisConnector(new RedisClient(_uri));
                }
                return instance;
            }
        }


    }
}

