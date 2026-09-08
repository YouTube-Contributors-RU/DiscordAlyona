using Bot.Interfaces.Settings;
using Bot.Models.Logger;
using Bot.Utils.Logger;
using Microsoft.AspNetCore.DataProtection;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Bot.Utils.Settings
{
    #region CLASS | Tokens
    /// <summary>
    /// Token class. Implementation of the <see cref="ITokens">ITokens</see> interface.
    /// </summary>
    public class Tokens : ITokens
    {
        private readonly ISettings Settings;
        private readonly LoggerApplicationSettings? LoggerApplicationSettings;

        private readonly IDataProtector Protector;
        private readonly string FilePath;

        #region METHOD-Tokens | Tokens
        /// <summary>
        /// Token class constructor.
        /// </summary>
        /// <param name="settings">Implementation of the settings class (<see cref="ISettings">ISettings</see>).</param>
        /// <param name="applicationSettings">Settings class for the application logger.</param>
        public Tokens(ISettings settings, LoggerApplicationSettings? applicationSettings) 
        {
            Settings = settings;
            LoggerApplicationSettings = applicationSettings;


            string? _applicationNameTemp = Settings.GetApplicationName();
            string _applicationName = _applicationNameTemp ?? "undefined";

            var provider = DataProtectionProvider.Create(_applicationName);
            Protector = provider.CreateProtector("AuthTokenProtection");

            string appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            FilePath = Path.Combine(appData, _applicationName, "token.protected");
        }
        #endregion

        #region METHOD-STRING? | GetToken
        /// <summary>
        /// Obtains the required token.
        /// </summary>
        /// <param name="key">A variable containing a token.</param>
        /// <returns>Token.</returns>
        public string? GetToken(string key)
        {
            try
            {
                var tokens = GetAllTokens();
                string? result = tokens != null && tokens.TryGetValue(key, out var token) ? token : null;
                Log(LogLevel.Debug, result != null ? "The token was successfully received!" : "The get token operation returned a null result.");
                return result;
            }
            catch (Exception ex)
            {
                Log(LogLevel.Error, $"An unexpected error occurred while retrieving the token. More details: {ex.Message}");
                return null; 
            }
        }
        #endregion

        #region METHOD-VOID | SaveToken
        /// <summary>
        /// Saves the token to a variable.
        /// </summary>
        /// <param name="key">Variable.</param>
        /// <param name="token">The token to be written to the variable.</param>
        public void SaveToken(string key, string token)
        {
            try
            {
                var tokens = GetAllTokens() ?? [];
                tokens[key] = token;

                string json = JsonSerializer.Serialize(tokens);
                string protectedData = Protector.Protect(json);

                Directory.CreateDirectory(Path.GetDirectoryName(FilePath)!);
                File.WriteAllText(FilePath, protectedData);

                Log(LogLevel.Debug, "The token was saved successfully!");
            }
            catch (Exception ex)
            {
                Log(LogLevel.Error, $"An unexpected error occurred while saving the token. More details: {ex.Message}");
            }
        }
        #endregion

        #region METHOD-BOOL | RemoveToken
        /// <summary>
        /// Deletes the specified variable.
        /// </summary>
        /// <param name="key">Variable.</param>
        /// <returns>True - if the variable was successfully deleted. Otherwise, False.</returns>
        public bool RemoveToken(string key)
        {
            try
            {
                var tokens = GetAllTokens();
                if (tokens == null || !tokens.Remove(key))
                    return false;

                string json = JsonSerializer.Serialize(tokens);
                string protectedData = Protector.Protect(json);
                File.WriteAllText(FilePath, protectedData);
                Log(LogLevel.Debug, "The token was successfully deleted!");
                return true;
            }
            catch (Exception ex)
            {
                Log(LogLevel.Error, $"An unexpected error occurred while deleting the token. More details: {ex.Message}");
                return false;
            }
        }
        #endregion

        #region (PRIVATE) METHOD-DICTIONARY<STRING,STRING>? | GetAllTokens
        /// <summary>
        /// Gets a dictionary with all tokens.
        /// </summary>
        /// <returns>Dictionary with all tokens.</returns>
        private Dictionary<string, string>? GetAllTokens()
        {
            if (!File.Exists(FilePath))
            {
                Log(LogLevel.Warn, "The get all variable tokens method was called, but the file does not exist.");
                return null;
            }

            try
            {
                string protectedData = File.ReadAllText(FilePath);
                string json = Protector.Unprotect(protectedData);
                Dictionary<string, string>? deserializedJson = JsonSerializer.Deserialize<Dictionary<string, string>>(json);
                Log(LogLevel.Debug, "Variable token data was successfully retrieved!");
                return deserializedJson;
            }
            catch (Exception ex)
            {
                Log(LogLevel.Error, $"An unexpected error occurred while retrieving all variable tokens. More details: {ex.Message}");
                return null;
            }
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
