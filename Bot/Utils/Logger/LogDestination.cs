namespace Bot.Utils.Logger
{
    #region ENUM | LogDestination
    /// <summary>
    /// Log Destination
    /// </summary>
    public enum LogDestination
    {
        /// <summary>
        /// Writing log to console only.
        /// </summary>
        Console,

        /// <summary>
        /// Writing log to file only.
        /// </summary>
        File,

        /// <summary>
        /// Writing log to console and file.
        /// </summary>
        Both
    }
    #endregion
}
