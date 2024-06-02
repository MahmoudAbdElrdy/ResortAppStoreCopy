using System.Collections.Generic;
using System.Reflection;
using AutoMapper;

using Common.Mapper;
using Sentry;

namespace Web.Profiler {
  public class AutoMapperProfile : Profile {
    public AutoMapperProfile() {
      LoadStandardMappings();
      LoadCustomMappings();
      LoadConverters();
    }

    private void LoadConverters() {

    }

    private void LoadStandardMappings() {
      var mapsFrom = MapperProfileHelper.LoadStandardMappings(Assembly.GetExecutingAssembly());

      foreach (var map in mapsFrom) {
        CreateMap(map.Source, map.Destination).ReverseMap();
      }
    }

    private void LoadCustomMappings() {
      var assemblies = new List<Assembly>() { (typeof(MapConfigDto).Assembly) };
      var mapsFrom = MapperProfileHelper.LoadCustomMappings();
      foreach (var map in mapsFrom) {
        map.CreateMappings(this);
      }
    }
  }
    public class MapConfigDto : IHaveCustomMapping
    {
        public long? Id { get; set; }
    
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<MapConfig, MapConfigDto>()
                .ReverseMap();

        }
    }
    public class MapConfig
    {
        public long? Id { get; set; }
    }
}
