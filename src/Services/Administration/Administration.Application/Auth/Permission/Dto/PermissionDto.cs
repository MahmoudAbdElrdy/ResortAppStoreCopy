using AuthDomain.Entities.Auth;
using AutoMapper;
using Common.Infrastructures;
using Common.Mapper;
using ResortAppStore.Services.Administration.Application.Roles.Dto;
using ResortAppStore.Services.Administration.Domain.Entities.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Auth.Permission.Dto
{
    public class ScreenDto:IHaveCustomMapping
    {
        public long Id { set; get; }
        public string ScreenNameAr { set; get; }
        public string ScreenNameEn { set; get; }
        public List<GetAllPermissionDTO> Permissions { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Screen, ScreenDto>()
              
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
            configuration.CreateMap<AuthDomain.Entities.Auth.Permission, CreatePermissionDto>()
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
