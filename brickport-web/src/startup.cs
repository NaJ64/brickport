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
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) => services
            .AddBrickPortWeb(options => 
            {
                options.UseDomainCommands = true;
                options.UseInMemoryQueries = true;
            })
            .AddSwaggerDocument(configure => 
            {
                configure.Version = "v1";
                configure.Title = "brickport";
                configure.Description = "Web API for brickport catan tracking companion";
            })
            .AddControllers(); // Register the Swagger services

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            app.UseRouting();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
