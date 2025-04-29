using Application.RepositoryInterface;
using Domain.Entities.User;
using Infrastructure.Persistance;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore; 
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));

                 
            });

            services.AddIdentityCore<AuthUser>(opt =>
            {
                opt.Password.RequireNonAlphanumeric = false;
            }).AddRoles<Role>()
              .AddRoleManager<RoleManager<Role>>()
              .AddSignInManager<SignInManager<AuthUser>>()
              .AddRoleValidator<RoleValidator<Role>>()
              .AddEntityFrameworkStores<ApplicationDbContext>();
          
            services.AddTransient<ITestRepository, TestRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<ISubCategoryRepository, SubCategoryRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<ICartRepository, CartRepository>();

            return services;
        }
    }
 
}
