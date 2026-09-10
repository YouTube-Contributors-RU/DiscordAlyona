//== Copyright (C) 2026, YouTube Contributors and dmitriykotik. ==
// Released under the MIT License.
// 
// This file is part of DiscordAlyona.
// This software is provided "AS IS", without warranty of any kind,
// express or implied, including but not limited to warranties
// of merchantability, fitness for a particular purpose and
// noninfringement.

namespace Bot.Utils.Logger
{
    #region ENUM | LogDestination
    /// <summary>
    /// Log Destination
    /// </summary>
    public enum LogDestination
    {
        /// <summary>
        /// Writing log to console only.
        /// </summary>
        Console,

        /// <summary>
        /// Writing log to file only.
        /// </summary>
        File,

        /// <summary>
        /// Writing log to console and file.
        /// </summary>
        Both
    }
    #endregion
}
