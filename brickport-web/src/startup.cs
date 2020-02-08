using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BrickPort.Web.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BrickPort.Web
{
    public class Startup
    {
        private const string _devCorsPolicy = "_devCorsPolicy";
        public void ConfigureServices(IServiceCollection services) => services
            .AddCors(options => options.AddPolicy(_devCorsPolicy, builder => builder.WithOrigins(
                "http://localhost:8080",
                "https://localhost:8081"
            )))
            .AddBrickPortWeb(options => 
            {
                //options.UseDomainCommands = true;
                options.UseInMemoryCommands = true;
                options.UseInMemoryQueries = true;
            })
            .AddSwaggerDocument(configure => 
            {
                configure.Version = "v1";
                configure.Title = "brickport";
                configure.Description = "Web API for brickport catan tracking companion";
            })
            .AddControllers(); // Register the Swagger services

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) 
            {
                app.UseDeveloperExceptionPage();
                app.UseCors(_devCorsPolicy);
            }
            app.UseRouting();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
