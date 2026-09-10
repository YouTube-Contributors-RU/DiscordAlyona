using Bot.Interfaces.Logger;
using Bot.Interfaces.Settings;

namespace Bot.Internal
{
    #region CLASS | CommandsSettingsModel
    /// <summary>
    /// A model for conveying the implementation of interfaces required by the internal command manager.
    /// </summary>
    internal class CommandsSettingsModel
    {
        /// <summary>
        /// Logger (<see cref="ILogger" />)
        /// </summary>
        public ILogger? Logger { get; set; }

        /// <summary>
        /// Settings (<see cref="ISettings" />)
        /// </summary>
        public ISettings? Settings { get; set; }

        /// <summary>
        /// Tokens (<see cref="ITokens" />)
        /// </summary>
        public ITokens? Tokens { get; set; }
    }
    #endregion
}
