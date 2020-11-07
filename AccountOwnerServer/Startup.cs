using System;
using System.IO;
using AccountOwnerServer.Data;
using AccountOwnerServer.Extensions;
using Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

namespace AccountOwnerServer
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			LogManager.LoadConfiguration(Path.Combine(Directory.GetCurrentDirectory(), "nlog.config"));

			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContextPool<ApplicationDbContext>(options => options				
				.UseMySql(Configuration.GetConnectionString("MyDefaultConnection"), mySqlOptions => mySqlOptions					
					.ServerVersion(new Version(5, 7, 9), ServerType.MySql)
					.EnableRetryOnFailure()

			));

			services.ConfigureLoggerService();
			services.ConfigureCors();
			
			services.AddControllers();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerManager logger)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			//app.ConfigureExceptionHandler(logger);
			app.ConfigureCustomExceptionMiddleware();

			app.UseHttpsRedirection();

			app.UseStaticFiles();
			app.UseCors("CorsPolicy");
			app.UseForwardedHeaders(new ForwardedHeadersOptions
			{
				ForwardedHeaders = ForwardedHeaders.All
			});

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
