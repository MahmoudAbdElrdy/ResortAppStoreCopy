using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Application.POS.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.Shifts.Dto
{
    public class ShiftDetailsDto:IHaveCustomMapping
    {
        public long Id { get; set; }
        public string AccountId { get; set; }

        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public int Code { get; set; }
        public TimeSpan StartAtTime { get; set; }
        public TimeSpan EndAtTime { get; set; }
 
        public long ShiftMasterId { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<ShiftDetailsDto, ShiftDetail>()
                .ReverseMap();
            configuration.CreateMap<ShiftDetail, ShiftDetailsDto>().ReverseMap();

        }

    }
}
