using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using webapi_m_sqlserver.Domain.Repositories;
using webapi_m_sqlserver.Domain.Services;
using webapi_m_sqlserver.Persistence;
using webapi_m_sqlserver.Persistence.Repositories;
using webapi_m_sqlserver.Services;

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
            //Swagger
             services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "WebAPI-M-SqlServer",
                        Description = "DDD WebAPI Template for SQL Server",
                    });
            });

            // Repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IWeatherForecastRepository, WeatherForecastRepository>();
            // Service
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
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI-M-SqlServer");
            });
        }
    }
}
