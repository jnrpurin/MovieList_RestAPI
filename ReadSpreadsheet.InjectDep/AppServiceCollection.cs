using Microsoft.Extensions.DependencyInjection;
using ReadSpreadsheet.App.Services;
using ReadSpreadsheet.Domain.Interfaces.Service;

namespace ReadSpreadsheet.InjectDep
{
    public static class AppServiceCollection
    {
        /// <summary>
        /// Register all the services
        /// </summary>
        /// <param name="services">Service collection extension</param>
        public static void RegisterGeneralServices(this IServiceCollection services)
        {
            RegisterAppServices(services);
        }

        /// <summary>
        /// Register the application services
        /// </summary>
        /// <param name="services">Service collection extension</param>
        private static void RegisterAppServices(IServiceCollection services)
        {
            services.AddScoped(typeof(ISpreadsheetService), typeof(SpreadsheetService));
        }
    }
}
