using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace BlazorWasmBlog.Core.Application.Logging
{
    public class BlazeDebugger : IBlazeDebugger
    {
        private readonly IJSRuntime jsRuntime;

        public BlazeDebugger(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task ConsoleLog(string message, object data = null, LogLevel level = LogLevel.Info)
        {
#if DEBUG
            var output = data != null ? this.Serialize(data) : string.Empty;
            var logMethod = "";

            switch (level)
            {
                case LogLevel.Info:
                    logMethod = "window.console.info";
                    break;

                case LogLevel.Warn:
                    logMethod = "window.console.warn";
                    break;

                case LogLevel.Error:
                    logMethod = "window.console.error";
                    break;
            }

            await this.jsRuntime.InvokeVoidAsync($"{logMethod}", message, output);
#endif
        }

        private string Serialize(object obj)
        {
            return System.Text.Json.JsonSerializer.Serialize(obj);
        }
    }
}
