using JoinIt_Backend.Shared.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JoinIt_Backend.Shared
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddShared(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DatabaseContext>();
            services.AddDbContext<DatabaseContext>(opt =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("JoinIt-Backend"));
            });
            return services;
        }
    }
}