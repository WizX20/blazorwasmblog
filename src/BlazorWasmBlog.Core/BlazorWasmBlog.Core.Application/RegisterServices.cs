using BlazorWasmBlog.Core.Application.Configuration;
using BlazorWasmBlog.Core.Infrastructure.Blazor;
using BlazorWasmBlog.Core.Infrastructure.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Polly.Retry;
using System.Net.Http;
using System.Reflection;

namespace BlazorWasmBlog.Core.Application
{
    public static class RegisterServices
    {
        /// <summary>
        /// Adds the default application services:
        /// - Adds the configuration settings class <see cref="ApplicationConfiguration"/> as singleton;
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="appAssembly">The active Blazor client application assembly.</param>
        public static void AddDefaultApplicationServices(this IServiceCollection services, Assembly appAssembly)
        {
            // Adds application configuration settings
            var applicationConfiguration = appAssembly.GetApplicationSettings<ApplicationConfiguration>(Constants.ApplicationConfigurationFileName);
            services.AddSingleton(applicationConfiguration);

            // TODO: Add future application services here: ...
        }

        public static void AddBlazorHttpClient(
            this IServiceCollection services,
            AsyncRetryPolicy<HttpResponseMessage> retryPolicy)
        {
            services.AddHttpClient<BlazorClient>(nameof(BlazorClient))
                .AddPolicyHandler(retryPolicy);
        }
    }
}
