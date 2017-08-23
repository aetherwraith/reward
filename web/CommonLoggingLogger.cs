using System;
using Common.Logging;

namespace web
{
        /// <summary>   A common logging logger. </summary>
    public class CommonLoggingLogger
    {
        /// <summary>   The singelton instance of the logger. </summary>
        private static CommonLoggingLogger _instance;

        /// <summary>   The logger implementation. </summary>
        private readonly ILog _loggerImplementation;

        /// <summary>   Default constructor. </summary>
        private CommonLoggingLogger()
        {
            _loggerImplementation = LogManager.GetLogger(GetType());
        }

        /// <summary>   The public method to get the instance. </summary>
        public static CommonLoggingLogger Instance => _instance ?? (_instance = new CommonLoggingLogger());

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Log a Debug message. </summary>
        /// <param name="message">  The message. </param>
        /// <param name="memberName">The calling method.</param>
        /// -------------------------------------------------------------------------------------------------
        public  void Debug(string message, string memberName = "")
        {
            _loggerImplementation.Debug($"[{memberName}] " + message);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Log a Debug message. </summary>
        /// <param name="message">      The message. </param>
        /// <param name="exception">    The exception. </param>
        /// <param name="memberName">The calling method.</param>
        /// -------------------------------------------------------------------------------------------------
        public  void Debug(string message, Exception exception, string memberName = "")
        {
            _loggerImplementation.Debug($"[{memberName}] " + message, exception);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Log an Error message. </summary>
        /// <param name="message">  The message. </param>
        /// <param name="memberName">The calling method.</param>
        /// -------------------------------------------------------------------------------------------------
        public  void Error(string message, string memberName = "")
        {
            _loggerImplementation.Error($"[{memberName}] " + message);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Log an Error message. </summary>
        /// <param name="message">      The message. </param>
        /// <param name="exception">    The exception. </param>
        /// <param name="memberName">The calling method.</param>
        /// -------------------------------------------------------------------------------------------------
        public  void Error(string message, Exception exception, string memberName = "")
        {
            _loggerImplementation.Error($"[{memberName}] " + message, exception);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Log a Fatal error. </summary>
        /// <param name="message">  The message. </param>
        /// <param name="memberName">The calling method.</param>
        /// -------------------------------------------------------------------------------------------------
        public  void Fatal(string message, string memberName = "")
        {
            _loggerImplementation.Fatal($"[{memberName}] " + message);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Log a Fatal error. </summary>
        /// <param name="message">      The message. </param>
        /// <param name="exception">    The exception. </param>
        /// <param name="memberName">The calling method.</param>
        /// -------------------------------------------------------------------------------------------------
        public  void Fatal(string message, Exception exception, string memberName = "")
        {
            _loggerImplementation.Fatal($"[{memberName}] " + message, exception);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Log an Info message. </summary>
        /// <param name="message">  The message. </param>
        /// <param name="memberName">The calling method.</param>
        /// -------------------------------------------------------------------------------------------------
        public  void Info(string message, string memberName = "")
        {
            _loggerImplementation.Info($"[{memberName}] " + message);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Log an Info message. </summary>
        /// <param name="message">      The message. </param>
        /// <param name="exception">    The exception. </param>
        /// <param name="memberName">The calling method.</param>
        /// -------------------------------------------------------------------------------------------------
        public  void Info(string message, Exception exception, string memberName = "")
        {
            _loggerImplementation.Info($"[{memberName}] " + message, exception);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Log a Trace messge. </summary>
        /// <param name="message">  The message. </param>
        /// <param name="memberName">The calling method.</param>
        /// -------------------------------------------------------------------------------------------------
        public  void Trace(string message, string memberName = "")
        {
            _loggerImplementation.Trace($"[{memberName}] " + message);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Log a Trace message. </summary>
        /// <param name="message">      The message. </param>
        /// <param name="exception">    The exception. </param>
        /// <param name="memberName">The calling method.</param>
        /// -------------------------------------------------------------------------------------------------
        public  void Trace(string message, Exception exception, string memberName = "")
        {
            _loggerImplementation.Trace($"[{memberName}] " + message, exception);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Log a Warning message. </summary>
        /// <param name="message">  The message. </param>
        /// <param name="memberName">The calling method.</param>
        /// -------------------------------------------------------------------------------------------------
        public  void Warn(string message, string memberName = "")
        {
            _loggerImplementation.Warn($"[{memberName}] " + message);
        }

        /// -------------------------------------------------------------------------------------------------
        /// <summary>   Log a Warning message. </summary>
        /// <param name="message">      The message. </param>
        /// <param name="exception">    The exception. </param>
        /// <param name="memberName">The calling method.</param>
        /// -------------------------------------------------------------------------------------------------
        public  void Warn(string message, Exception exception, string memberName = "")
        {
            _loggerImplementation.Warn($"[{memberName}] " + message, exception);
        }
    }
}