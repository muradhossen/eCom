using Application;
using Application.Common.Mapper;
using Application.DTOs.Tests;
using Application.ServiceInterface;
using AutoMapper;
using Domain.Entities;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
       .AddApplication()
       .AddPersistence(builder.Configuration);

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/tests", async (ITestService _testService,IMapper _mapper ,[FromBody] TestCreateDto testCreateDto) =>
{
    if (testCreateDto is null)
    {
        return Results.BadRequest("No data found to add");
    }
    var test = _mapper.Map<Test>(testCreateDto);

    bool isSaved = await _testService.AddAsync(test);

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

 