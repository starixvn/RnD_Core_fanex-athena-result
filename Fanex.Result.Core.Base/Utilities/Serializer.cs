namespace Fanex.Result.Core.Base.Utilities
{
    using SimpleJson;

    public interface ISerializer
    {
        string Serialize<T>(T data);

        T Deserialize<T>(string data);
    }

    public class JsonSerializer : ISerializer
    {
        public T Deserialize<T>(string data)
            => SimpleJson.DeserializeObject<T>(data);

        public string Serialize<T>(T data)
            => SimpleJson.SerializeObject(data);
    }
}