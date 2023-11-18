using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Add Redoc
builder.Services.AddSwaggerDocument(config =>
{
    config.PostProcess = document =>
    {
        document.Info.Title = "Todo API";
        document.Info.Description = "A simple CRUD API for managing tasks.";
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
    // Middleware for Redoc
    app.UseOpenApi();
    app.UseSwagger();
    app.UseReDoc(options =>
    {
        options.DocumentTitle = "Swagger Demo Documentation";
        options.SpecUrl = "/swagger/v1/swagger.json";
    });
    
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
