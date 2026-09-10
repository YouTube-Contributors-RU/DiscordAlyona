using Bot.Models.Logger;
using Bot.Utils.Logger;
using System.Runtime.CompilerServices;

namespace Bot.Macros.Logger
{
    #region CLASS | LoggerMacros
    /// <summary>
    /// Logger macros.
    /// </summary>
    /// <param name="loggerApplicationSettings">Settings for application logging.</param>
    public class LoggerMacros(LoggerApplicationSettings? loggerApplicationSettings)
    {
        private readonly LoggerApplicationSettings? LoggerApplicationSettings = loggerApplicationSettings;

        #region METHOD-VOID | Log
        /// <summary>
        /// Method for logging.
        /// </summary>
        /// <param name="logLevel">Logging level.</param>
        /// <param name="message">Message.</param>
        /// <param name="callerName">The method that called the log.</param>
        public void Log(LogLevel logLevel, string message, [CallerMemberName] string callerName = "undefined")
        {
            if (LoggerApplicationSettings == null)
                return;

            var Logger = LoggerApplicationSettings.Logger;

            Logger.Log(LoggerApplicationSettings.ApplicationId, logLevel, message, LoggerApplicationSettings.LogDestination, callerName);
        }
        #endregion
    }
    #endregion
}
