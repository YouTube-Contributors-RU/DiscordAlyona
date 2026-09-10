//== Copyright (C) 2026, YouTube Contributors and dmitriykotik. ==
// Released under the MIT License.
// 
// This file is part of DiscordAlyona.
// This software is provided "AS IS", without warranty of any kind,
// express or implied, including but not limited to warranties
// of merchantability, fitness for a particular purpose and
// noninfringement.

namespace Bot.Models.Logger
{
    #region CLASS | LoggerApplication
    /// <summary>
    /// Class containing application data.
    /// </summary>
    public class LoggerApplication
    {
        /// <summary>
        /// Application name.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// The file in which the application logs will be saved.
        /// </summary>
        public required string FileName { get; set; }

        /// <summary>
        /// Description of the application.
        /// </summary>
        public string? Description { get; set; }
    }
    #endregion
}
