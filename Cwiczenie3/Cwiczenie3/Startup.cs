using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Cwiczenie3.Serivices;
using Cwiczenie3.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cwiczenie3
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //Lifetime
            //Ninject
            //Autofac
            //...
            services.AddTransient<IDbService, MockDbService>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
             app.UseDeveloperExceptionPage();

             app.UseRouting();

            //Doklejal do odpowiedzi naglowek http
            app.Use(async (context, c) =>
            {
                context.Response.Headers.Add("Secret", "1234");
                await c.Invoke();
            });
            app.UseMiddleware<CustomMiddleware>();

            //app.UseHttpsRedirection;

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
