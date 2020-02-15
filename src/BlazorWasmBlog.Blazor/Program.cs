#pragma warning disable RCS1102 // Make class static.

using BlazorWasmBlog.Core.Application;
using BlazorWasmBlog.Core.Application.Logging;
using BlazorWasmBlog.Modules.SquidexCms;
using Microsoft.AspNetCore.Blazor.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
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
            // Logging
            services.AddSingleton<IBlazeDebugger, BlazeDebugger>();

            // Configuration
            var assembly = Assembly.GetExecutingAssembly();
            services.AddDefaultApplicationServices(assembly);
            services.AddSquidexCms(assembly); // using squidex as Content Management System.

            // TODO: Future service registrations here: ...
            // Caching
            // Http and retry policies
            // Blog-post services
        }
    }
}
