using ServiceStack.Redis;

namespace DatabaseExamAPI.DB.Redis
{
    public class RedisConnector
    {
        //private static RedisConnector? instance;
        private static readonly string _uri = "localhost:6379";

        //private RedisConnector()
        //{

        //}

        //public static RedisConnector Instance
        //{
        //    get
        //    {
        //        if (instance == null)
        //        {
        //            instance = new RedisConnector();
        //        }
        //        return Instance;
        //    }
        //}


        public static void SaveData(string key, string value)
        {
            using (RedisClient client = new RedisClient(_uri))
            {
                if (client.Get<string>(key) == null)
                {
                    client.Set(key, value);
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

