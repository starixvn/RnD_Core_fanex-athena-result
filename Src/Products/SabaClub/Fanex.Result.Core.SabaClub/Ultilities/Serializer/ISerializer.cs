namespace Fanex.Result.Core.SabaClub.Ultilities.Serializer
{
    public interface ISerializer
    {
        string Serialize<T>(T data);

        T Deserialize<T>(string data);
    }
}