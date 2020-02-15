using System.Threading.Tasks;

namespace BlazorWasmBlog.Core.Application.Logging
{
    public interface IBlazeDebugger
    {
        Task ConsoleLog(string message, object data = null, LogLevel level = LogLevel.Info);
    }
}
