using Microsoft.Extensions.Configuration;

namespace BlazorWasmBlog.Core.Infrastructure.Configuration
{
    public interface ISettingStore
    {
        T GetSettings<T>(string configurationName, string sectionName) where T : class, new();

        T GetDevelopmentSettings<T>(string configurationName, string sectionName) where T : class, new();

        IConfiguration GetConfigurationRoot(string configurationName, string fileName, bool useDevelopmentSettings);
    }
}