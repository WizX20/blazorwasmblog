namespace BlazorWasmBlog.Core.Application.Configuration
{
    public class ApplicationConfiguration
    {
        public string EnvironmentName { get; set; }
    }

    public struct Constants
    {
        public const string ApplicationConfigurationFileName = nameof(ApplicationConfiguration) + ".json";
    }
}
