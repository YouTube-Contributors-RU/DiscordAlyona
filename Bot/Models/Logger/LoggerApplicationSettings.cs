using Bot.Interfaces.Logger;
using Bot.Utils.Logger;

namespace Bot.Models.Logger
{
    #region CLASS | LoggerApplicationSettings
    /// <summary>
    /// A class containing settings for simply passing a logger to SOME class.
    /// </summary>
    public class LoggerApplicationSettings
    {
        /// <summary>
        /// Logger.
        /// </summary>
        public required ILogger Logger { get; set; }

        /// <summary>
        /// Application id.
        /// </summary>
        public int ApplicationId { get; set; }

        /// <summary>
        /// Log destination.
        /// </summary>
        public required LogDestination LogDestination { get; set; }
    }
    #endregion
}
