using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Parks.Models
{
	public class HelperFunctions
	{
		private readonly CacheService _cacheService;

		public HelperFunctions(CacheService cacheService)
		{
			_cacheService = cacheService;
		}

		public async Task<ParkViewModel> GetParksData(string searchTerm)
		{
			List<Park> parkList = await _cacheService.GetSetParksCache();
			return CreateParkViewModel(parkList, searchTerm);
		}

		public ParkViewModel CreateParkViewModel(List<Park> parkList, string searchTerm)
		{
			if (!string.IsNullOrEmpty(searchTerm) && searchTerm.Length > 0)
			{
				List<Park> parksFiltered = parkList.Where(park => park.ParkName.ToLower().Contains(searchTerm.ToLower())).ToList();
				ParkViewModel parkVM = new ParkViewModel();
				parkVM.AllParks = parksFiltered;
				return parkVM;
			}
			else
			{
				ParkViewModel parkVM = new ParkViewModel();
				parkVM.AllParks = parkList;
				return parkVM;
			}
		}
	}
}
