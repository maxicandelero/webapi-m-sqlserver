using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using webapi_m_sqlserver.Domain.Repositories;
using webapi_m_sqlserver.Domain.Services;
using webapi_m_sqlserver.Domain.Services.Background;
using webapi_m_sqlserver.Persistence;
using webapi_m_sqlserver.Persistence.Repositories;
using webapi_m_sqlserver.Services;
using webapi_m_sqlserver.Services.Background;

namespace webapi_m_sqlserver
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });
            services.AddDbContextPool<AppDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("Default")
            ));

            services.AddControllers();
            services.AddAutoMapper(typeof(Startup));
            // Swagger
             services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "Web API Template",
                        Description = "ASP.NET Core Web API Template (DDD)",
                    });
            });
            // Configure strongly typed settings object
            services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

            // MQClient - Uncomment the next line if you implement RabbitMQ (or other) to send messages, and inject IMQClient on your services.
            // services.AddSingleton<IMQClient, RabbitMQClient>();
            // MQ Background Services - Example to configure a hosted service to receive messages, uncomment and change the implementation for your propose.
            // services.AddHostedService<ExampleListener>();

            // Repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IWeatherForecastRepository, WeatherForecastRepository>();
            // Services
            services.AddScoped<IWeatherForecastService, WeatherForecastService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API Template");
            });
        }
    }
}
