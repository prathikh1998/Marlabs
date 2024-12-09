using CoreMiddlewareDemo.CustomMiddleware;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddTransient<MyCustomMiddleware>();
var app = builder.Build();

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("HELLO WORLD 1st\n");
    await next(context);
});

app.MyMiddleware();

app.UseWhen(
    context => context.Request.Query.ContainsKey("username"),
    app =>
    {
        app.Use(
            async (context, next) =>
            {

                await context.Response.WriteAsync("the key exists\n");
                await next(context);
            }
            );
    }
    );
app.UseSecondMiddleware();

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    await context.Response.WriteAsync("HELLO WORLD 2nd\n");
    await next(context);
});

app.Run(async (HttpContext context) =>
{
    await context.Response.WriteAsync("HELLO WORLD 3rd\n");
});


app.Run();
