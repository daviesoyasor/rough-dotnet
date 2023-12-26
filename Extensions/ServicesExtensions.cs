using Hangfire;
using Hangfire.PostgreSql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            // Add services to the container.
            services.AddScoped<IRepository, UserRepository>();


            // NOTE: you can register your service, by specifying an interface to depend on and the concrete class that implements that interface
            // which means that when the interface is used as a type for a variable anywhere,
            // the application will inject the appropriate instance of the concrete class specified when it was registered
            services.AddScoped<IExternalAPI, XuperAuthService>();

            // OR: you can register the service with only the concrete implementation without specifying an interface
            // this means that the application will inject an instance of the concrete class, anytime the concrete class itself is used as a type for another variable
            services.AddScoped<XuperAuthService>();
            
            services.AddControllers();
            services.AddResponseCaching();
            services.AddMemoryCache();
            services.AddHttpClient();
            services.AddSingleton<ApiKeyAuthorizationFilter>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            return services;
        }

        public static IServiceCollection ConfigureHangfireServices (this IServiceCollection services, IConfiguration configuration )
        {
            services.AddHangfire(x =>
                 x.UsePostgreSqlStorage(configuration.GetConnectionString("hangfireConnection")));

            services.AddHangfireServer(); // responsible for actually starting the hangfire server that will execute the background jobs

            return services;
        }
    }
}
