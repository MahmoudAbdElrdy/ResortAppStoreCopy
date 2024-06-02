
using Common.Helper;

namespace Common.Localization
{
    public class ResourceSourceManager : IResourceSourceManager, ISingletonDependency
    {
        protected IList<Type> RegisteredSourceTypes { get; }

        public ResourceSourceManager()
        {
            RegisteredSourceTypes = new List<Type>();
        }

        public IReadOnlyList<Type> GetRegisteredSourceTypes()
        {
            return RegisteredSourceTypes.ToList();
        }

        public void RegisterSourceType(Type type)
        {
            RegisteredSourceTypes.Add(type);
        }
    }
}