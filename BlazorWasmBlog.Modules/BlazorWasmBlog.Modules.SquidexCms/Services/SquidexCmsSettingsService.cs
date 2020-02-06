using BlazorWasmBlog.Core.Infrastructure.Configuration;
using BlazorWasmBlog.Modules.SquidexCms.Configuration;
using Dawn;

namespace BlazorWasmBlog.Modules.SquidexCms.Services
{
    public class SquidexCmsSettingsService
    {
        protected ISettingStore SettingStore { get; set; }

        public SquidexCmsSettingsService(ISettingStore settingStore)
        {
            Guard.Argument(settingStore, nameof(settingStore)).NotNull();

            this.SettingStore = settingStore;
        }

        public SquidexCmsSettings GetSquidexCmsSettings(bool useDevelopmentSettings)
        {
            var configurationName = typeof(SquidexCmsConfiguration).Name;
            var sectionName = typeof(SquidexCmsSettings).Name;

            if (useDevelopmentSettings)
            {
                var developmentSettings = this.SettingStore.GetDevelopmentSettings<SquidexCmsSettings>(
                    configurationName: configurationName,
                    sectionName: sectionName
                );

                return developmentSettings;
            }

            var settings = this.SettingStore.GetSettings<SquidexCmsSettings>(
                configurationName: configurationName,
                sectionName: sectionName
            );

            return settings;
        }
    }
}