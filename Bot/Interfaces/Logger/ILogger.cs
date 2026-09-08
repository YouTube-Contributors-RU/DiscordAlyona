using Bot.Utils.Logger;
using System.Runtime.CompilerServices;

namespace Bot.Interfaces.Logger
{
    #region INTERFACE | ILogger
    /// <summary>
    /// This is the logger interface. Indeed.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// General method for outputting logs.
        /// </summary>
        /// <param name="id">Application ID.</param>
        /// <param name="logLevel">Log level.</param>
        /// <param name="message">Message.</param>
        /// <param name="logDestination">Log saving locations.</param>
        /// <param name="callerName">The method that called the log.</param>
        void Log(int id, LogLevel logLevel, string message, LogDestination logDestination, [CallerMemberName] string callerName = "undefined");
    }
    #endregion
}
