using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Polaris.APIKeyAuthentication.Custom.Attributes;
using Polaris.Entities;
using Polaris.External.API;
using Polaris.Repositories;
using Polaris.Service.Kafka;
using System.Text.Json;

namespace Polaris.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IRepository _repository;
        private readonly IExternalAPI _xuperauthService;
        private readonly KafkaProducer _kafkaProducer;
        private readonly IConfiguration _config;


        public WeatherForecastController(
            ILogger<WeatherForecastController> logger, 
            IRepository repository, 
            IExternalAPI xuperauthService, 
            KafkaProducer kafkaProducer, 
            IConfiguration configuration)
        {
            _logger = logger;
            _repository = repository;
            _xuperauthService = xuperauthService;
            _kafkaProducer = kafkaProducer;
            _config = configuration;
        }

        [Auth]
        [ApiKey]
        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Get()
        {
            var result = await _xuperauthService.GetSubscriptionPlansAsync();
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpGet, Route("GetXuperauthplans")]
        public async Task<IActionResult> GetSubscriptionPlans()
        {
            //BackgroundJob.Enqueue(() => Console.WriteLine("This is a hangfire fire and forget"));
            //BackgroundJob.Schedule(() => Console.WriteLine("Hangfire to run after 3 minutes"), TimeSpan.FromMinutes(3));
            //RecurringJob.AddOrUpdate(() => Console.WriteLine("An Hangfire program that runs every 30 minutes"), Cron.Minutely);
            var reqbody = new { error = false, message = "Successful testing" };
            _logger.LogInformation("Testing Serilog Logger {@request}", reqbody);
            var result = await _xuperauthService.GetSubscriptionPlansAsync();

            var serializedMessage = JsonConvert.SerializeObject(new { error = false, message = "Response returned from xuperauth", data = result });
            _kafkaProducer.PublishToKafka(_config.GetValue<string>("kafkaTopic:Notification"), serializedMessage);
            return StatusCode(StatusCodes.Status201Created, result);
        }

        [HttpPost(Name = "Login")]
        public async Task<IActionResult> Login(Employee employee)
        {

            if (!ModelState.IsValid)
            {
                throw new Exception("Not valid");
            }
                var result = await _repository.AddEmployee(employee);
                return Ok(result); 
        }
    }
}