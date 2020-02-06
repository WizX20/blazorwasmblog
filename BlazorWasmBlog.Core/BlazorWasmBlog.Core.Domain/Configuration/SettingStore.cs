using Microsoft.Extensions.Configuration;

namespace BlazorWasmBlog.Core.Domain.Configuration
{
    public abstract class SettingsStore : ISettingStore
    {
        public abstract T GetSettings<T>(string sectionName) where T : class, IConfigurationSettings, new();

        public abstract T GetDevelopmentSettings<T>(string sectionName) where T : class, IConfigurationSettings, new();

        public abstract IConfiguration GetConfigurationRoot(string configurationName, string fileName, bool useDevelopmentSettings);
    }
}