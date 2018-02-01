namespace NewRelic.Synthetics.Api.Data
{
    public static class Constants
    {
        /// <summary>
        /// Base URL for New Relic Synthetics API
        /// </summary>
        internal const string SyntheticsApiBaseUrl = "https://synthetics.newrelic.com/synthetics/api/v3/";
        /// <summary>
        /// New Relic Synthetics API monitors endpoint
        /// </summary>
        internal const string SyntheticsApiMonitors = "monitors";
        /// <summary>
        /// Contains header name 
        /// </summary>
        internal const string ApiKeyHeader = "X-Api-Key";
        internal const string MonitorEnabled = "enabled";
        internal const string MonitorDisabled = "disabled";
    }
}