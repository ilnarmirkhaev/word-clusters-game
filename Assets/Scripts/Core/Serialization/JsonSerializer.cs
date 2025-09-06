using Newtonsoft.Json;

namespace Core.Serialization
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize<T>(T data)
        {
            return JsonConvert.SerializeObject(data, Formatting.Indented);
        }

        public T Deserialize<T>(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return default;
            }

            return JsonConvert.DeserializeObject<T>(data);
        }
    }
}