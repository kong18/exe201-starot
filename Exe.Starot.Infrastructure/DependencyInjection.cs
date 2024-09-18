using AutoMapper;
using Exe.Starot.Domain.Common.Interfaces;
using Exe.Starot.Domain.Entities.Repositories;
using Exe.Starot.Infrastructure.Persistence;
using Exe.Starot.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Exe.Starot.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("server"),
                    b =>
                    {
                        b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName);
                        b.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery);
                    });
                options.UseLazyLoadingProxies();
            });

            services.AddScoped<IUnitOfWork>(provider => provider.GetRequiredService<ApplicationDbContext>());

            services.AddTransient<IUserRepository, UserRepository>();

            services.AddAutoMapper(typeof(ApplicationDbContext));

            return services;
        }
    }
}
