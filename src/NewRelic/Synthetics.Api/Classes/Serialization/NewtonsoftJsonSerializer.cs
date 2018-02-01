namespace NewRelic.Synthetics.Api.Classes.Serialization
{
    using System.IO;

    using Newtonsoft.Json;

    using RestSharp;

    public class NewtonsoftJsonSerializer : Serialization.IJsonSeriliazer
    {
        private readonly Newtonsoft.Json.JsonSerializer serializer;

        public NewtonsoftJsonSerializer(Newtonsoft.Json.JsonSerializer serializer)
        {
            this.serializer = serializer;
        }

        public string ContentType
        {
            get { return "application/json"; }
            set
            {
            }
        }

        public string DateFormat { get; set; }
        public string Namespace { get; set; }

        public string Serialize(object obj)
        {
            using (var stringWriter = new StringWriter())
            {
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
                    this.serializer.Serialize(jsonTextWriter, obj);
                    return stringWriter.ToString();
                }
            }
        }

        public T Deserialize<T>(IRestResponse response)
        {
            var content = response.Content;

            using (var stringReader = new StringReader(content))
            {
                using (var jsonTextReader = new JsonTextReader(stringReader))
                {
                    return this.serializer.Deserialize<T>(jsonTextReader);
                }
            }
        }

        public string RootElement { get; set; }

        public static NewtonsoftJsonSerializer Default => new NewtonsoftJsonSerializer(new Newtonsoft.Json.JsonSerializer()
        {
            NullValueHandling = NullValueHandling.Ignore
        });
    }
}