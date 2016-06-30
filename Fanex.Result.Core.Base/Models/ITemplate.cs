namespace Fanex.Result.Core.Base.Models
{
    public interface ITemplate
    {
        string GetRazorTemplate(string templateName);
    }
}