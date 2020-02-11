using BlazorWasmBlog.Core.Infrastructure.Configuration;
using BlazorWasmBlog.Modules.SquidexCms.Configuration;
using Dawn;

namespace BlazorWasmBlog.Modules.SquidexCms.Services
{
    public class SquidexCmsSettingsService<T> : ISquidexCmsSettingsService
        where T : class, IConfigurationSettings
    {
        protected ISettingStore SettingStore { get; }

        public SquidexCmsSettingsService(ISettingStore settingStore)
        {
            Guard.Argument(settingStore, nameof(settingStore)).NotNull();

            this.SettingStore = settingStore;
        }

        public SquidexCmsSettings GetSquidexCmsSettings()
        {
            var configurationName = typeof(T).Name;
            var sectionName = typeof(SquidexCmsSettings).Name;

            var settings = this.SettingStore.GetSettings<SquidexCmsSettings>(
                configurationName: configurationName,
                sectionName: sectionName
            );

            return settings;
        }
    }
}
