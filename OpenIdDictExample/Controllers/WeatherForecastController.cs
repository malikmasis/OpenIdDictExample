using Microsoft.AspNetCore.Mvc;

namespace OpenIdDictExample.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly ILogger<WeatherForecastController> _logger;
    private readonly ApplicationDbContext _applicationDbContext;

    public WeatherForecastController(ILogger<WeatherForecastController> logger, ApplicationDbContext applicationDbContext)
    {
        _logger = logger;
        _applicationDbContext = applicationDbContext;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return _applicationDbContext.Set<WeatherForecast>().ToList();
    }

    [HttpPost("Create")]
    public async Task Create()
    {
        await _applicationDbContext.Set<WeatherForecast>().AddAsync(new WeatherForecast()
        {
            Date = DateTime.Now,
            Summary = "Test",
            TemperatureC = 32
        });
        _logger.LogInformation("Created value");
        await _applicationDbContext.SaveChangesAsync();
    }
}
