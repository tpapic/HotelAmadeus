using Application.Core;
using Application.Hotel;
using Application.Interfaces;
using Infrastructure.Amadeus;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Extenisions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
            });

            services.AddScoped<IAmadeus, Amadeus>();
            services.AddScoped<IAmadeusProxy, AmadeusCacheRedisProxy>();

            services.AddMediatR(typeof(Search).Assembly);
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            return services;
        }
    }
}