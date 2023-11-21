using Polaris.Extensions;

namespace Polaris
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
      
            builder.Services
                .ConfigureSqlContext(builder.Configuration)
                .ConfigureApplicationServices();
 
            var app = builder.Build();
            app
                .ConfigurePipeline()
                .ConfigureGlobalExceptionHandler()
                .Run();
        }
    }
}