using Microsoft.EntityFrameworkCore;
using webapi_m_sqlserver.Domain.Models;

namespace webapi_m_sqlserver.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

        public DbSet<WeatherForecast> WeatherForecasts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // WeatherForecast
            modelBuilder.Entity<WeatherForecast>().HasKey(f => f.Id);
            modelBuilder.Entity<WeatherForecast>().Property(f => f.Id).IsRequired().ValueGeneratedOnAdd();
            modelBuilder.Entity<WeatherForecast>().Property(f => f.Continent).IsRequired().HasConversion(x => (int) x, x => (Continents) x);
        }
    }
}