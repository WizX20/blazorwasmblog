using BlazorWasmBlog.Modules.SquidexCms.Models;
using System.Threading.Tasks;

namespace BlazorWasmBlog.Modules.SquidexCms.Queries.Authentication
{
    public interface IGetBearerTokenQuery
    {
        Task<LoginResultModel> GetBlogPostModelAsync(string url, string clientId, string clientSecret);
    }
}