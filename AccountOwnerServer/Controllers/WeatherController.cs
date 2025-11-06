using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace AccountOwnerServer.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class WeatherController : ControllerBase
	{
		public IEnumerable<WeatherData> Get()
		{
			return new List<WeatherData>
			{
				new WeatherData{
					Date = DateTime.Parse("2008-11-10"),
					TemperatureC = 12,
					Summary = "Chilly"
				},
				new WeatherData{
					Date = DateTime.Parse("2008-07-10"),
					TemperatureC = 52,
					Summary = "Hot"
				},
				new WeatherData{
					Date = DateTime.Parse("2008-12-10"),
					TemperatureC = 14,
					Summary = "Cold"
				},
				new WeatherData{
					Date = DateTime.Parse("2008-09-10"),
					TemperatureC = 42,
					Summary = "Softy"
				}
			};
		}
	}

	public class WeatherData
	{
		public DateTime Date { get; set; }
		public int TemperatureC { get; set; }
		public string Summary { get; set; }
	}
}
