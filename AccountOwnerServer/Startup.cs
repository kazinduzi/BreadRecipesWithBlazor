using System;
using System.IO;
using AccountOwnerServer.Data;
using AccountOwnerServer.Extensions;
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
					.ServerVersion(new Version(8, 0, 18), ServerType.MySql)
			));
			
			services.Configure<IISServerOptions>(options => 
			{
			    options.AutomaticAuthentication = false;
			});
			
			services.ConfigureLoggerService();
			services.ConfigureCors();
			
			services.AddControllers();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ApplicationDbContext dbContext)
		{
			// migrate any database changes on startup (includes initial db creation)
			dbContext.Database.Migrate();

			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

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
