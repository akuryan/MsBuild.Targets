namespace NewRelic.Synthetics.Api.Classes.ApiProxies
{
    using System;
    using System.Net;
    using System.Threading;

    using NewRelic.Synthetics.Api.Classes.Serialization;
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

        /// <summary>
        /// Working with particular monitor
        /// </summary>
        /// <param name="apiKey"></param>
        public UpdateMonitor(string apiKey)
        {
            this.adminApiKey = apiKey;
        }
        /// <summary>
        /// Tries to change Synthetics monitor status 
        /// </summary>
        /// <param name="monitor"><see cref="SyntheticsJsonRoot.Monitors"/> class</param>
        /// <param name="enableMonitor">Boolean to which status we are moving monitor</param>
        /// <returns></returns>
        public bool Execute(Classes.Monitor monitor, bool enableMonitor)
        {
            var request = new RestRequest();
            var client = new RestClient
            {
                BaseUrl = new Uri(string.Concat(this.baseUrl, monitor.Id))
            };
            //change monitor status
            monitor.Status = enableMonitor ? Constants.MonitorEnabled : Constants.MonitorDisabled;
            request.AddHeader(Constants.ApiKeyHeader, this.adminApiKey);
            request.AddHeader(ContentTypeHeader, ContentTypeHeaderValue);
            request.JsonSerializer = NewtonsoftJsonSerializer.Default;
            request.AddJsonBody(monitor);
            request.Method = Method.PUT;

            var response = client.Execute(request);
            while (response.StatusCode.Equals(429))
            {
                //we hit rate limit and must wait for 1 second
                Thread.Sleep(1000);
                response = client.Execute(request);
            }

            return response.StatusCode.Equals(HttpStatusCode.NoContent);
        }
    }
}