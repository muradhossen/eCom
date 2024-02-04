using Application;
using Application.Common;
using Application.Common.Mapper;
using Application.Middleware;
using Domain.Entities.User;
using Infrastructure;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = JwtBearerDefaults.AuthenticationScheme.ToLower()
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type=ReferenceType.SecurityScheme,
                                    Id="Bearer"
                                }
                            },
                            new List<string>()
                        }
            });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
  .AddJwtBearer(options =>
  {
      options.TokenValidationParameters = new TokenValidationParameters()
      {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("TokenKey").Value)),
          ValidateIssuer = false,
          ValidateAudience = false
      };
  });

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

