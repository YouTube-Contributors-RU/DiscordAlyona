//== Copyright (C) 2026, YouTube Contributors and dmitriykotik. ==
// Released under the MIT License.
// 
// This file is part of YouTube-Contributors-RU/DiscordBot.
// This software is provided "AS IS", without warranty of any kind,
// express or implied, including but not limited to warranties
// of merchantability, fitness for a particular purpose and
// noninfringement.

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
