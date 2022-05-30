using ServiceStack;
using ServiceStack.Redis;
using System;

namespace DatabaseExamAPI.DB.Redis
{
    public class CacheMapper
    {
  
        private RedisConnector _connector;

       public CacheMapper()
        {
            _connector = new RedisConnector();
        }

        public void SaveData<T>(string key, T value, int expiration)
        {
          
            using (RedisClient client = new RedisConnector().getClient())
            {
                if (client.Get<string>(key) == null)
                {
                    //var trans = client.CreateTransaction();
                    //trans.QueueCommand(r => client.Set(key, JSON.stringify(value)));
                    //trans.QueueCommand(r => client.Expire(key, _expiration));
                    //trans.Commit();
                    client.Set(key, JSON.stringify(value));
                    client.Expire(key, expiration);


                }
            }
        }

        public async Task<object> ReadData(string key)
        {
            await using (RedisClient client = new RedisConnector().getClient())
            {
                return JSON.parse(client.Get<string>(key));
            }
        }
    }
}

