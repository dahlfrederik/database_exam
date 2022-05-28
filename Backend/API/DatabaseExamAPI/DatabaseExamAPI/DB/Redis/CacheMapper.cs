using ServiceStack;
using ServiceStack.Redis;
using System;

namespace DatabaseExamAPI.DB.Redis
{
    public class CacheMapper
    {
  
        private static readonly string _uri = "localhost:6379";
        private static readonly int _expiration = 1800;
        private RedisConnector _connector;

       public CacheMapper()
        {
            _connector = RedisConnector.Instance;
        }

        public void SaveData<T>(string key, T value)
        {
          
            using (RedisClient client = _connector.getClient())
            {
                if (client.Get<string>(key) == null)
                {
                    var trans = client.CreateTransaction();
                    trans.QueueCommand(r => client.Set(key, JSON.stringify(value)));
                    trans.QueueCommand(r => client.Expire(key, _expiration));
                    trans.Commit();
                    
                }
            }
        }

        public async Task<object> ReadData(string key)
        {
            using (RedisClient client = _connector.getClient())
            {
                return JSON.parse(client.Get<string>(key));
            }
        }
    }
}

