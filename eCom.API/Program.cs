using Application;
using Application.DTOs.Tests;
using Application.ServiceInterface;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
       .AddApplication()
       .AddPersistence(builder.Configuration);

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.MapPost("/tests", async (ITestService _testService, [FromBody] TestCreateDto testCreateDto) =>
{

    bool isSaved = await _testService.AddAsync(new Domain.Entities.Test
    {
        Name = testCreateDto.Name
    });

    if (isSaved)
    {
        return Results.Ok(isSaved);
    }
    return Results.BadRequest();
});

    app.MapGet("/tests", async (ITestService _testService) =>
    {

       var tests = await _testService.GetAllAsync();

        if (tests == null || !tests.Any())
        {
            return Results.NotFound("No tests found!");
        }
        return Results.Ok(tests);
    })
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

 