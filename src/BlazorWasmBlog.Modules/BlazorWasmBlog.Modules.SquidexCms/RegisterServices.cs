using BlazorWasmBlog.Core.Infrastructure.Extensions;
using BlazorWasmBlog.Modules.SquidexCms.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BlazorWasmBlog.Modules.SquidexCms
{
    public static class RegisterServices
    {
        /// <summary>
        /// Adds the squidex CMS services:
        /// - Adds the configuration settings class <see cref="SquidexCmsConfiguration"/> as singleton;
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="appAssembly">The active Blazor client application assembly.</param>
        public static void AddSquidexCms(this IServiceCollection services, Assembly appAssembly)
        {
            // Adds squidex configuration settings
            var squidexCmsConfiguration = appAssembly.GetApplicationSettings<SquidexCmsConfiguration>(Constants.SquidexCmsConfigurationFileName);
            services.AddSingleton(squidexCmsConfiguration);

            // TODO: Add future squidex CMS services here: ...
            // HttpClients
        }
    }
}
