using BlazorWasmBlog.Core.Infrastructure.Configuration;
using BlazorWasmBlog.Core.Infrastructure.Extensions;
using Dawn;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorWasmBlog.Core.Application.Configuration
{
    public class DefaultConfigSettingStore : SettingsStore
    {
        protected IEnumerable<IConfigurationSettings> ConfigurationSettings { get; set; }

        public DefaultConfigSettingStore(IEnumerable<IConfigurationSettings> configurationSettings)
        {
            Guard.Argument(configurationSettings, nameof(configurationSettings)).NotNull();

            this.ConfigurationSettings = configurationSettings;
        }

        public override T GetSettings<T>(string configurationName, string sectionName)
        {
            return this.GetConfigurationInstance<T>(
                configurationName: configurationName,
                sectionName: sectionName,
                fileName: configurationName,
                useDevelopmentSettings: false
            );
        }

        public override T GetDevelopmentSettings<T>(string configurationName, string sectionName)
        {
            return this.GetConfigurationInstance<T>(
                configurationName: configurationName,
                sectionName: sectionName,
                fileName: $"Development.{configurationName}",
                useDevelopmentSettings: true
            );
        }

        private T GetConfigurationInstance<T>(
            string configurationName,
            string sectionName,
            string fileName,
            bool useDevelopmentSettings)
            where T : class, new()
        {
            var configuration = this.GetConfigurationRoot(
                configurationName: configurationName,
                fileName: fileName,
                useDevelopmentSettings: useDevelopmentSettings
            );

            var settingsInstance = new T();
            configuration.GetSection(sectionName).Bind(settingsInstance);

            return settingsInstance;
        }

        public override IConfiguration GetConfigurationRoot(
            string configurationName,
            string fileName,
            bool useDevelopmentSettings)
        {
            // Find the injected configuration settings instance.
            var configurationSettings = this.ConfigurationSettings.FirstOrDefault(
                c => c.GetType().Name == configurationName);
            if (configurationSettings == null)
            {
                throw new TypeLoadException($"{nameof(DefaultConfigSettingStore)}.{nameof(GetSettings)}: " +
                    $"No {nameof(IConfigurationSettings)} found with the name '{configurationName}'!");
            }

            // Use a custom file provider to build the configuration root in memory.
            InMemoryFileProvider inMemoryFileProvider;
            if (useDevelopmentSettings)
            {
                inMemoryFileProvider = new InMemoryFileProvider(configurationSettings.GetDevelopmentSettings());
            }
            else
            {
                inMemoryFileProvider = new InMemoryFileProvider(configurationSettings.GetSettings());
            }

            var configuration = new ConfigurationBuilder()
                .AddJsonFile(inMemoryFileProvider, $"{fileName}.json", false, false)
                .Build();

            return configuration;
        }
    }
}