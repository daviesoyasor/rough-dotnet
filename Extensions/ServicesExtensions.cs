using Microsoft.EntityFrameworkCore;
using Polaris.APIKeyAuthentication.Filters;
using Polaris.Entities;
using Polaris.External.API;
using Polaris.Repositories;

namespace Polaris.Extensions
{
    public static class ServicesExtensions
    {
        public static IServiceCollection ConfigureSqlContext(this IServiceCollection services, IConfiguration configurations)
        {
            services.AddDbContextPool<AppDbContext>(options => options.UseNpgsql(configurations.GetConnectionString("sqlConnection")));
            return services;
        }

        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            // Add services to the container.
            services.AddScoped<IRepository, UserRepository>();
            services.AddScoped<IExternalAPI, XuperAuthService>();
            
            services.AddControllers();
            services.AddResponseCaching();
            services.AddMemoryCache();
            services.AddHttpClient();
            services.AddSingleton<ApiKeyAuthorizationFilter>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
        }
    }
}
