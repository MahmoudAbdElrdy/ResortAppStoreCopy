

using Common.Interfaces;

namespace Common.Localization
{
    public interface ILocalizationManager
    {
        ILocalizationProvider LocalizationProvider { get; }
        IAuditService AppSession { get; }
    }
}