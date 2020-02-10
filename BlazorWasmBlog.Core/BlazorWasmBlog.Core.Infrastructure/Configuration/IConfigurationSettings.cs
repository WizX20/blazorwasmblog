namespace BlazorWasmBlog.Core.Infrastructure.Configuration
{
    public interface IConfigurationSettings
    {
        string GetSettings(string environmentName);
    }
}
