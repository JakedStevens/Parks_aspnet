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
		private static readonly HttpClient _httpClient = new();
		private readonly CacheService _cacheService;

		public HelperFunctions(CacheService cacheService)
		{
			_cacheService = cacheService;
		}

		public async Task<ParkViewModel> GetParksData(string searchTerm)
		{
			bool cacheExists = _cacheService.CacheExists();

			if (cacheExists)
			{
				List<Park> parkList = _cacheService.GetCachedData();
				return CreateParkViewModel(parkList, searchTerm);
			}
			else
			{
				List<Park> parkList = await BuildParksList();
				_cacheService.SetCachedData(parkList);
				return CreateParkViewModel(parkList, searchTerm);
			}
		}

		public async Task<List<Park>> BuildParksList()
		{
			HttpResponseMessage response = await _httpClient.GetAsync("https://seriouslyfundata.azurewebsites.net/api/parks");
			response.EnsureSuccessStatusCode();
			string responseContent = await response.Content.ReadAsStringAsync();

			JArray parksResponse = JArray.Parse(responseContent);
			List<Park> parkList = new List<Park>();

			foreach (var park in parksResponse)
			{
				Park newParkRecord = new Park();
				newParkRecord.ParkId = park["ParkID"].ToString();
				newParkRecord.ParkName = park["Parkname"].ToString();
				newParkRecord.SanctuaryName = park["SanctuaryName"].ToString();
				newParkRecord.Borough = park["Borough"].ToString();
				newParkRecord.Acres = Double.Parse(park["Acres"].ToString());
				newParkRecord.Directions = park["Directions"].ToString();
				newParkRecord.Description = park["Description"].ToString();
				newParkRecord.HabitatType = park["HabitatType"].ToString();

				parkList.Add(newParkRecord);
			}
			return parkList;
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
