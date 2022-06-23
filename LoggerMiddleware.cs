using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace middlewareStuff
{
    public class LoggerMiddleware : IMiddleware
    {
        //To use this register this to dotnet core service colletion
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            context.Response.WriteAsync("Hello world \n");

            await next(context);
            
            context.Response.WriteAsync("After Request");

            // next is the next middleware
            // so app.use is used to chain the middlewares


        }
    }
}
