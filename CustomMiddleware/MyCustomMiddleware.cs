
namespace CoreMiddlewareDemo.CustomMiddleware
{
    public class MyCustomMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            await context.Response.WriteAsync("Hello Middleware no1 \n");
            await next(context);
        }
    }

    public static class CustomMiddleWareExtension
    {
        public static void MyMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<MyCustomMiddleware>();
        }
    }
}
