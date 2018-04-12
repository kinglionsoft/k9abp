using System;
using System.IO;
using Castle.Core.Logging;
using NLog.Config;

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
            if (!System.IO.File.Exists(configFile))
            {
                throw new FileNotFoundException(configFile);
            }
            NLog.LogManager.Configuration = new XmlLoggingConfiguration(configFile);
        }

        public override ILogger Create(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }
           return  new CastleNLogLogger(NLog.LogManager.GetLogger(name));
            //  var logger=   NLog.LogManager.CreateNullLogger().Factory.GetLogger<CastleNLogLogger>(name);
            //  return (ILogger)logger;
        }

        public override ILogger Create(string name, LoggerLevel level)
        {
            throw new NotSupportedException(
                "Logger levels cannot be set at runtime. Please review your configuration file.");
        }
    }
}



