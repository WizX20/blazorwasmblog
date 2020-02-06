using Microsoft.Extensions.Configuration;

namespace BlazorWasmBlog.Core.Infrastructure.Configuration
{
    public abstract class SettingsStore : ISettingStore
    {
        public abstract T GetSettings<T>(string configurationName, string sectionName) where T : class, new();

        public abstract T GetDevelopmentSettings<T>(string configurationName, string sectionName) where T : class, new();

        public abstract IConfiguration GetConfigurationRoot(string configurationName, string fileName, bool useDevelopmentSettings);
    }
}