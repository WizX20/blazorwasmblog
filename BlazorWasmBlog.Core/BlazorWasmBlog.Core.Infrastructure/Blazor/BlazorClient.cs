using Dawn;
using System.Net.Http;

namespace BlazorWasmBlog.Core.Infrastructure.Blazor
{
    public class BlazorClient
    {
        public HttpClient HttpClient { get; set; }

        public BlazorClient(HttpClient httpClient)
        {
            Guard.Argument(httpClient, nameof(httpClient)).NotNull();

            this.HttpClient = httpClient;
        }
    }
}