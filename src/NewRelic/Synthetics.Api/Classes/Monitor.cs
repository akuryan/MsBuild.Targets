using System;

namespace NewRelic.Synthetics.Api.Classes
{
    using System.Runtime.Serialization;

    using NewRelic.Synthetics.Api.Data;

    using Newtonsoft.Json;

    public class Monitor : MonitorBase
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        public bool ShouldSerializeId()
        {
            return false;
        }

        [JsonProperty("options")]
        public Options Options { get; set; }

        public bool ShouldSerializeOptions()
        {
            return false;
        }

        [JsonProperty("modifiedAt")]
        public DateTime ModifiedAt { get; set; }

        public bool ShouldSerializeModifiedAt()
        {
            return false;
        }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        public bool ShouldSerializeCreatedAt()
        {
            return false;
        }

        [JsonProperty("userId")]
        public int UserId { get; set; }

        public bool ShouldSerializeUserId()
        {
            return false;
        }

        [JsonProperty("apiVersion")]
        public string ApiVersion { get; set; }

        public bool ShouldSerializeApiVersion()
        {
            return false;
        }
        [IgnoreDataMember]
        public bool IsEnabled => this.Status.Equals(Constants.MonitorEnabled, StringComparison.InvariantCultureIgnoreCase);
    }
}
