using BlazorWasmBlog.Core.Infrastructure.Configuration;
using BlazorWasmBlog.Modules.SquidexCms.Services;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace BlazorWasmBlog.Modules.SquidexCms
{
    public static class RegisterServices
    {
        /// <summary>
        /// Adds the squidex CMS services:
        /// - Adds the configuration settings class of <see cref="IConfigurationSettings"/> as singleton;
        /// - Adds an instance of the <see cref="ISquidexCmsSettingsService"/>.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void AddSquidexCms<T>(this IServiceCollection services)
            where T : class, IConfigurationSettings
        {
            if (!services.Any(s => s.ServiceType == typeof(T)))
            {
                services.AddSingleton<IConfigurationSettings, T>();
            }

            services.AddSingleton<ISquidexCmsSettingsService, SquidexCmsSettingsService<T>>();

            // TODO: Add future squidex CMS services here: ...
            // HttpClients
        }
    }
}
