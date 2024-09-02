using Microsoft.AspNetCore.Builder;

namespace CleanArchitecture.App.Extensions.ApplicationBuilder
{
    public static class HealthChecksBuilderExtensions
    {
        public static void UseHealthChecksExtension(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/health");
        }
    }
}
