using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace WebApplication2.MiddleWare
{
    public class FirstMiddleware
    {
        private readonly RequestDelegate _next;
        // RequestDelegate ~ async (HttpContext context) => {} 
        public FirstMiddleware(RequestDelegate nex)
        {
            _next = nex;
        }

        // HttpCOntext đi qua MiddleWare trong pipeline 
        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine($"URL: {context.Request.Path}");
            context.Items.Add("DataFirstMiddleware", $"<p> URL: {context.Request.Path}</p>" );
            await _next(context);
        }
    }
}