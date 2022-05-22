using ServiceStack.Redis;
using System;

namespace DatabaseExamAPI.DB.Redis
{
    public class RedisConnector
    {
  
        private static readonly string _uri = "localhost:6379";
        private static readonly int _expiration = 1800;

       


        public static void SaveData<T>(string key, T value)
        {
            using (RedisClient client = new RedisClient(_uri))
            {
                if (client.Get<string>(key) == null)
                {
                    client.Set(key, value);
                    client.Expire(key, _expiration);
                }
            }
        }

        public static string ReadData(string key)
        {
            using (RedisClient client = new RedisClient(_uri))
            {
                return client.Get<string>(key);
            }
        }
    }
}

