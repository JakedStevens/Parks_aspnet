using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Parks.Models
{
	public class CacheService
	{
		private readonly IMemoryCache _cache;

		public CacheService(IMemoryCache memoryCache)
		{
			_cache = memoryCache;
		}

		public bool CacheExists()
		{
			return _cache.TryGetValue("parkList", out List<Park> _);
		}

		public List<Park> GetCachedData()
		{
			return _cache.Get<List<Park>>("parkList");
		}

		public void SetCachedData(List<Park> parksList)
		{
			var cacheEntryOptions = new MemoryCacheEntryOptions()
			{
				AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
			};
			_cache.Set("parkList", parksList, cacheEntryOptions);
		}
	}
}
