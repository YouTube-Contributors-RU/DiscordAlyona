//== Copyright (C) 2026, YouTube Contributors and dmitriykotik. ==
// Released under the MIT License.
// 
// This file is part of YouTube-Contributors-RU/DiscordBot.
// This software is provided "AS IS", without warranty of any kind,
// express or implied, including but not limited to warranties
// of merchantability, fitness for a particular purpose and
// noninfringement.

namespace Bot.Models.Settings
{
    #region CLASS | SettingsModel
    /// <summary>
    /// Settings model.
    /// </summary>
    public class SettingsModel
    {
        /// <summary>
        /// Application name.
        /// </summary>
        public required string ApplicationName { get; set; }

        /// <summary>
        /// Dynamic variables.
        /// </summary>
        public Dictionary<string, string>? DynamicVariables { get; set; }
    }
    #endregion
}
