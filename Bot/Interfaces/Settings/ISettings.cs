namespace Bot.Interfaces.Settings
{
    #region INTERFACE | ISettings
    /// <summary>
    /// Settings class interface.
    /// </summary>
    public interface ISettings
    {
        /// <summary>
        /// Loads settings from a settings file.
        /// </summary>
        public bool Load();

        /// <summary>
        /// Gets the name of the application.
        /// </summary>
        /// <returns>Name of the apllication.</returns>
        public string? GetApplicationName();

        /// <summary>
        /// Gets the value of a dynamic variable.
        /// </summary>
        /// <param name="variable">Dynamic variable.</param>
        /// <returns>Value of a dynamic variable.</returns>
        public string? GetValueOfVariable(string variable);
    }
    #endregion
}
