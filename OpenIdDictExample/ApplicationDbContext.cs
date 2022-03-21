using Microsoft.EntityFrameworkCore;

namespace OpenIdDictExample;

public class ApplicationDbContext : DbContext
{
    public DbSet<WeatherForecast> WeatherForecast { get; set; }

    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
}