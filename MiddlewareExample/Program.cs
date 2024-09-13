using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.Use(async (context, next) =>
{
    Console.WriteLine("Logika prije izvrsavanja next delegata u Use metodi!");
    await next.Invoke();
    Console.WriteLine("Logika posle izvrsavanja next delegata u Use metodi!");
});


app.Map("/usingmapbranch", async builder =>
{
    builder.Use(async (context, next) =>
    {
        Console.WriteLine("Map branch logika u Use metodi prije next delegata!");
        await next.Invoke();
        Console.WriteLine("Map branch logic in the Use method after the next delegate");
    });

    builder.Run(async context =>
    {
        Console.WriteLine("Map branch response klijentu u Run metodi!");
        await context.Response.WriteAsync("Pozdrav iz Map grane!");
    });
});


app.MapWhen(context => context.Request.Query.ContainsKey("testQueryString"), builder =>
{
    builder.Run(async context =>
    {
        await context.Response.WriteAsync("Pozdrav iz MapWhen grane!");
    });
});

app.Run(async context =>
{
    Console.WriteLine("Writing the response to the client in the Run method!");
    await context.Response.WriteAsync("Pozdrav iz middleware komponente!");
});

app.MapControllers();

app.Run();
