using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Polaris.APIKeyAuthentication.Custom.Attributes;
using Polaris.Entities;
using Polaris.External.API;
using Polaris.Repositories;
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

 
        public WeatherForecastController(ILogger<WeatherForecastController> logger, IRepository repository, IExternalAPI xuperauthService)
        {
            _logger = logger;
            _repository = repository;
            _xuperauthService = xuperauthService;
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
            BackgroundJob.Enqueue(() => Console.WriteLine("This is a hangfire fire and forget"));
            BackgroundJob.Schedule(() => Console.WriteLine("Hangfire to run after 3 minutes"), TimeSpan.FromMinutes(3));
            RecurringJob.AddOrUpdate(() => Console.WriteLine("An Hangfire program that runs every 30 minutes"), Cron.Minutely);
            var result = await _xuperauthService.GetSubscriptionPlansAsync();
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