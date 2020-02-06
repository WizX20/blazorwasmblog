using BlazorWasmBlog.Core.Application.Configuration;
using BlazorWasmBlog.Core.Infrastructure.Blazor;
using BlazorWasmBlog.Core.Infrastructure.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly.Retry;
using System.Net.Http;

namespace BlazorWasmBlog.Modules.SquidexCms
{
    public static class RegisterServices
    {
        public static void AddDefaultConfigSettingStore(this IServiceCollection services)
        {
            services.AddSingleton<ISettingStore, DefaultConfigSettingStore>();
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