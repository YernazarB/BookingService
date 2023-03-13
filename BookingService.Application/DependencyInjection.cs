using BookingService.Application.Managers;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BookingService.Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddScoped<IRestManager, RestManager>();
        }
    }
}
