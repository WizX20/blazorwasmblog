using Microsoft.Extensions.Configuration;

namespace BlazorWasmBlog.Core.Domain.Configuration
{
    public abstract class SettingsStore : ISettingStore
    {
        public abstract string GetSettings<T>() where T : class, IConfigurationSettings;

        public abstract IConfiguration GetConfiguration();
    }
}