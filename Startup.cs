using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace middlewareStuff
{
    public class Startup
    {
        //Register the Custom middlewares
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<LoggerMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        // Middleware are used handle request and responses
        // Middlewares are configured inside the Configure method
        // When there are no middlewares it will return a 404

        

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // simple method method handle requests and return hello world
            // This will handle any http context and return helloworld

            // So now this is called a Request delegate or .net Middleware

            //app.Run(async context => await context.Response.WriteAsync("Hello world")); //This is an inline middleware

            //On app builder class we can use 3 methods to configure request delegates

            // Run, Use, and Map

            // Run - Terminal delegate - used to end the pipeline
            // Use - Chain the middlewares
            // Map - Branch the pipeline

            //app.Run(async context => await context.Response.WriteAsync("hello again")); //This won't run

            //app.Use(async (context, next) =>
            //{
            //    context.Response.WriteAsync("Hello world \n");
            //    await next();
            //    context.Response.WriteAsync("After Request");
            //});

            app.UseMiddleware<LoggerMiddleware>(); 

            app.Map("/map", MapHandler);

            app.MapWhen(context => context.Request.Query.ContainsKey("q"), HandleRequestWithQuery);

            app.Run(async context => await context.Response.WriteAsync("hello again \n"));


        }

        private void HandleRequestWithQuery(IApplicationBuilder obj)
        {
            obj.Use(async (context, next) =>
            {
                context.Response.WriteAsync("Helllo from query \n");
            });
        }

        private void MapHandler(IApplicationBuilder app)
        {
            app.Run(async context =>
            {
                await context.Response.WriteAsync("Helllo from map \n");
            });
        }
    }
}
