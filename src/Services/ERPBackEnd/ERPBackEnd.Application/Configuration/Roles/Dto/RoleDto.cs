
using AutoMapper;
using Common.Infrastructures;
using Common.Mapper;
using Common.ValidationAttributes;
using Configuration.Entities;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Roles.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using System.Collections.Generic;

namespace ResortAppStore.Services.ERPBackEnd.Application.Roles.Dto
{
    public class RoleDto : IHaveCustomMapping
    {
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Id { get; set; }
        public string Code { get; set; }
        public bool? IsActive { get; set; }
        public List<VouchersRolesPermissionsDto> VouchersRolesPermissions { set; get; }

        public PaginatedList<GetAllPermissionDTO> Permissions { get; set; }
        public List<BillsRolesPermissionsDto> BillsRolesPermissions { get; set; }
        public List<POSBillsRolesPermissionsDto> POSBillsRolesPermissions { get; set; }

        public List<ScreenDto> Screens { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.NameEn, opt => opt.MapFrom(s => s.Name))
                .ReverseMap();

        }
    }
    public class CreateRoleCommand
    {
        [ValidationLocalizedData("NameEn")]
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Code { get; set; }
        public List<CreatePermissionDto> Permissions { set; get; }
        public List<BillsRolesPermissions> BillsRolesPermissions { set; get; }
        public List<POSBillsRolesPermissions> POSBillsRolesPermissions { set; get; }

        public List<VouchersRolesPermissions> VouchersRolesPermissions { set; get; }
    }
    public class DeleteListRoleCommand
    {
        public string[] Ids { get; set; }

    }
    public class DeleteRoleCommand
    {
        public string Id { get; set; }
    }
    public class EditRoleCommand
    {
        [ValidationLocalizedData("NameEn")]
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Id { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; } 
        public List<CreatePermissionDto> Permissions { set; get; }
        public List<BillsRolesPermissions> BillsRolesPermissions { set; get; }
        public List<POSBillsRolesPermissions> POSBillsRolesPermissions { set; get; }

        public List<VouchersRolesPermissions> VouchersRolesPermissions { set; get; }
    }
    public class GetAllRolesWithPaginationCommand : Paging
    {
    }
    public class GetByRoleId 
    {
        public string Id { get; set; }
    }
}