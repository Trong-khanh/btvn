using Microsoft.AspNetCore.Builder;

namespace WebApplication2.MiddleWare
{
    public static class UseMiddleWareMethod
    {
        public static  void UseFirstMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<FirstMiddleware>();
        }
        
        public static  void UseSecondMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<SecondMiddleware>();
        }
    }
}