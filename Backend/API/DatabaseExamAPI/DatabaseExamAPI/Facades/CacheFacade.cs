using DatabaseExamAPI.DB.Redis;

namespace DatabaseExamAPI.Facades
{
    public class CacheFacade
    {
        
        private CacheMapper _cacheMapper;

        public CacheFacade()
        {
            _cacheMapper = new CacheMapper();

        }
      

        public void saveData<T>(string key, T value)
        {
            _cacheMapper.SaveData(key, value);
        }


        public async Task<string> ReadData(string key)
        {
            return await _cacheMapper.ReadData(key);
        }
    }

}
