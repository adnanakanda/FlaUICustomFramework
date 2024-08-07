using NLog;
using System;

namespace Framework.Utils
{
    public static class Logger
    {
        public enum LogLevel
        {
            Info,
            Debug,
            Error
        }

        static Logger()
        {
            LogManager.LoadConfiguration("NLog.config");
        }

        private static readonly NLog.Logger logger = LogManager.GetCurrentClassLogger();

        public static void Info(string message, Exception ex = null)
        {
            if (ex == null)
            {
                logger.Info(message);
            }
            else
            {
                logger.Info(ex, message);
            }
        }

        public static void Debug(string message, Exception ex = null)
        {
            if (ex == null)
            {
                logger.Debug(message);
            }
            else
            {
                logger.Debug(ex, message);
            }
        }

        public static void Error(string message, Exception ex = null)
        {
            if (ex == null)
            {
                logger.Error(message);
            }
            else
            {
                logger.Error(ex, message);
            }
        }

        public static void LogMessage(LogLevel level, string message, Exception ex = null)
        {
            switch (level)
            {
                case LogLevel.Info:
                    Info(message, ex);
                    break;
                case LogLevel.Debug:
                    Debug(message, ex);
                    break;
                case LogLevel.Error:
                    Error(message, ex);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(level), level, null);
            }
        }
    }
}
