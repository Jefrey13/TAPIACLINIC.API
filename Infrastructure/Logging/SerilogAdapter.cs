using Application.Services;
using Serilog;

namespace Infrastructure.Logging
{
    public class SerilogAdapter<T> : ILoggerAdapter<T>
    {
        private readonly ILogger _logger;

        public SerilogAdapter()
        {
            _logger = Log.Logger;
        }

        public void LogInformation(string message)
        {
            _logger.Information($"[{typeof(T).Name}] {message}");
        }

        public void LogError(string message)
        {
            _logger.Error($"[{typeof(T).Name}] {message}");
        }
    }
}