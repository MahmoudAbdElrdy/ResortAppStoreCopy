using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;
using System;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.POSDeliveries.Dto
{
    public class POSDeliveryDetailsDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public long ShiftDetailId { get; set; }
        public long POSId { get; set; }
        public DateOnly Date { get; set; }
        public string? BillIds { get; set; }
        public string? CashTransferIds { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<POSDeliveryDetailsDto, POSDeliveryDetail>()
                .ReverseMap();
            configuration.CreateMap<POSDeliveryDetail, POSDeliveryDetailsDto>().ReverseMap();

        }

    }
}
