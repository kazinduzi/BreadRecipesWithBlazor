using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorAppWASM.Server.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class UploadController: ControllerBase
	{
		private readonly IWebHostEnvironment _env;

		public UploadController(IWebHostEnvironment env)
		{
			_env = env;
		}

		public async Task Post()
		{
			if (HttpContext.Request.Form.Files.Any())
			{
				var files = HttpContext.Request.Form.Files.ToList();
				foreach(var file in files)
				{
					var path = Path.Combine(_env.ContentRootPath, "Upload", file.FileName);

					if (!Directory.Exists(path))
					{
						Directory.CreateDirectory(Path.GetDirectoryName(path));
					}						

					using var stream = new FileStream(path, FileMode.Create);
					await file.CopyToAsync(stream);
				}
			}
		}
	}
}
