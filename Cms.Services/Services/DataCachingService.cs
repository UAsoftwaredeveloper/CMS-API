using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System;
using System.Text.Json;
using Cms.Services.Interfaces;

namespace CMS.Services.Services
{
    public class DataCachingService : IDataCachingService
    {
        public IMemoryCache _memoryCache;
        public DataCachingService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        public bool PushDataInCache<T>(T model, string cacheKey)
        {
            var serializedData = JsonSerializer.Serialize(model,options:new JsonSerializerOptions
            {
                IncludeFields = true,
                MaxDepth=int.MaxValue,
                PropertyNameCaseInsensitive = true,
                WriteIndented = true,
                ReferenceHandler=System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
            });
            if (!_memoryCache.TryGetValue(cacheKey, out T compressData))
            {
                //calling the server

                //setting up cache options
                var cacheExpiryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(20),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromHours(16),
                    
                };
                //setting cache entries
                _memoryCache.Set(cacheKey, serializedData, cacheExpiryOptions);
            }
            var cached = _memoryCache.Get(cacheKey);
            if (cached != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public T PullDataFromCache<T>(string cacheKey)
        {
            if (_memoryCache.TryGetValue(cacheKey, out string compressData))
            {
                return JsonSerializer.Deserialize<T>(compressData, options: new JsonSerializerOptions
                {
                    IncludeFields = true,
                    MaxDepth = int.MaxValue,
                    PropertyNameCaseInsensitive = true,
                    WriteIndented = true,
                    ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles
                });
            }
            else
            {
                throw new KeyNotFoundException(cacheKey + "not found");
            }
        }
        public bool IsKeyAvailable(string key) => _memoryCache.TryGetValue(key, out string _);
    }
}
