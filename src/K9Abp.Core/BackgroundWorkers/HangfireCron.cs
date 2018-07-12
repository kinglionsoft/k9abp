using System;

namespace Abp.Threading.BackgroundWorkers
{
    /// <summary>
    /// Helper class that provides common values for the cron expressions.
    /// </summary>
    public static class HangfireCron
    {
        /// <summary>Returns cron expression that fires every minute.</summary>
        public static string Minutely()
        {
            return "* * * * *";
        }

        /// <summary>
        /// Returns cron expression that fires every hour at the first minute.
        /// </summary>
        public static string Hourly()
        {
            return HangfireCron.Hourly(0);
        }

        /// <summary>
        /// Returns cron expression that fires every hour at the specified minute.
        /// </summary>
        /// <param name="minute">The minute in which the schedule will be activated (0-59).</param>
        public static string Hourly(int minute)
        {
            return $"{minute} * * * *";
        }

        /// <summary>
        /// Returns cron expression that fires every day at 00:00 UTC.
        /// </summary>
        public static string Daily()
        {
            return HangfireCron.Daily(0);
        }

        /// <summary>
        /// Returns cron expression that fires every day at the first minute of
        /// the specified hour in UTC.
        /// </summary>
        /// <param name="hour">The hour in which the schedule will be activated (0-23).</param>
        public static string Daily(int hour)
        {
            return HangfireCron.Daily(hour, 0);
        }

        /// <summary>
        /// Returns cron expression that fires every day at the specified hour and minute
        /// in UTC.
        /// </summary>
        /// <param name="hour">The hour in which the schedule will be activated (0-23).</param>
        /// <param name="minute">The minute in which the schedule will be activated (0-59).</param>
        public static string Daily(int hour, int minute)
        {
            return $"{minute} {hour} * * *";
        }

        /// <summary>
        /// Returns cron expression that fires every week at Monday, 00:00 UTC.
        /// </summary>
        public static string Weekly()
        {
            return HangfireCron.Weekly(DayOfWeek.Monday);
        }

        /// <summary>
        /// Returns cron expression that fires every week at 00:00 UTC of the specified
        /// day of the week.
        /// </summary>
        /// <param name="dayOfWeek">The day of week in which the schedule will be activated.</param>
        public static string Weekly(DayOfWeek dayOfWeek)
        {
            return HangfireCron.Weekly(dayOfWeek, 0);
        }

        /// <summary>
        /// Returns cron expression that fires every week at the first minute
        /// of the specified day of week and hour in UTC.
        /// </summary>
        /// <param name="dayOfWeek">The day of week in which the schedule will be activated.</param>
        /// <param name="hour">The hour in which the schedule will be activated (0-23).</param>
        public static string Weekly(DayOfWeek dayOfWeek, int hour)
        {
            return HangfireCron.Weekly(dayOfWeek, hour, 0);
        }

        /// <summary>
        /// Returns cron expression that fires every week at the specified day
        /// of week, hour and minute in UTC.
        /// </summary>
        /// <param name="dayOfWeek">The day of week in which the schedule will be activated.</param>
        /// <param name="hour">The hour in which the schedule will be activated (0-23).</param>
        /// <param name="minute">The minute in which the schedule will be activated (0-59).</param>
        public static string Weekly(DayOfWeek dayOfWeek, int hour, int minute)
        {
            return $"{minute} {hour} * * {dayOfWeek}";
        }

        /// <summary>
        /// Returns cron expression that fires every month at 00:00 UTC of the first
        /// day of month.
        /// </summary>
        public static string Monthly()
        {
            return HangfireCron.Monthly(1);
        }

        /// <summary>
        /// Returns cron expression that fires every month at 00:00 UTC of the specified
        /// day of month.
        /// </summary>
        /// <param name="day">The day of month in which the schedule will be activated (1-31).</param>
        public static string Monthly(int day)
        {
            return HangfireCron.Monthly(day, 0);
        }

