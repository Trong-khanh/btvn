using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebApplication2.MiddleWare;

namespace WebApplication2
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<SecondMiddleware>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseStaticFiles(); // StaticFileMiddleware
            app.UseFirstMiddleware(); // đưa vào pineline FirstMiddleWare
            //app.UseMiddleware<SecondMiddleware>();
            app.UseSecondMiddleware(); // đưa vào pineline FirstMiddleWare

            app.UseRouting(); //EndpointRoutingMiddleware
            // Tạo EndPoint (terminate middleware)
            app.UseEndpoints((endpoint) =>
            {
                // E1 
                endpoint.MapGet("/about.html", async (context) =>
                {
                    await context.Response.WriteAsync("Trang gioi thieu ");
                });
                // E2
                endpoint.MapGet("/sanpham.html", async (context) =>
                {
                    await context.Response.WriteAsync("Trang san pham  ");
                });
            });
            // rẽ nhánh pipeline
            app.Map("/admin", (app1) =>
            {
                // Tạo ra Middleware của nhánh 
                app1.UseRouting();
                //BE1
                app1.UseEndpoints((endpoint) =>
                {
                    endpoint.MapGet("/user", async (context) =>
                    {
                        await context.Response.WriteAsync("Trang quan ly user ");
                    });
                });
                //BE2
                app1.UseEndpoints((endpoint) =>
                {
                    endpoint.MapGet("/sanpham", async (context) =>
                    {
                        await context.Response.WriteAsync("Trang quan ly san pham  ");
                    });
                });
                app1.Run(async (context) =>
                {
                    await context.Response.WriteAsync(("Trang Admin"));
                });
            });
            // Terminate Middleware M1
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(("xin chao ASP.NET Core"));
            });
        }
    }
}