namespace Fanex.Result.Core.PPLiveCasino.Ultilities.Serializer
{
    public interface ISerializer
    {
        string Serialize<T>(T data);

        T Deserialize<T>(string data);
    }
}