using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Parks.Models;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Parks.Controllers
{
	public class ParksController : Controller
	{
		private readonly ILogger<ParksController> _logger;
		private readonly HelperFunctions _helperFunctions;

		public ParksController(ILogger<ParksController> logger, HelperFunctions helperFunctions)
		{
			_logger = logger;
			_helperFunctions = helperFunctions;
		}

		public async Task<IActionResult> ParkData(string search)
		{
			ParkViewModel parks = await _helperFunctions.GetParks(search);
			//return Json();
			return View("ParkData", parks);
		}

		public async Task<IActionResult> JSParkData(string search)
		{
			ParkViewModel parks = await _helperFunctions.GetParks(search);
			return Json(parks);
		}

		public async Task<IActionResult> JSParkDataView(string search)
		{
			return View("JavascriptParks");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
