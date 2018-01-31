namespace NewRelic.Synthetics.Api.Classes.ApiProxies
{
    using System;
    using System.Collections.Generic;
    using System.Threading;

    using NewRelic.Synthetics.Api.Data;

    using RestSharp;

    public class GetAllMonitors
    {
        /// <summary>
        /// Base URL which we are calling to work with monitors
        /// </summary>
        private readonly string baseUrl = string.Concat(Constants.SyntheticsApiBaseUrl, Constants.SyntheticsApiMonitors);
        /// <summary>
        /// New Relic Synthetics Admin API key
        /// </summary>
        private readonly string adminApiKey;
        /// <summary>
        /// Working with particular monitor
        /// </summary>
        /// <param name="apiKey"></param>
        public GetAllMonitors(string apiKey)
        {
            this.adminApiKey = apiKey;
        }

        public T Execute<T>(RestRequest request) where T : new()
        {
            var client = new RestClient
            {
                BaseUrl = new Uri(this.baseUrl)
            };
            request.AddHeader(Constants.ApiKeyHeader, this.adminApiKey);

            var response = client.Execute<T>(request);

            while (response.StatusCode.Equals(429))
            {
                //we hit rate limit and must wait for 1 second
                Thread.Sleep(1000);
                response = client.Execute<T>(request);
            }

            return response.Data;
        }

        public List<Monitors> GetMonitors()
        {
            var request = new RestRequest();
            return Execute<List<Monitors>>(request);
        }
    }
}