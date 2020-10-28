using System;

using Microsoft.Extensions.Logging;

using Serilog.Debugging;

namespace Cogito.Extensions.Logging.Serilog
{

    /// <summary>
    /// Implements <see cref="ILoggerFactory"/> so that we can inject Serilog Logger.
    /// </summary>
    public class SerilogLoggerFactory : ILoggerFactory
    {

        readonly ILoggerProvider provider;

        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        /// <param name="provider"></param>
        public SerilogLoggerFactory(ILoggerProvider provider)
        {
            this.provider = provider ?? throw new ArgumentNullException(nameof(provider));
        }

        public ILogger CreateLogger(string categoryName)
        {
            return provider.CreateLogger(categoryName);
        }

        public void AddProvider(ILoggerProvider provider)
        {
            SelfLog.WriteLine("Ignoring added logger provider {0}.", provider);
        }

        public void Dispose()
        {

        }

    }

}
