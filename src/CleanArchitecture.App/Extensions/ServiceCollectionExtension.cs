using CleanArchitecture.App.Extensions.ServiceCollection;
using CleanArchitecture.Infrastructure.Configurations;
using CleanArchitecture.Infrastructure.ORM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CleanArchitecture.App.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void Configure(this IServiceCollection services)
        {
            services.AddCorsExtension();
            services.AddHealthChecksExtension();
            services.AddInjections();
            services.AddSwaggerGenExtension();
            services.AddHttpContextAccessor();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Application.AssemblyReference.Assembly));

            services.AddControllers(o =>
            {
                o.Filters.Add(new ProducesResponseTypeAttribute(400));
            }).AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddDbContext<MyDbContext>(opt =>
            {
                opt.UseSqlServer(AppSettingConfig.DataBaseConnectionString);
            });
        }
    }
}
