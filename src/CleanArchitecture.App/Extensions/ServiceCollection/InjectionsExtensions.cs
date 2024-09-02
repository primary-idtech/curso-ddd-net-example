using CleanArchitecture.Domain.Abstractions;
using CleanArchitecture.Infrastructure.ORM;
using CleanArchitecture.Infrastructure.ORM.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.App.Extensions.ServiceCollection
{
    public static class InjectionsExtensions
    {
        public static IServiceCollection AddInjections(this IServiceCollection services)
        {
            services.AddScoped<DbContext, MyDbContext>();
            services.AddScoped<IPortfolioRepository, PortfolioRepository>();

            return services;
        }
    }
}
