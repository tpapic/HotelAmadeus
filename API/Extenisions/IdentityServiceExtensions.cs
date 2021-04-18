using System.Text;
using Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace API.Extenisions
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            //var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));

            // services.AddAuthentication();

            // services.AddAuthorization();


            return services;
        }
    }
}