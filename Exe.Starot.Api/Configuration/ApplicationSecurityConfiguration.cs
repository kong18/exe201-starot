using Exe.Starot.Api.Services;
using Exe.Starot.Application.Interfaces;
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("from kongez deptrai6mui with love")),
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
            options.AddPolicy("Business", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("role", "Business");
            });
            options.AddPolicy("Manager", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("role", "Manager");
            });
            options.AddPolicy("Kiosk", policy =>
            {
                policy.RequireAuthenticatedUser();
                policy.RequireClaim("role", "Kiosk");
            });
        }
    }
}
