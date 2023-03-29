using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApplication2.MiddleWare
{
    public class SecondMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            if (context.Request.Path == "/XXX.html")
            {
                context.Response.Headers.Add("seconmiddleware", "ban k dc truy cap");
                var DatafromFirstMiddleware = context.Items["DataFirstMiddleware"];
                if (DatafromFirstMiddleware != null)
                    await context.Response.WriteAsync((string)DatafromFirstMiddleware);
                await context.Response.WriteAsync("ban k dc truy cap");
            }
            else
            {
                context.Response.Headers.Add("seconmiddleware", "ban  dc truy cap");
                var DatafromFirstMiddleware = context.Items["DataFirstMiddleware"];
                if (DatafromFirstMiddleware != null)
                    await context.Response.WriteAsync((string)DatafromFirstMiddleware);
                await next(context);
            }
        }
    }
}