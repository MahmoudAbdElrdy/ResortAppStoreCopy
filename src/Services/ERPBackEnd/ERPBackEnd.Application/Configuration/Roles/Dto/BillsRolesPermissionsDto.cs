using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.Roles.Dto
{
    public class BillsRolesPermissionsDto:IHaveCustomMapping
    {

        public long Id { get; set; }
        public long? BillTypeId { get; set; }
        public bool? IsUserChecked { get; set; }
        public string? PermissionsJson { get; set; }
        public string? RoleId { get; set; }

        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<BillsRolesPermissionsDto, BillsRolesPermissions>()
                .ReverseMap();
            configuration.CreateMap<BillsRolesPermissions, BillsRolesPermissionsDto>().ReverseMap();

        }
    }
}
