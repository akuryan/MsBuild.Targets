namespace NewRelic.Synthetics.Api.Monitors
{
    using Microsoft.Build.Framework;

    /// <summary>
    /// Gets all NewRelic Synthetics monitors and enables/disables them
    /// </summary>
    public class UpdateMonitorStatus : Microsoft.Build.Utilities.Task
    {
        /// <summary>
        /// Admin User key for NewRelic Synthetics
        /// </summary>
        [Required]
        public string SyntheticsApiKey { get; set; }

        /// <summary>
        /// Defines, if monitors shall be enables or disabled
        /// </summary>
        [Required]
        public bool EnableMonitors { get; set; }

        /// <summary>
        /// If defined, we will work only on those monitors. Shall contain comma-separated string
        /// </summary>
        public string MonitorsNamesCollection { get; set; }

        public override bool Execute()
        {
            
        }
    }
}