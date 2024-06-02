
using Common.Helper;
using System.Collections.Generic;

namespace Common.Localization
{
    public interface IResourceSourceManager : ISingletonDependency
    {
        IReadOnlyList<Type> GetRegisteredSourceTypes();
        void RegisterSourceType(Type type);
    }
}