//== Copyright (C) 2026, YouTube Contributors and dmitriykotik. ==
// Released under the MIT License.
// 
// This file is part of DiscordAlyona.
// This software is provided "AS IS", without warranty of any kind,
// express or implied, including but not limited to warranties
// of merchantability, fitness for a particular purpose and
// noninfringement.

using Bot.Interfaces.Logger;
using Bot.Utils.Logger;

namespace Bot.Models.Logger
{
    #region CLASS | LoggerApplicationSettings
    /// <summary>
    /// A class containing settings for simply passing a logger to SOME class.
    /// </summary>
    public class LoggerApplicationSettings
    {
        /// <summary>
        /// Logger.
        /// </summary>
        public required ILogger Logger { get; set; }

        /// <summary>
        /// Application id.
        /// </summary>
        public int ApplicationId { get; set; }

        /// <summary>
        /// Log destination.
        /// </summary>
        public required LogDestination LogDestination { get; set; }
    }
    #endregion
}
