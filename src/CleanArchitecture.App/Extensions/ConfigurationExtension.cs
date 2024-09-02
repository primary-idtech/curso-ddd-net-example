using CleanArchitecture.Infrastructure.Configurations;
using Microsoft.Extensions.Configuration;

namespace CleanArchitecture.App.Extensions
{
    public static class ConfigurationExtension
    {
        public static AppSettingConfig AppSettings { get; set; }

        public static void Configure(this IConfiguration configuration)
        {
            AppSettings = configuration.GetSection(nameof(AppSettings)).Get<AppSettingConfig>();
        }
    }
}
