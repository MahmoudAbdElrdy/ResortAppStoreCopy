
namespace Common.Localization
{
    public interface ILocalizationProvider
    {
        string Localize(string str, string cultureName);
        string Localize(string str, string cultureName, params object[] args);
    }
}