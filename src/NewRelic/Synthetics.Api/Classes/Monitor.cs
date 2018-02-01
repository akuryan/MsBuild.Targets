using System;

namespace NewRelic.Synthetics.Api.Classes
{
    using RestSharp.Deserializers;

    public class Monitor
    {
        [DeserializeAs(Name = "id")]
        public string Id { get; set; }
        [DeserializeAs(Name = "name")]
        public string Name { get; set; }
        [DeserializeAs(Name = "type")]
        public string Type { get; set; }
        [DeserializeAs(Name = "frequency")]
        public int Frequency { get; set; }
        [DeserializeAs(Name = "uri")]
        public string Uri { get; set; }
        [DeserializeAs(Name = "locations")]
        public string[] Locations { get; set; }
        [DeserializeAs(Name = "status")]
        public string Status { get; set; }
        [DeserializeAs(Name = "slaThreshold")]
        public double SlaThreshold { get; set; }
        [DeserializeAs(Name = "options")]
        public Options Options { get; set; }
        [DeserializeAs(Name = "modifiedAt")]
        public DateTime ModifiedAt { get; set; }
        [DeserializeAs(Name = "createdAt")]
        public DateTime CreatedAt { get; set; }
        [DeserializeAs(Name = "userId")]
        public int UserId { get; set; }
        [DeserializeAs(Name = "apiVersion")]
        public string ApiVersion { get; set; }
    }
}
