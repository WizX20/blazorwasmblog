namespace BlazorWasmBlog.Modules.SquidexCms.Configuration
{
    public class SquidexCmsConfiguration
    {
        public string AppName { get; set; }

        public string Url { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }
    }

    public struct Constants
    {
        public const string SquidexCmsConfigurationFileName = nameof(SquidexCmsConfiguration) + ".json";
    }
}
