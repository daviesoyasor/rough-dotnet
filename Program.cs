using Polaris.Extensions;
using Serilog;
using Serilog.Sinks.Elasticsearch;

namespace Polaris
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            #region SERILOG LOGGING SETTINGS

            //Make asp.net use appsettings.json before it builds the application else it fails if no appsettings.json doesn't exist
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string SegLogUrl = configuration.GetValue<string>("AppSettings:SeqLogUrl");
            string ELKLogUrl = configuration.GetValue<string>("AppSettings:ELKLogUrl");


            Log.Logger = new LoggerConfiguration()
                    .WriteTo.Seq(SegLogUrl)
                    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(ELKLogUrl))
                    {
                        IndexFormat = "ussd",
                        DetectElasticsearchVersion = false,
                        AutoRegisterTemplate = true,
                    })
                    .CreateLogger();

            builder.Host.UseSerilog();

            #endregion

            #region APPLICATION SERVICES
            builder.Services
                .ConfigureSqlContext(builder.Configuration)
                .ConfigureKafkaServices()
                .ConfigureHangfireServices(builder.Configuration)
                .ConfigureApplicationServices();
            #endregion

            var app = builder.Build();
            app
                .ConfigurePipeline()
                .ConfigureGlobalExceptionHandler()
                .Run();
        }
    }
}