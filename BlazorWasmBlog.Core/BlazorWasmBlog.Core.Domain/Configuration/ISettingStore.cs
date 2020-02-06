using Microsoft.Extensions.Configuration;

namespace BlazorWasmBlog.Core.Domain.Configuration
{
    public interface ISettingStore
    {
        string GetSettings<T>() where T : class, IConfigurationSettings;

        IConfiguration GetConfiguration();
    }
}