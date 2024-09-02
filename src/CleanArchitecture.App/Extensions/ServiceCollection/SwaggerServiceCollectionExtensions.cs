using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.IO;

namespace CleanArchitecture.App.Extensions.ServiceCollection
{
    public static class SwaggerServiceCollectionExtensions
    {
        public static void AddSwaggerGenExtension(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = GetApplicationName(), Version = "v1" });

                var xmlFile = $"CleanArchitecture.Presentation.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                {
                    c.IncludeXmlComments(xmlPath);
                }
            });
        }

        private static string GetApplicationName()
        {
            return (System.Reflection.Assembly.GetEntryAssembly() ?? System.Reflection.Assembly.GetExecutingAssembly()).GetName().Name;
        }
    }
}
