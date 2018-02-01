namespace NewRelic.Synthetics.Api.Classes
{
    using Newtonsoft.Json;

    public class MonitorBase
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("frequency")]
        public int Frequency { get; set; }

        [JsonProperty("uri")]
        public string Uri { get; set; }

        [JsonProperty("locations")]
        public string[] Locations { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("slaThreshold")]
        public double SlaThreshold { get; set; }
    }
}