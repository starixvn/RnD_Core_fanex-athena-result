namespace Fanex.Result.Core.Joker.Ultilities.Serializer
{
    public interface ISerializer
    {
        string Serialize<T>(T data);

        T Deserialize<T>(string data);
    }
}