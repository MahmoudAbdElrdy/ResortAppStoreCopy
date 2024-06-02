using Common.Helper;
using System.Reflection;


namespace Common.Mapper
{
    public sealed class Map {
    public Type Source { get; set; }
    public Type Destination { get; set; }
  }

  public static class MapperProfileHelper {
    public static IList<Map> LoadStandardMappings(Assembly rootAssembly) {
      var types = rootAssembly.GetExportedTypes();

      var mapsFrom = (
        from type in types
        from instance in type.GetInterfaces()
        where
          instance.IsGenericType && instance.GetGenericTypeDefinition() == typeof(IMapFrom<>) &&
          !type.IsAbstract &&
          !type.IsInterface
        select new Map {
          Source = type.GetInterfaces().First().GetGenericArguments().First(),
          Destination = type
        }).ToList();

      return mapsFrom;
    }

    public static IList<IHaveCustomMapping> LoadCustomMappings() {
      var mapsFrom = new List<IHaveCustomMapping>();


            var repoInterfaces = DependenciesHelper.GetAssemblies().Where(t =>  t.FullName.EndsWith("IHaveCustomMapping") && t.FullName.Contains("")).ToList();
            var cc = DependenciesHelper.GetAssemblies().Where(c=>c.FullName.Contains("ResortAppStore.Services")|| c.FullName.Contains("ResortAppStore.Services"));
            foreach (var assembly in cc) {
        var types = assembly.GetExportedTypes();

        var map = (
          from type in types
          from instance in type.GetInterfaces()
          where
            typeof(IHaveCustomMapping).IsAssignableFrom(type) &&
            !type.IsAbstract &&
            !type.IsInterface
          select (IHaveCustomMapping) Activator.CreateInstance(type)).ToList();
        
        mapsFrom.AddRange(map);
      }
      return mapsFrom;
    }

    public static IList<IHaveCustomMapping> LoadCustomMappings(Assembly rootAssembly) {
      var types = rootAssembly.GetExportedTypes();

      var mapsFrom = (
        from type in types
        from instance in type.GetInterfaces()
        where
          typeof(IHaveCustomMapping).IsAssignableFrom(type) &&
          !type.IsAbstract &&
          !type.IsInterface
        select (IHaveCustomMapping) Activator.CreateInstance(type)).ToList();

      return mapsFrom;
    }
  }
}