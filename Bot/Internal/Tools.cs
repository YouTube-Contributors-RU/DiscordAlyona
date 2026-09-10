//== Copyright (C) 2026, YouTube Contributors and dmitriykotik. ==
// Released under the MIT License.
// 
// This file is part of DiscordAlyona.
// This software is provided "AS IS", without warranty of any kind,
// express or implied, including but not limited to warranties
// of merchantability, fitness for a particular purpose and
// noninfringement.

using Bot.Interfaces.Logger;
using Bot.Models.Logger;
using Bot.Utils.Logger;

namespace Bot.Internal
{
    #region CLASS | Tools
    /// <summary>
    /// Internal project tools.
    /// </summary>
    public class Tools
    {
        /// <summary>
        /// Obtaining the log file address by application ID.
        /// </summary>
        /// <param name="id">Application Id.</param>
        /// <returns>Log file path.</returns>
        public static string GetLogPath(ApplicationIds id)
        {
            string logsDir = "logs/";
            string logExtension = ".log";
            return logsDir + id.ToString().ToLower() + logExtension;
        }

        /// <summary>
        /// Gets information about an application by its application Id.
        /// </summary>
        /// <param name="id">Application Id.</param>
        /// <returns>Logger application.</returns>
        public static LoggerApplication GetLoggerApplication(ApplicationIds id) 
            => new() { Name = id.ToString(), FileName = GetLogPath(id), Description = $"{id} logger." };

        /// <summary>
        /// Creates and returns a logging settings model for the application.
        /// </summary>
        /// <param name="logger">Logger.</param>
        /// <param name="id">Application Id.</param>
        /// <param name="logDestination">Log destination.</param>
        /// <returns>Logger application settings.</returns>
        public static LoggerApplicationSettings GetLoggerApplicationSettings(ILogger logger, ApplicationIds id, LogDestination logDestination = LogDestination.Both)
            => new() { Logger = logger, ApplicationId = (int)id, LogDestination = logDestination };
    }
    #endregion
}
