namespace Fanex.Result.Core.Base.Models
{
    public interface IValidate
    {
        bool ValidateNormal(string dtoName, SportType sportType);

        bool ValidateOutright(string dtoName);

        bool ValidateParlay(string dtoName);

        bool ValidateResource(string languageCode);
    }
}