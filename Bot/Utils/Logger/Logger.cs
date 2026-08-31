using Bot.Models;
using System.Collections.Concurrent;
using System.Runtime.CompilerServices;

namespace Bot.Utils.Logger
{
    public class Logger(IDictionary<int, LoggerApplication> models)
    {
        private readonly IReadOnlyDictionary<int, LoggerApplication> _models = new Dictionary<int, LoggerApplication>(models);
        private readonly ConcurrentDictionary<string, object> _fileLocks = new ConcurrentDictionary<string, object>();

        public void Log(int id, LogLevel logLevel, string message, LogDestination logDestination = LogDestination.Both, [CallerMemberName] string callerName = "undefined")
        {
            if (!_models.TryGetValue(id, out var model))
                throw new ArgumentException($"The application with ID `{id}` was not registered.");

            string formattedMessage = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{logLevel}] [{callerName}] [{model.Name}] {message}";

            if (logDestination == LogDestination.Console || logDestination == LogDestination.Both)
                WriteToConsole(logLevel, formattedMessage);

            if (logDestination == LogDestination.File || logDestination == LogDestination.Both)
                WriteToFile(model.FileName, formattedMessage);
        }

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

        private void WriteToConsole(LogLevel logLevel, string message)
        {
            lock (Console.Out)
            {
                SetConsoleColor(logLevel);
                Console.WriteLine(message);
                Console.Clear();
            }
        }

        private void WriteToFile(string fileName, string message)
        {
            var fileLock = _fileLocks.GetOrAdd(fileName, _ => new object());

            lock (fileLock)
            {
                try
                {
                    File.AppendAllText(fileName, message + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    lock (Console.Out)
                    {
                        SetConsoleColor(LogLevel.Fatal);
                        Console.WriteLine($"[Logger] Failed to write to file `{fileName}`. More details: {ex.Message}");
                    }
                }
            }
        }
    }
}
