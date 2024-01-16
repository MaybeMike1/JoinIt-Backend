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
            var context = new DatabaseContext();
            var seeder = new Seeder(context);
            seeder.SeedActivityTypes();
            seeder.SeedCountries();
            seeder.SeedZipCodes(context.Countries.ToList());
            seeder.SeedAddresses(context.Zips.ToList());
            var listOfUserGuids = seeder.SeedUsers();
            var listOfActivityGuids = seeder.SeedActivities(context.Addresses.ToList(), listOfUserGuids);
            services.AddDbContext<DatabaseContext>(opt =>
            {
                opt.UseSqlServer("data source=DESKTOP-G7M4EEG\\SQLEXPRESS;initial catalog=JoinIt-dev-v2;trusted_connection=true;Encrypt=True;TrustServerCertificate=True");
            });

            services.AddScoped<DatabaseContext>();
            return services;
        }
    }
}