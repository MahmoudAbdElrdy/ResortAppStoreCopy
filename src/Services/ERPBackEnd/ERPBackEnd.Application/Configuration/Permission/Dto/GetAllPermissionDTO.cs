using AutoMapper;
using Common.Enums;
using Common.Mapper;
using Configuration.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd
{
    public class GetAllPermissionDTO : IHaveCustomMapping
    {
        public long Id { set; get; }
        public string Name { get; set; }
        public string ControllerNameAr { set; get; }
        public string ControllerNameEn { set; get; }

        public string ActionNameAr { set; get; }
        public string ActionName { set; get; } 
        public string ActionNameEn { set; get; }
        public bool? IsChecked { set; get; }
        public long? ScreenId { get; set; }
        public string RoleId { get; set; }
   
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Permission, GetAllPermissionDTO>()
                 .ForMember(dest => dest.ControllerNameAr, opt => opt.MapFrom(s => s.Screen.ScreenNameAr))
                 .ForMember(dest => dest.ControllerNameEn, opt => opt.MapFrom(s => s.Screen.ScreenNameEn))
                 .ForMember(dest => dest.Name, opt => opt.MapFrom(s => s.Screen.Name))
                // .ForMember(dest => dest.ModuleType, opt => opt.MapFrom(s => s.Screen.ModuleType))
                // .ForMember(dest => dest.Screens, opt => opt.MapFrom(s => s.Screen))
                .ReverseMap();

        }
    }
}

