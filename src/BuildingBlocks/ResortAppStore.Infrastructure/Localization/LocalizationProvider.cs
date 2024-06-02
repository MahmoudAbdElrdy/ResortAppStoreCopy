using System;
using System.Globalization;
using System.Resources;

namespace Common.Localization
{
    public class LocalizationProvider : ILocalizationProvider
    {
        private readonly IResourceSourceManager _resourceSourceManager;

        public LocalizationProvider(IResourceSourceManager resourceSourceManager)
        {
            _resourceSourceManager = resourceSourceManager;
        }

        public string Localize(string str, string cultureName)
        {
            var types = _resourceSourceManager.GetRegisteredSourceTypes();

            foreach (var type in types)
            {
                var result = TryLocalize(str, type, cultureName);
                if (!string.IsNullOrWhiteSpace(result))
                    return result;
            }

            return str;
        }

        public string Localize(string str, string cultureName, params object[] args)
        {
            return string.Format(Localize(str, cultureName), args);
        }

        private string TryLocalize(string str, Type type, string cultureName)
        {
            var ci = new CultureInfo(cultureName);
            var resourceManager = new ResourceManager(type);
            var result = resourceManager.GetString(str, ci);
            return result;
        }
    }
}