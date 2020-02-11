#pragma warning disable RCS1102 // Make class static.

using BlazorWasmBlog.Blazor.Configuration;
using BlazorWasmBlog.Core.Application;
using BlazorWasmBlog.Modules.SquidexCms;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace BlazorWasmBlog.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            RegisterServices(builder.Services);

            await builder.Build().RunAsync();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            // Configuration
            services.AddDefaultConfigSettingStore(); // use default in-memory configuration
            services.AddSquidexCms<SquidexCmsConfigurationSettings>(); // using squidex as Content Management System

            // TODO: Future service registrations here: ...
            // Caching
            // Http and retry policies
            // Blog-post services
        }
    }
}
