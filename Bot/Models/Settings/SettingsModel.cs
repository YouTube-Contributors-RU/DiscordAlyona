namespace Bot.Models.Settings
{
    #region CLASS | SettingsModel
    /// <summary>
    /// Settings model.
    /// </summary>
    public class SettingsModel
    {
        /// <summary>
        /// Application name.
        /// </summary>
        public required string ApplicationName { get; set; }

        /// <summary>
        /// Dynamic variables.
        /// </summary>
        public Dictionary<string, string>? DynamicVariables { get; set; }
    }
    #endregion
}
