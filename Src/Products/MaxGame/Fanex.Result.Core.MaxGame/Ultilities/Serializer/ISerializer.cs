namespace Fanex.Result.Core.MaxGame.Ultilities.Serializer
{
    public interface ISerializer
    {
        string Serialize<T>(T data);

        T Deserialize<T>(string data);
    }
}