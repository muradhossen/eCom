using Application;
using Application.Common;
using Application.Common.Mapper;
using Application.Middleware;
using Domain.Entities.User;
using Infrastructure;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Add automapper
builder.Services.AddAutoMapper(typeof(MappingProfile));
#endregion

#region AddAuthentication

builder.Services.AddAuthentication();
#endregion

builder.Services
       .AddApplication()
       .AddPersistence(builder.Configuration);


var app = builder.Build();

using var scop = app.Services.CreateScope();
var services = scop.ServiceProvider;
var context = services.GetRequiredService<ApplicationDbContext>();
var roleManager = services.GetRequiredService<RoleManager<Role>>();

await context.Database.MigrateAsync();
await Seed.SeedUsers(roleManager);

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

 