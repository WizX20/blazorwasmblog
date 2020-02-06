using BlazorWasmBlog.Core.Infrastructure.Blazor.Factories;
using BlazorWasmBlog.Modules.SquidexCms.Models;
using Dawn;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorWasmBlog.Modules.SquidexCms.Queries.Authentication
{
    public class GetBearerTokenQuery : IGetBearerTokenQuery
    {
        private readonly IBlazorClientFactory blazorClientFactory;

        public GetBearerTokenQuery(IBlazorClientFactory blazorClientFactory)
        {
            Guard.Argument(blazorClientFactory, nameof(blazorClientFactory)).NotNull();

            this.blazorClientFactory = blazorClientFactory;
        }

        public async Task<LoginResultModel> GetBlogPostModelAsync(string url, string clientId, string clientSecret)
        {
            var blazorClient = this.blazorClientFactory.Create();

            var serviceUrl = $"{url}/identity-server/connect/token";
            var content = GetHttpContent(clientId, clientSecret);

            var response = await blazorClient.HttpClient.PostAsync(serviceUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                throw new SecurityException($"Failed to retrieve access token for client, " +
                    $"got HTTP {response.StatusCode}: {response.ReasonPhrase}");
            }

            var loginResultModel = JsonSerializer.Deserialize<LoginResultModel>(
                await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true }
            );

            return loginResultModel;
        }

        private static HttpContent GetHttpContent(string clientId, string clientSecret)
        {
            HttpContent content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("scope", "squidex-api"),
                new KeyValuePair<string, string>("grant_type", "client_credentials"),
                new KeyValuePair<string, string>("client_id", clientId),
                new KeyValuePair<string, string>("client_secret", clientSecret),
            });

            content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            return content;
        }
    }
}