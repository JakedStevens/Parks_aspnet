using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Parks.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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

		public IActionResult ParkData(string search)
		{
			ViewBag.search = search;
			_helperFunctions.GetParks(search);
			return View("ParkData");
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
