using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Cwiczenie3.Serivices;
using Cwiczenie3.DAL;
using Cwiczenie3.Middlewares;
using Cwiczenie3.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            app.Use(async (context, next) =>
            {

                if (!context.Request.Headers.ContainsKey("Index"))
                {
                    context.Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("Nie podales loginu i hasla");
                    return;
                }

                string index = context.Request.Headers["Index"].ToString();
                IStudentsDbService dbs = new ServerDbService();

                if (!dbs.StudentExist(index))
                {
                    context.Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized;
                    await context.Response.WriteAsync("blendny login lub chaslo");
                    return;
                }



                await next();

            }

                 );

            app.UseMiddleware<LogginMiddleware>();

            //app.UseHttpsRedirection;

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }


    }
}
