namespace Cms.Services.Interfaces
{
    public interface IDataCachingService
    {
        bool IsKeyAvailable(string key);
        T PullDataFromCache<T>(string cacheKey);
        bool PushDataInCache<T>(T model, string cacheKey);
    }
}