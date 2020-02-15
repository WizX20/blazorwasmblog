using System.IO;
using System.Reflection;
using System.Text.Json;

namespace BlazorWasmBlog.Core.Infrastructure.Extensions
{
    public static class AssemblyConfigurationExtensions
    {
        /// <summary>
        /// Gets the application settings from the given <paramref name="assembly"/>
        /// by using getting a stream from the manifest resource; and then deserializes
        /// the file to the given class <typeparamref name="T"/>.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="configurationFileName">The configuration file name.</param>
        /// <returns>The deserialized .</returns>
        public static T GetApplicationSettings<T>(this Assembly assembly, string configurationFileName)
            where T : class, new()
        {
            using (var stream = assembly.GetManifestResourceStream(configurationFileName))
            using (var reader = new StreamReader(stream))
            {
                var settings = reader.ReadToEnd();
                return JsonSerializer.Deserialize<T>(settings);
            }
        }
    }
}
