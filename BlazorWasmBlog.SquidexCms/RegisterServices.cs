using BlazorWasmBlog.Core.Domain.Configuration;
using BlazorWasmBlog.SquidexCms.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWasmBlog.SquidexCms
{
    public static class RegisterServices
    {
        public static void AddSquidexCms(this IServiceCollection services)
        {
            services.AddSingleton<IConfigurationSettings, SquidexCmsSettings>();
        }
    }
}