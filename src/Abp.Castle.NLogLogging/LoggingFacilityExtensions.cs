using Abp.Castle.NLogLogging.Internal;
using Castle.Facilities.Logging;

namespace Abp.Castle.NLogLogging
{
    public static class LoggingFacilityExtensions
    {
        public static LoggingFacility UseAbpNLog(this LoggingFacility loggingFacility)
        {
            return loggingFacility.LogUsing<NLogLoggerFactory>();
        }
    }
}


