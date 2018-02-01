namespace NewRelic.Synthetics.Api.Classes.ApiProxies
{
    using System;
    using System.Net;
    using System.Threading;

    using NewRelic.Synthetics.Api.Data;

    using RestSharp;

    public class UpdateMonitor
    {
        /// <summary>
        /// Content Type header
        /// </summary>
        internal const string ContentTypeHeader = "Content-Type";
        internal const string ContentTypeHeaderValue = "application/json";

        /// <summary>
        /// Base URL which we are calling to work with monitors
        /// </summary>
        private readonly string baseUrl = string.Concat(Constants.SyntheticsApiBaseUrl, Constants.SyntheticsApiMonitors, "/");
        /// <summary>
        /// New Relic Synthetics Admin API key
        /// </summary>
        private readonly string adminApiKey;
        private readonly Classes.Monitor monitor;

        /// <summary>
        /// Working with particular monitor
        /// </summary>
        /// <param name="apiKey"></param>
        /// <param name="monitor"></param>
        /// <param name="enableMonitor"></param>
        public UpdateMonitor(string apiKey, Classes.Monitor monitor, bool enableMonitor)
        {
            this.adminApiKey = apiKey;
            this.monitor = monitor;
            //change monitor status
            this.monitor.Status = enableMonitor ? Constants.MonitorEnabled : Constants.MonitorDisabled;
        }

        public bool Execute(RestRequest request)
        {
            var client = new RestClient
            {
                BaseUrl = new Uri(string.Concat(this.baseUrl, this.monitor.Id))
            };
            request.AddHeader(Constants.ApiKeyHeader, this.adminApiKey);
            request.AddHeader(ContentTypeHeader, ContentTypeHeaderValue);
            request.AddJsonBody(monitor);
            request.Method = Method.PUT;

            var response = client.Execute(request);

            return response.StatusCode.Equals(HttpStatusCode.NoContent);
        }
    }
}