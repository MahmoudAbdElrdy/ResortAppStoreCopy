

using Common.Interfaces;

namespace Common.Localization
{
    public class LocalizationManager : ILocalizationManager
    {
        public LocalizationManager(ILocalizationProvider localizationProvider, IAuditService appSession)
        {
            LocalizationProvider = localizationProvider;
            AppSession = appSession;
        }

        public ILocalizationProvider LocalizationProvider { get; }
        public IAuditService AppSession { get; }
    }
}