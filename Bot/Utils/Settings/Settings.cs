using Bot.Interfaces.Settings;
using Bot.Models.Logger;
using Bot.Models.Settings;
using Bot.Utils.Json;
using Bot.Utils.Logger;
using System.Runtime.CompilerServices;

namespace Bot.Utils.Settings
{
    #region CLASS | Settings
    /// <summary>
    /// Settings class. Implementation of the <see cref="ISettings">ISettings</see> interface.
    /// </summary>
    public class Settings : ISettings
    {
        LoggerApplicationSettings? LoggerApplicationSettings;
        private readonly JFileManager<SettingsModel> JFileManager;
        private SettingsModel? SettingsModel = null;

        #region METHOD-Settings | Settings
        /// <summary>
        /// Settings class constructor.
        /// </summary>
        /// <param name="settingsFile">Settings file.</param>
        /// <param name="applicationSettings">Settings class for the application logger.</param>
        /// <param name="jFileManagerLoggerSettings">Settings class for the <see cref="JFileManager">JFileManager</see> application logger.</param>
        public Settings(string settingsFile, LoggerApplicationSettings? applicationSettings, LoggerApplicationSettings? jFileManagerLoggerSettings)
        {
            LoggerApplicationSettings = applicationSettings;

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
                SettingsModel = JFileManager.Read();
                if (SettingsModel == null)
                    Log(LogLevel.Warn, "Reading the settings file failed because it contains a null value. Please check the settings file.");
                else
                    Log(LogLevel.Debug, "The settings file has been loaded successfully!");

                return SettingsModel != null;
            }
            catch (Exception ex)
            {
                Log(LogLevel.Error, $"An unexpected error occurred while loading the settings file. More details: {ex.Message}");
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

        #region (PRIVATE) METHOD-VOID | Log
        /// <summary>
        /// Private method for logging.
        /// </summary>
        /// <param name="logLevel">Logging level.</param>
        /// <param name="message">Message.</param>
        /// <param name="callerName">The method that called the log.</param>
        private void Log(LogLevel logLevel, string message, [CallerMemberName] string callerName = "undefined")
        {
            if (LoggerApplicationSettings == null)
                return;

            var _logger = LoggerApplicationSettings.Logger;

            _logger.Log(LoggerApplicationSettings.ApplicationId, logLevel, message, LoggerApplicationSettings.LogDestination, callerName);
        }
        #endregion
    }
    #endregion
}
