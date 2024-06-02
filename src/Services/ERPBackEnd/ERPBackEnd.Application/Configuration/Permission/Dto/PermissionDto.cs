
using AutoMapper;
using Common.Infrastructures;
using Common.Mapper;
using Configuration.Entities;
using MediatR;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using System.Collections.Generic;

namespace ResortAppStore.Services.ERPBackEnd
{
    public class ScreenDto : IHaveCustomMapping
    {
        public long Id { set; get; }
        public string ScreenNameAr { set; get; }
        public string ScreenNameEn { set; get; }
        public int? ModuleType { get; set; }
        public List<GetAllPermissionDTO> Permissions { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Screen, ScreenDto>()
                .ForMember(dest => dest.ModuleType, opt => opt.MapFrom(s => (int)s.ModuleType))

                .ReverseMap();

        }
    }
    public class CreatePermissionDto : IHaveCustomMapping
    {
        public long Id { set; get; }
        public string Name { get; set; }
        public string ControllerNameAr { set; get; }
        public string ControllerNameEn { set; get; }

        public string ActionNameAr { set; get; }
        public string ActionNameEn { set; get; }
        public bool? IsChecked { set; get; }
        public long? ScreenId { get; set; }
        // public string RoleId { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Permission, CreatePermissionDto>()
                // .ForMember(dest => dest.RoleId, opt => opt.MapFrom(s => s.PermissionRoles.Select()))
                .ForMember(dest => dest.ControllerNameAr, opt => opt.MapFrom(s => s.Screen.ScreenNameAr))
                 .ForMember(dest => dest.ControllerNameEn, opt => opt.MapFrom(s => s.Screen.ScreenNameEn))

                .ReverseMap();

        }
    }
    public class GetAllPermissionWithPaginationCommand : Paging
    {
        public string RoleId { get; set; }
    }
    public class CreatePermissionCommand
    {
        public string RoleId { set; get; }
        public List<CreatePermissionDto> Permissions { set; get; }
    }
    public class DeletePermissionCommand
    {
        public long Id { get; set; }
    }
}