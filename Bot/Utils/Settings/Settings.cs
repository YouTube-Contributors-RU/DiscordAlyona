//== Copyright (C) 2026, YouTube Contributors and dmitriykotik. ==
// Released under the MIT License.
// 
// This file is part of DiscordAlyona.
// This software is provided "AS IS", without warranty of any kind,
// express or implied, including but not limited to warranties
// of merchantability, fitness for a particular purpose and
// noninfringement.

using Bot.Interfaces.Settings;
using Bot.Macros.Logger;
using Bot.Models.Logger;
using Bot.Models.Settings;
using Bot.Utils.Json;
using Bot.Utils.Logger;

namespace Bot.Utils.Settings
{
    #region CLASS | Settings
    /// <summary>
    /// Settings class. Implementation of the <see cref="ISettings">ISettings</see> interface.
    /// </summary>
    public class Settings : ISettings
    {
        private readonly string SettingsFile;
        private readonly LoggerMacros? Logger;
        private readonly JFileManager<SettingsModel> JFileManager;
        private SettingsModel? SettingsModel = null;

        #region METHOD-Settings | Settings
        /// <summary>
        /// Settings class constructor.
        /// </summary>
        /// <param name="settingsFile">Settings file.</param>
        /// <param name="applicationSettings">Settings class for the application logger.</param>
        /// <param name="jFileManagerLoggerSettings">Settings class for the <see cref="JFileManager" /> application logger.</param>
        public Settings(string settingsFile, LoggerApplicationSettings? applicationSettings, LoggerApplicationSettings? jFileManagerLoggerSettings)
        {
            SettingsFile = settingsFile;

            Logger = new(applicationSettings);
            JFileManager = new JFileManager<SettingsModel>(settingsFile, jFileManagerLoggerSettings);
            Load();
        }
        #endregion

        #region METHOD-BOOL | Load
        /// <summary>
        /// Loads the settings file.
        /// </summary>
        /// <returns>True - if the download was successful and deserialization did not return null. Otherwise, False.</returns>
        public bool Load()
        {
            try
            {
                if (!File.Exists(SettingsFile))
                    JFileManager.Write(GenerateSampleModel());

                SettingsModel = JFileManager.Read();
                if (SettingsModel == null)
                    Logger?.Log(LogLevel.Warn, "Reading the settings file failed because it contains a null value. Please check the settings file.");
                else
                    Logger?.Log(LogLevel.Debug, "The settings file has been loaded successfully!");

                return SettingsModel != null;
            }
            catch (Exception ex)
            {
                Logger?.Log(LogLevel.Error, $"An unexpected error occurred while loading the settings file. More details: {ex.Message}");
                return false;
            }
        }
        #endregion

        #region METHOD-STRING? | GetApplicationName
        /// <summary>
        /// Gets the value of the application name variable.
        /// </summary>
        /// <returns>Value of the application name variable.</returns>
        public string? GetApplicationName() => SettingsModel?.ApplicationName;
        #endregion

        #region METHOD-STRING? | GetValueOfVariable
        /// <summary>
        /// Gets the value of a dynamic variable.
        /// </summary>
        /// <param name="variable">Dynamic variable.</param>
        /// <returns>Value of a dynamic variable.</returns>
        public string? GetValueOfVariable(string variable)
        {
            string? value = null;
            SettingsModel?.DynamicVariables?.TryGetValue(variable, out value);
            return value;
        }
        #endregion

        private SettingsModel GenerateSampleModel()
            => new()
            {
                ApplicationName = "Application1",
                DynamicVariables = new() { { "TestVariable", "TestValue" } }
            };
    }
    #endregion
}
