using Microsoft.AspNetCore.Builder;

namespace MyTODOList.WebAPI.DI
{
    public static class ConfigurationAppExtensions
    {
        public static void AddConfigurationApp(this WebApplication app)
        {
            ConfigureSwagger(app);
            ConfigureCors(app);
        }

        private static void ConfigureSwagger(WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        private static void ConfigureCors(WebApplication app)
        {
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
        }
    }
}
