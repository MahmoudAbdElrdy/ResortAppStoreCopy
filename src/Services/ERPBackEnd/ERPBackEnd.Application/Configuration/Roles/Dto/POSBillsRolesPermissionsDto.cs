using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.Roles.Dto
{
    public class POSBillsRolesPermissionsDto : IHaveCustomMapping
    {

        public long Id { get; set; }
        public long? BillTypeId { get; set; }
        public bool? IsUserChecked { get; set; }
        public string? PermissionsJson { get; set; }
        public string? RoleId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<POSBillsRolesPermissionsDto, POSBillsRolesPermissions>()
                .ReverseMap();
            configuration.CreateMap<POSBillsRolesPermissions, POSBillsRolesPermissionsDto>().ReverseMap();

        }
    }
}
