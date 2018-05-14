using System;
using System.IO;
using Castle.Core.Logging;
using NLog;
using NLog.Config;
using ILogger = Castle.Core.Logging.ILogger;

namespace Abp.Castle.NLogLogging.Internal
{
    internal class NLogLoggerFactory : AbstractLoggerFactory
    {
        internal const string DefaultConfigFileName = "nlog.config";

        public NLogLoggerFactory()
            : this(DefaultConfigFileName)
        {
        }

        public NLogLoggerFactory(string configFile)
        {
            if (!File.Exists(configFile))
            {
                throw new FileNotFoundException(configFile);
            }
            LogManager.Configuration = new XmlLoggingConfiguration(configFile);
        }

        public override ILogger Create(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
           return  new CastleNLogLogger(LogManager.GetLogger(name));
        }

        public override ILogger Create(string name, LoggerLevel level)
        {
            throw new NotSupportedException(
                "Logger levels cannot be set at runtime. Please review your configuration file.");
        }
    }
}



