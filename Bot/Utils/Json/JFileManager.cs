using Bot.Models.Logger;
using Bot.Utils.Logger;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace Bot.Utils.Json
{
    #region CLASS | JFileManager<T>
    /// <summary>
    /// A simple class for managing Json files.
    /// </summary>
    /// <typeparam name="T">The class you will be working with in the file.</typeparam>
    public class JFileManager<T> where T : class
    {
        private readonly LoggerApplicationSettings? LoggerApplicationSettings;

        private readonly string FullPathToJsonFile;

        #region METHOD-JFileManager | JFileManager
        /// <summary>
        /// <see cref="JFileManager{T}" /> constructor.
        /// </summary>
        /// <param name="jsonFile">Path to the Json file you are working with.</param>
        /// <param name="applicationSettings">Logger settings for the application.</param>
        public JFileManager(string jsonFile, LoggerApplicationSettings? applicationSettings)
        {
            FullPathToJsonFile = GetFullPathToJsonFile(jsonFile);

            LoggerApplicationSettings = applicationSettings;

            InitDirectory(FullPathToJsonFile);
        }
        #endregion

        #region METHOD-VOID | Write
        /// <summary>
        /// Writes data to a Json file.
        /// </summary>
        /// <param name="data">Input data.</param>
        public void Write(T data)
        {
            try
            {
                string serializedJsonData = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(FullPathToJsonFile, serializedJsonData);
                Log(LogLevel.Debug, $"The data in file `{FullPathToJsonFile}` was successfully overwritten!");
            }
            catch (Exception ex)
            {
                Log(LogLevel.Error, $"An unexpected error occurred while overwriting file '{FullPathToJsonFile}'. More details: {ex.Message}");
            }
        }
        #endregion

        #region METHOD-<T> | Read
        /// <summary>
        /// Reads data from a Json file.
        /// </summary>
        /// <returns>Output data.</returns>
        public T? Read()
        {
            try
            {
                if (!File.Exists(FullPathToJsonFile))
                {
                    Log(LogLevel.Error, $"The file `{FullPathToJsonFile}` was not found.");
                    return null;
                }

                string data = File.ReadAllText(FullPathToJsonFile);
                T? deserializedJsonData = JsonSerializer.Deserialize<T>(data);
                Log(LogLevel.Debug, $"Data from file `{FullPathToJsonFile}` was successfully retrieved!");
                return deserializedJsonData;
            }
            catch (Exception ex)
            {
                Log(LogLevel.Error, $"An unexpected error occurred while reading data from file '{FullPathToJsonFile}'. More details: {ex.Message}");
                return null;
            }
        }
        #endregion

        #region METHOD-VOID | Delete
        /// <summary>
        /// Deletes the Json file.
        /// </summary>
        public void Delete()
        {
            try
            {
                File.Delete(FullPathToJsonFile);
                Log(LogLevel.Debug, $"File `{FullPathToJsonFile}` successfully deleted!");
            }
            catch (Exception ex)
            {
                Log(LogLevel.Error, $"An unexpected error occurred while deleting the file '{FullPathToJsonFile}'. More details: {ex.Message}");
            }
        }
        #endregion

        #region (PRIVATE) METHOD-STRING | GetFullPathToJsonFile
        /// <summary>
        /// Converts a path to a full path.
        /// </summary>
        /// <param name="jsonFile">Path.</param>
        /// <returns>Full path.</returns>
        private string GetFullPathToJsonFile(string jsonFile) => Path.GetFullPath(jsonFile);
        #endregion

        #region (PRIVATE) METHOD-VOID | InitDirectory
        /// <summary>
        /// Initializes the directory if it has not been created.
        /// </summary>
        /// <param name="fullPathToJsonFile">Full path to the Json file.</param>
        private void InitDirectory(string fullPathToJsonFile)
        {
            string? directoryPath = Path.GetDirectoryName(fullPathToJsonFile);
            if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                Directory.CreateDirectory(directoryPath);
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
