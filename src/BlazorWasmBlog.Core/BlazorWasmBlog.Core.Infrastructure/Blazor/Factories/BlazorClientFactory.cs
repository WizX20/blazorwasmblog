using Microsoft.Extensions.DependencyInjection;
using System;

namespace BlazorWasmBlog.Core.Infrastructure.Blazor.Factories
{
    public class BlazorClientFactory : IBlazorClientFactory
    {
        private readonly IServiceProvider serviceProvider;

        public BlazorClientFactory(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public BlazorClient Create()
        {
            return this.serviceProvider.GetRequiredService<BlazorClient>();
        }
    }
}