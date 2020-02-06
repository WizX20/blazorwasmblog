namespace BlazorWasmBlog.Core.Domain.Configuration
{
    public interface IConfigurationSettings
    {
        string GetSettings();

        string GetDevelopmentSettings();
    }
}