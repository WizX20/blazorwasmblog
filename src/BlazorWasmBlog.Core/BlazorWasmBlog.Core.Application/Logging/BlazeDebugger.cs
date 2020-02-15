using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using Mono.WebAssembly.Interop;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlazorWasmBlog.Core.Application.Logging
{
    public class BlazeDebugger : IBlazeDebugger
    {
        private readonly IJSRuntime jsRuntime;

        public BlazeDebugger(IServiceProvider serviceProvider)
        {
            var jsRuntime = serviceProvider.GetService<IServiceProvider>() as IJSRuntime;
            if (jsRuntime == null || jsRuntime.GetType() != typeof(MonoWebAssemblyJSRuntime))
            {
                return;
            }

            this.jsRuntime = jsRuntime;
        }

        public async Task ConsoleLog(string message, object data = null, LogLevel level = LogLevel.Info)
        {
            if (this.jsRuntime == null)
            {
                return;
            }

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
            return JsonSerializer.Serialize(obj);
        }
    }
}
