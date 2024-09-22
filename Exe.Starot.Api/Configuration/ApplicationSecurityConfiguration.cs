using Exe.Starot.Api.Services;
using Exe.Starot.Application.Common.Interfaces;
using Exe.Starot.Domain.Entities.Repositories;
using Exe.Starot.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

using Microsoft.IdentityModel.Tokens;

using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Exe.Starot.Api.Configuration
{
    public static class ApplicationSecurityConfiguration
    {
        public static IServiceCollection ConfigureApplicationSecurity(
        this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IJwtService, JwtService>();
            JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
            services.AddHttpContextAccessor();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme; // Add this line
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("from sonhohuu deptrai6mui with love")),
                };
            })
       
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme); // Ensure cookies are used

            services.AddAuthorization(ConfigureAuthorization);

            return services;
        }

        private static void ConfigureAuthorization(AuthorizationOptions options)
        {
            // Configure policies and other authorization options here. For example:
            // options.AddPolicy("EmployeeOnly", policy => policy.RequireClaim("role", "employee"));
           
        }
    }
}
