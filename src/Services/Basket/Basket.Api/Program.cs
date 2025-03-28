using Basket.Api.Data;
using Basket.Api.Models;
using BuildingBlocks.Behaviours;
using Carter;
using Marten;

var builder = WebApplication.CreateBuilder(args);

var assembly = typeof(Program).Assembly;

// Add Services to the container
builder.Services.AddScoped<IBasketRepositery, BasketRepositery>();

builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});

builder.Services.AddMarten(config =>
{
    config.Connection(connectionString: builder.Configuration.GetConnectionString("Database")!);
    config.Schema.For<ShoppingCart>().Identity(x=> x.UserName);

}).UseLightweightSessions();

var app = builder.Build();

// Configure the HTTP request Pipeline

app.MapCarter();

app.Run();
