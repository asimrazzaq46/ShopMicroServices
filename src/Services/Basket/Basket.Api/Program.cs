using Carter;

var builder = WebApplication.CreateBuilder(args);

// Add Services to the container

builder.Services.AddCarter();

var app = builder.Build();

// Configure the HTTP request Pipeline

app.Run();
