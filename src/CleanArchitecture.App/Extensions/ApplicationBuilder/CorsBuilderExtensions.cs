using Microsoft.AspNetCore.Builder;

namespace CleanArchitecture.App.Extensions.ApplicationBuilder
{
    public static class CorsBuilderExtensions
    {
        public static void UseCorsExtension(this IApplicationBuilder app)
        {
            app.UseCors("CorsPolicy");
        }
    }
}
