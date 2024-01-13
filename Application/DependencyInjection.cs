using Application.Service;
using Application.ServiceInterface;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddTransient<ITestService, TestService>();
            return services;
        }
    }
}
