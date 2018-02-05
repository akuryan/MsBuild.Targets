namespace NewRelic.Synthetics.Api.Monitors
{
    using System.Linq;

    using Microsoft.Build.Framework;

    using NewRelic.Synthetics.Api.Classes;
    using NewRelic.Synthetics.Api.Classes.ApiProxies;
    using NewRelic.Synthetics.Api.Data;

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
            var changeAllMonitors = string.IsNullOrWhiteSpace(MonitorsNamesCollection);

            var monitorRetriver = new GetMonitors(SyntheticsApiKey);
            var monitorUpdater = new UpdateMonitor(SyntheticsApiKey);

            Log.LogMessage("Starting API operations", MessageImportance.Low);

            var monitorsCollection = changeAllMonitors ? monitorRetriver.GetAllMonitors() : GetNamedMonitors(monitorRetriver, MonitorsNamesCollection);

            if (!monitorsCollection.Monitors.Any())
            {
                Log.LogMessage(MessageImportance.High, "Could not retrieve any monitors for given set of parameters");
                return false;
            }

            foreach (var monitor in monitorsCollection.Monitors)
            {
                var updateStatus = monitorUpdater.Execute(monitor, EnableMonitors);
                if (!updateStatus)
                {
                    //failure encountered during monitor update
                    Log.LogError("Could change monitor {0} to status {1}", monitor.Name, EnableMonitors ? Constants.MonitorEnabled : Constants.MonitorDisabled);
                }
            }

            Log.LogMessage("Finished API operations", MessageImportance.Low);

            return true;
        }

        private static SyntheticsJsonRoot GetNamedMonitors(GetMonitors monitorRetriver, string monitorsNamesCollection)
        {
            var monitorNames = monitorsNamesCollection.Split(',');

            var monitorsArray = new Monitor[monitorNames.Length];

            for (var counter = 0; counter < monitorNames.Length; counter++)
            {
                monitorsArray[counter] = monitorRetriver.GetMonitorByName(monitorNames[counter]);
            }

            var monitorsCollection = new SyntheticsJsonRoot
            {
                Count = monitorNames.Length,
                Monitors = monitorsArray
            };

            return monitorsCollection;
        }
    }
}