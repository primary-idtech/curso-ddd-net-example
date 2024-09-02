using CleanArchitecture.App.Extensions.ApplicationBuilder;
using CleanArchitecture.Infrastructure.Configurations;
using Microsoft.AspNetCore.Builder;

namespace CleanArchitecture.App.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void Configure(this IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseCorsExtension();
            app.UseHealthChecksExtension();
            app.UseSwaggerExtension();

            app.UseExceptionHandler(errorPipeline =>
            {
                errorPipeline.UseExceptionHandlerMiddleware(AppSettingConfig.IncludeErrorDetailInResponse);
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapControllers();
            });
        }
    }
}
