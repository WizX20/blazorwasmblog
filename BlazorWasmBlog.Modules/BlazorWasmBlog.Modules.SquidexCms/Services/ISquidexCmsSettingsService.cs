using BlazorWasmBlog.Modules.SquidexCms.Configuration;

namespace BlazorWasmBlog.Modules.SquidexCms.Services
{
    public interface ISquidexCmsSettingsService
    {
        SquidexCmsSettings GetSquidexCmsSettings(bool useDevelopmentSettings);
    }
}