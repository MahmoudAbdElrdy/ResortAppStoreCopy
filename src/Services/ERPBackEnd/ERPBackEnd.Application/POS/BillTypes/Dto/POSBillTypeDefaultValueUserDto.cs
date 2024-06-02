using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.BillTypes.Dto
{
    public class POSBillTypeDefaultValueUserDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public long BillTypeId { get; set; }
        public string UserId { get; set; }
        public long? DefaultCurrencyId { get; set; }
        public long? StoreId { get; set; }
        public long? CostCenterId { get; set; }
        public int? DefaultPrice { get; set; }
        public long DefaultShiftId { get; set; }
        public long PointOfSaleId { get; set; }
        public long DefaultCustomerId { get; set; }
        public long? DefaultPaymentMethodId { get; set; }


        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<POSBillTypeDefaultValueUserDto, POSBillTypeDefaultValueUser>().ReverseMap();
            configuration.CreateMap<POSBillTypeDefaultValueUser, POSBillTypeDefaultValueUserDto>().ReverseMap();


        }
       
    }
}