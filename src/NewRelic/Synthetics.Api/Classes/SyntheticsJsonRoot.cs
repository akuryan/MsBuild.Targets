namespace NewRelic.Synthetics.Api.Classes
{
    using RestSharp.Deserializers;

    public class SyntheticsJsonRoot
    {
        [DeserializeAs(Name = "monitors")]
        public Monitor[] Monitors { get; set; }
        [DeserializeAs(Name = "count")]
        public int Count { get; set; }
    }
}