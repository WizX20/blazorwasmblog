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
        /// Gets the environment name from the launchSettings.json.
        /// </summary>
        public static string EnvironmentName { get; } = Environment.GetEnvironmentVariable(ASPNETCORE_ENVIRONMENT);

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
            var configuration = this.GetConfigurationRoot(
                configurationName: configurationName,
                fileName: configurationName,
                environmentName: EnvironmentName
            );

            var settingsInstance = new T();
            configuration.GetSection(sectionName).Bind(settingsInstance);

            return settingsInstance;
        }

        /// <summary>
        /// Loads the JSON configuration with <paramref name="configurationName"/> from the
        /// available <see cref="ConfigurationSettings"/> using a <see cref="InMemoryFileProvider"/>.
        /// </summary>
        /// <param name="configurationName">The configuration name.</param>
        /// <param name="fileName">The name of the JSON file.</param>
        /// <param name="environmentName">The active environment name.</param>
        /// <returns>The <see cref="IConfiguration"/> built by the <see cref="ConfigurationBuilder"/>.</returns>
        public override IConfiguration GetConfigurationRoot(
            string configurationName,
            string fileName,
            string environmentName)
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
            var inMemoryFileProvider = new InMemoryFileProvider(configurationSettings.GetSettings(environmentName));
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(inMemoryFileProvider, $"{fileName}.{environmentName}.json", false, false)
                .Build();

            return configuration;
        }
    }
}
