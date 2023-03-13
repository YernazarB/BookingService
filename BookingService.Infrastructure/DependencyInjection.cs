using BookingService.Application;
using BookingService.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace BookingService.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureServices(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton<IDistributedLockSource>(x => new PostgresDistributedLockSource(connectionString));
            services.AddApplicationServices();
        }
    }
}
