using AutoMapper;
using Common.Enums;
using Common.Infrastructures;
using Common.Mapper;
using Configuration.Entities;
using MediatR;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Companies.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.GeneralConfigurations.Dto
{
    public class GeneralConfigurationDto : IHaveCustomMapping
    {
        public long Id { set; get; }
        public ValueTypeEnum? ValueType { get; set; }
        public ModuleType? ModuleType { get; set; }
        public string Value { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Code { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<GeneralConfiguration, GeneralConfigurationDto>()

                .ReverseMap();

        }
    }
    public class GetAllGeneralConfigurationWithPagination : Paging
    {
        public ModuleType? ModuleType { get; set; }
    }
    public class EditGeneralConfigurationCommand 
    {
        public List<GeneralConfigurationDto> generalConfiguration { get; set; }
    }
}