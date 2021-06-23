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
		private static readonly HttpClient _httpClient = new HttpClient();

		public async Task<string> GetParks(string searchTerm)
		{
			HttpResponseMessage response = await _httpClient.GetAsync("https://seriouslyfundata.azurewebsites.net/api/parks");
			response.EnsureSuccessStatusCode();

			string responseContent = await response.Content.ReadAsStringAsync();
			var parksResponse = JArray.Parse(responseContent);
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
			var thing1 = parkList;
			return searchTerm;
		}
	}
}
