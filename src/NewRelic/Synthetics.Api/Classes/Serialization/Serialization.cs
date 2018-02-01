namespace NewRelic.Synthetics.Api.Classes.Serialization
{
    using RestSharp.Deserializers;
    using RestSharp.Serializers;

    public class Serialization
    {
        /// <summary>
        /// Custom interface to implement NewtonSoft Json Serialization
        /// </summary>
        public interface IJsonSeriliazer : ISerializer, IDeserializer
        {
            
        }
    }
}