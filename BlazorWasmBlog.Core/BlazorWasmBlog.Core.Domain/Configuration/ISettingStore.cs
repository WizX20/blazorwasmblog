using Microsoft.Extensions.Configuration;

namespace BlazorWasmBlog.Core.Domain.Configuration
{
    public interface ISettingStore
    {
        T GetSettings<T>(string sectionName) where T : class, IConfigurationSettings, new();

        T GetDevelopmentSettings<T>(string sectionName) where T : class, IConfigurationSettings, new();

        IConfiguration GetConfigurationRoot(string configurationName, string fileName, bool useDevelopmentSettings);
    }
}