using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Parks.Models
{
	public class CacheService
	{
		private static readonly HttpClient _httpClient = new();
		private readonly IMemoryCache _cache;

		public CacheService(IMemoryCache memoryCache)
		{
			_cache = memoryCache;
		}

		public async Task<List<Park>> GetSetParksCache()
		{
			if (!_cache.TryGetValue("parkList", out List<Park> parkList))
			{
				parkList = await BuildParksList();

				var cacheEntryOptions = new MemoryCacheEntryOptions()
				{
					AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(5)
				};

				_cache.Set("parkList", parkList, cacheEntryOptions);
			}
			return parkList;
		}

		public async Task<List<Park>> BuildParksList()
		{
			HttpResponseMessage response = await _httpClient.GetAsync("https://seriouslyfundata.azurewebsites.net/api/parks");
			response.EnsureSuccessStatusCode();
			return JsonConvert.DeserializeObject<List<Park>>(await response.Content.ReadAsStringAsync());
		}
	}
}
