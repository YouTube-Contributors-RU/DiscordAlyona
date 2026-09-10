using Bot.Interfaces.Logger;
using Bot.Models.Logger;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Bot.Utils.Logger
{
    #region Class | Logger
    // Logger. Contains instructions for writing logs to the console and/or to a file.
    // This is an implementation of the ILogger interface. Where it is, though... I have no idea.

    /// <summary>
    /// <see cref="Logger" /> class constructor.
    /// This is an implementation of the <see cref="ILogger" /> interface. Where it is, though... I have no idea.
    /// </summary>
    /// <param name="applications">Dictionary with index and application model.</param>
    public class Logger(IDictionary<int, LoggerApplication> applications) : ILogger
    {
        private readonly IReadOnlyDictionary<int, LoggerApplication> _applications = new Dictionary<int, LoggerApplication>(applications);
        private readonly ConcurrentDictionary<string, object> _fileLocks = new ConcurrentDictionary<string, object>();

        #region METHOD-VOID | Log
        /// <summary>
        /// Write log to console and/or file.
        /// </summary>
        /// <param name="id">Application ID.</param>
        /// <param name="logLevel">Log level.</param>
        /// <param name="message">Message.</param>
        /// <param name="logDestination">Log saving locations.</param>
        /// <param name="callerName">The method that called the log.</param>
        public void Log(int id, LogLevel logLevel, string message, LogDestination logDestination = LogDestination.Both, [CallerMemberName] string callerName = "undefined")
        {
            LoggerApplication? applicationModel;
            if (!_applications.TryGetValue(id, out applicationModel))
                applicationModel = new() { Name = "undefined", FileName = "logs/undefined.log", Description = "The logger does not have an application configured with the specified ID." };

            string formattedMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{logLevel}] [{callerName}] [{applicationModel.Name}] {message}";

            if (logDestination is LogDestination.Console or LogDestination.Both)
                WriteToConsole(logLevel, formattedMessage);

            if (logDestination is LogDestination.File or LogDestination.Both)
                WriteToFile(applicationModel.FileName, formattedMessage);
        }
        #endregion

        #region (PRIVATE) METHOD-VOID | SetConsoleColor
        /// <summary>
        /// Sets the console color based on the log level.
        /// </summary>
        /// <param name="level">Log level.</param>
        private void SetConsoleColor(LogLevel level)
        {
            Console.ForegroundColor = level switch
            {
                LogLevel.Trace => ConsoleColor.DarkGray,
                LogLevel.Debug => ConsoleColor.DarkGray,
                LogLevel.Info => ConsoleColor.Cyan,
                LogLevel.Warn => ConsoleColor.DarkYellow,
                LogLevel.Error => ConsoleColor.Red,
                LogLevel.Fatal => ConsoleColor.DarkRed,
                _ => ConsoleColor.White
            };
        }
        #endregion

        #region (PRIVATE) METHOD-VOID | WriteToConsole
        /// <summary>
        /// Writing text to the console.
        /// </summary>
        /// <param name="logLevel">Log level.</param>
        /// <param name="message">Message.</param>
        private void WriteToConsole(LogLevel logLevel, string message)
        {
            lock (Console.Out)
            {
                SetConsoleColor(logLevel);
                Console.WriteLine(message);
                Console.ResetColor();
            }
        }
        #endregion

        #region (PRIVATE) METHOD-VOID | WriteToFile
        /// <summary>
        /// Writing text to the file.
        /// </summary>
        /// <param name="fileName">File name.</param>
        /// <param name="message"> Message.</param>
        private void WriteToFile(string fileName, string message)
        {
            string fullPath = Path.GetFullPath(fileName);
            var fileLock = _fileLocks.GetOrAdd(fullPath, _ => new object());

            lock (fileLock)
            {
                try
                {
                    string? directory = Path.GetDirectoryName(fullPath);
                    if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                    {
                        Directory.CreateDirectory(directory);
                    }

                    File.AppendAllText(fullPath, message + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    WriteToConsole(LogLevel.Fatal, $"[Logger] Failed to write to file `{fileName}`. More details: {ex.Message}");
                }
            }
        }
        #endregion
    }
    #endregion
}
