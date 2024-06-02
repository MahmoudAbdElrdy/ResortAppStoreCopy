using AutoMapper;
using Common.Enums;
using Common.Infrastructures;
using Common.Mapper;
using MediatR;
using ResortAppStore.Services.Administration.Domain.Entities.Configuration;
using System;
using System.Collections.Generic;

namespace ResortAppStore.Services.Administration.Application.Settings.GeneralConfigurations.Dto
{
    public class SettingDto : IHaveCustomMapping
    {
        public long Id { set; get; }
        public ValueTypeEnum? ValueType { get; set; }
        public string Value { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public string Code { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Setting, SettingDto>()

                .ReverseMap();

        }
    }
    public class GetAllSettingWithPagination : Paging
    {
     
    }
    public class EditSettingDto 
    {
        public List<SettingDto> Setting { get; set; }
    }
}