using BlazorWasmBlog.Core.Infrastructure.Configuration;
using BlazorWasmBlog.Modules.SquidexCms.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWasmBlog.Modules.SquidexCms
{
    public static class RegisterServices
    {
        public static void AddSquidexCms(this IServiceCollection services)
        {
            services.AddSingleton<IConfigurationSettings, SquidexCmsConfiguration>();
        }
    }
}