using Microsoft.Extensions.Configuration;
using System;

namespace BlazorWasmBlog.Core.Domain.Configuration
{
    public class DefaultConfigSettingStore : SettingsStore
    {
        /// <summary>
        /// Gets singleton instance.
        /// </summary>
        public static DefaultConfigSettingStore Instance { get; } = new DefaultConfigSettingStore();

        private DefaultConfigSettingStore()
        { }

        public override string GetSettings<T>()
        {
            var sectionName = typeof(T).Name;

        }

        public override IConfiguration GetConfiguration()
        {
            var memoryFileProvider = new InMemoryFileProvider(this.AppSettingsProvider.AppSettings);
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(memoryFileProvider, "appsettings.json", false, false)
                .Build();

            return configuration;
        }
    }
}