        /// <summary>
        /// Returns cron expression that fires every month at the first minute of the
        /// specified day of month and hour in UTC.
        /// </summary>
        /// <param name="day">The day of month in which the schedule will be activated (1-31).</param>
        /// <param name="hour">The hour in which the schedule will be activated (0-23).</param>
        public static string Monthly(int day, int hour)
        {
            return HangfireCron.Monthly(day, hour, 0);
        }

        /// <summary>
        /// Returns cron expression that fires every month at the specified day of month,
        /// hour and minute in UTC.
        /// </summary>
        /// <param name="day">The day of month in which the schedule will be activated (1-31).</param>
        /// <param name="hour">The hour in which the schedule will be activated (0-23).</param>
        /// <param name="minute">The minute in which the schedule will be activated (0-59).</param>
        public static string Monthly(int day, int hour, int minute)
        {
            return $"{minute} {hour} {day} * *";
        }

        /// <summary>
        /// Returns cron expression that fires every year on Jan, 1st at 00:00 UTC.
        /// </summary>
        public static string Yearly()
        {
            return HangfireCron.Yearly(1);
        }

        /// <summary>
        /// Returns cron expression that fires every year in the first day at 00:00 UTC
        /// of the specified month.
        /// </summary>
        /// <param name="month">The month in which the schedule will be activated (1-12).</param>
        public static string Yearly(int month)
        {
            return HangfireCron.Yearly(month, 1);
        }

        /// <summary>
        /// Returns cron expression that fires every year at 00:00 UTC of the specified
        /// month and day of month.
        /// </summary>
        /// <param name="month">The month in which the schedule will be activated (1-12).</param>
        /// <param name="day">The day of month in which the schedule will be activated (1-31).</param>
        public static string Yearly(int month, int day)
        {
            return HangfireCron.Yearly(month, day, 0);
        }

        /// <summary>
        /// Returns cron expression that fires every year at the first minute of the
        /// specified month, day and hour in UTC.
        /// </summary>
        /// <param name="month">The month in which the schedule will be activated (1-12).</param>
        /// <param name="day">The day of month in which the schedule will be activated (1-31).</param>
        /// <param name="hour">The hour in which the schedule will be activated (0-23).</param>
        public static string Yearly(int month, int day, int hour)
        {
            return HangfireCron.Yearly(month, day, hour, 0);
        }

        /// <summary>
        /// Returns cron expression that fires every year at the specified month, day,
        /// hour and minute in UTC.
        /// </summary>
        /// <param name="month">The month in which the schedule will be activated (1-12).</param>
        /// <param name="day">The day of month in which the schedule will be activated (1-31).</param>
        /// <param name="hour">The hour in which the schedule will be activated (0-23).</param>
        /// <param name="minute">The minute in which the schedule will be activated (0-59).</param>
        public static string Yearly(int month, int day, int hour, int minute)
        {
            return string.Format("{0} {1} {2} {3} *", new object[4]
            {
         minute,
         hour,
         day,
         month
            });
        }

        /// <summary>
        /// Returns cron expression that fires every &lt;<paramref name="interval"></paramref>&gt; minutes.
        /// </summary>
        /// <param name="interval">The number of minutes to wait between every activation.</param>
        public static string MinuteInterval(int interval)
        {
            return $"*/{interval} * * * *";
        }

        /// <summary>
        /// Returns cron expression that fires every &lt;<paramref name="interval"></paramref>&gt; hours.
        /// </summary>
        /// <param name="interval">The number of hours to wait between every activation.</param>
        public static string HourInterval(int interval)
        {
            return $"0 */{interval} * * *";
        }

        /// <summary>
        /// Returns cron expression that fires every &lt;<paramref name="interval"></paramref>&gt; days.
        /// </summary>
        /// <param name="interval">The number of days to wait between every activation.</param>
        public static string DayInterval(int interval)
        {
            return $"0 0 */{interval} * *";
        }

        /// <summary>
        /// Returns cron expression that fires every &lt;<paramref name="interval"></paramref>&gt; months.
        /// </summary>
        /// <param name="interval">The number of months to wait between every activation.</param>
        public static string MonthInterval(int interval)
        {
            return $"0 0 1 */{interval} *";
        }
    }
}
