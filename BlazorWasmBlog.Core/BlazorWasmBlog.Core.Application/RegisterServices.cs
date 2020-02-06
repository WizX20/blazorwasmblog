using BlazorWasmBlog.Core.Application.Configuration;
using BlazorWasmBlog.Core.Infrastructure.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlazorWasmBlog.Modules.SquidexCms
{
    public static class RegisterServices
    {
        public static void AddDefaultConfigSettingStore(this IServiceCollection services)
        {
            services.AddSingleton<ISettingStore, DefaultConfigSettingStore>();
        }
    }
}