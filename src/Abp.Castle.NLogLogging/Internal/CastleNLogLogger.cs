using System;
using System.Globalization;
using NLog;
using ILogger = Castle.Core.Logging.ILogger;

namespace Abp.Castle.NLogLogging.Internal
{
    [Serializable]
    internal class CastleNLogLogger : MarshalByRefObject,
        ILogger
    {
        protected internal NLog.ILogger Logger { get; }

        public CastleNLogLogger(NLog.ILogger logger)
        {
            Logger = logger;
        }


        public bool IsDebugEnabled => Logger.IsEnabled(LogLevel.Debug);

        public bool IsErrorEnabled => Logger.IsEnabled(LogLevel.Error);

        public bool IsFatalEnabled => Logger.IsEnabled(LogLevel.Fatal);

        public bool IsInfoEnabled => Logger.IsEnabled(LogLevel.Info);

        public bool IsWarnEnabled => Logger.IsEnabled(LogLevel.Warn);

        public override string ToString()
        {
            return Logger.ToString();
        }

        public virtual global::Castle.Core.Logging.ILogger CreateChildLogger(string name)
        {
            return new CastleNLogLogger(Logger.Factory.GetLogger(Logger.Name + "." + name));
        }

        public void Debug(string message)
        {
            Logger.Debug(message);
        }

        public void Debug(Func<string> messageFactory)
        {
            Logger.Debug(messageFactory.Invoke());
        }

        public void Debug(string message, Exception exception)
        {
            Logger.Debug(exception, message);
        }

        public void DebugFormat(string format, params object[] args)
        {
            Logger.Debug(CultureInfo.InvariantCulture, format, args);
        }

        public void DebugFormat(Exception exception, string format, params object[] args)
        {
            Logger.Debug(exception, CultureInfo.InvariantCulture, format, args);
        }

        public void DebugFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Logger.Debug(formatProvider, format, args);
        }

        public void DebugFormat(Exception exception, IFormatProvider formatProvider, string format,
            params object[] args)
        {
            Logger.Debug(exception, formatProvider, format, args);
        }

        public void Error(string message)
        {
            Logger.Error(message);
        }

        public void Error(Func<string> messageFactory)
        {
            Logger.Error(messageFactory.Invoke());
        }

        public void Error(string message, Exception exception)
        {
            Logger.Error(exception, message);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            Logger.Error(CultureInfo.InvariantCulture, format, args);
        }

        public void ErrorFormat(Exception exception, string format, params object[] args)
        {
            Logger.Error(exception, CultureInfo.InvariantCulture, format, args);
        }

        public void ErrorFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Logger.Error(formatProvider, format, args);
        }

        public void ErrorFormat(Exception exception, IFormatProvider formatProvider, string format,
            params object[] args)
        {
            Logger.Error(exception, formatProvider, format, args);
        }

        public void Fatal(string message)
        {
            Logger.Fatal(message);
        }

        public void Fatal(Func<string> messageFactory)
        {
            Logger.Fatal(messageFactory.Invoke());
        }

        public void Fatal(string message, Exception exception)
        {
            Logger.Fatal(exception, message);
        }

        public void FatalFormat(string format, params object[] args)
        {
            Logger.Fatal(CultureInfo.InvariantCulture, format, args);
        }

        public void FatalFormat(Exception exception, string format, params object[] args)
        {
            Logger.Fatal(exception, CultureInfo.InvariantCulture, format, args);
        }

        public void FatalFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Logger.Fatal(formatProvider, format, args);
        }

        public void FatalFormat(Exception exception, IFormatProvider formatProvider, string format,
            params object[] args)
        {
            Logger.Fatal(exception, formatProvider, format, args);
        }

        public void Info(string message)
        {
            Logger.Info(message);
        }

        public void Info(Func<string> messageFactory)
        {
            Logger.Info(messageFactory.Invoke());
        }

        public void Info(string message, Exception exception)
        {
            Logger.Info(exception, message);
        }

        public void InfoFormat(string format, params object[] args)
        {
            Logger.Info(CultureInfo.InvariantCulture, format, args);
        }

        public void InfoFormat(Exception exception, string format, params object[] args)
        {
            Logger.Info(exception, CultureInfo.InvariantCulture, format, args);
        }

        public void InfoFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Logger.Info(formatProvider, format, args);
        }

        public void InfoFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            Logger.Info(exception, formatProvider, format, args);
        }

        public void Warn(string message)
        {
            Logger.Warn(message);
        }

        public void Warn(Func<string> messageFactory)
        {
            Logger.Warn(messageFactory.Invoke());
        }

        public void Warn(string message, Exception exception)
        {
            Logger.Warn(exception, message);
        }

        public void WarnFormat(string format, params object[] args)
        {
            Logger.Warn(CultureInfo.InvariantCulture, format, args);
        }

        public void WarnFormat(Exception exception, string format, params object[] args)
        {
            Logger.Warn(exception, CultureInfo.InvariantCulture, format, args);
        }

        public void WarnFormat(IFormatProvider formatProvider, string format, params object[] args)
        {
            Logger.Warn(formatProvider, format, args);
        }

        public void WarnFormat(Exception exception, IFormatProvider formatProvider, string format, params object[] args)
        {
            Logger.Warn(exception, formatProvider, format, args);
        }
    }
}


