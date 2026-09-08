namespace Bot.Models.Logger
{
    #region CLASS | LoggerApplication
    /// <summary>
    /// Class containing application data.
    /// </summary>
    public class LoggerApplication
    {
        /// <summary>
        /// Application name.
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// The file in which the application logs will be saved.
        /// </summary>
        public required string FileName { get; set; }

        /// <summary>
        /// Description of the application.
        /// </summary>
        public string? Description { get; set; }
    }
    #endregion
}
