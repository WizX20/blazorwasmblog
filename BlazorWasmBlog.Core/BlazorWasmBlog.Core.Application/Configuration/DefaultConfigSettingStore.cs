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
        /// <summary>
        /// The ASPNETCORE_ENVIRONMENT variable as defined in the launchSettings.json.
        /// </summary>
        public const string ASPNETCORE_ENVIRONMENT = nameof(ASPNETCORE_ENVIRONMENT);

        /// <summary>
        /// The active ASPNETCORE_ENVIRONMENT value when in development mode as defined in the launchSettings.json.
        /// </summary>
        public const string Development = nameof(Development);

        /// <summary>
        /// Gets the environment name from the launchSettings.json.
        /// </summary>
        public static string EnvironmentName { get; } = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        protected IEnumerable<IConfigurationSettings> ConfigurationSettings { get; }

        public DefaultConfigSettingStore(IEnumerable<IConfigurationSettings> configurationSettings)
        {
            Guard.Argument(configurationSettings, nameof(configurationSettings)).NotNull();

            this.ConfigurationSettings = configurationSettings;
        }

        /// <summary>
        /// Gets the settings as given class <typeparamref name="T"/> based on the active 
        /// <see cref="EnvironmentName"/>.
        /// </summary>
        /// <param name="configurationName">
        /// The name of the configuration, must be available in the <see cref="ConfigurationSettings"/>: 
        /// registered as service.
        /// </param>
        /// <param name="sectionName">
        /// The name of the configured section to convert to the class <typeparamref name="T"/>
        /// .</param>
        /// <returns>The requested configuration section as <typeparamref name="T"/>.</returns>
        public override T GetSettings<T>(string configurationName, string sectionName)
        {
            if (!string.IsNullOrEmpty(EnvironmentName)
                && EnvironmentName.Equals(Development, StringComparison.OrdinalIgnoreCase))
            {
                return this.GetDevelopmentSettings<T>(
                    configurationName: configurationName,
                    sectionName: sectionName
                );
            }

            return this.GetConfigurationInstance<T>(
                configurationName: configurationName,
                sectionName: sectionName,
                fileName: configurationName,
                useDevelopmentSettings: false
            );
        }

        /// <summary>
        /// Gets the settings as given class <typeparamref name="T"/> for the 
        /// <see cref="Development"/> environment.
        /// </summary>
        /// <param name="configurationName">
        /// The name of the configuration, must be available in the <see cref="ConfigurationSettings"/>: 
        /// registered as service.
        /// </param>
        /// <param name="sectionName">
        /// The name of the configured section to convert to the class <typeparamref name="T"/>
        /// .</param>
        /// <returns>The requested configuration section as <typeparamref name="T"/>.</returns>
        public override T GetDevelopmentSettings<T>(string configurationName, string sectionName)
        {
            return this.GetConfigurationInstance<T>(
                configurationName: configurationName,
                sectionName: sectionName,
                fileName: $"{Development}.{configurationName}",
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
