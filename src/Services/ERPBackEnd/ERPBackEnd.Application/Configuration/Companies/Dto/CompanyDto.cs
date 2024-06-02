using AutoMapper;
using Common.Mapper;
using Common.ValidationAttributes;
using Configuration.Entities;
using System.ComponentModel.DataAnnotations;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.Companies.Dto
{
    public class CompanyDto: IHaveCustomMapping
    {
        public long Id { get; set; }
        [ValidationLocalizedData("NameEn")]
        public string NameAr { get; set; } 
        public string? NameEn { get; set; }
        public string? Address { get; set; }
        public string? Code { get; set; }
        public string? PhoneNumber { get; set; }
        public string? MailBox { get; set; }
  
        public string? ZipCode { get; set; }
    
        public string? CommercialRegistrationNo { get; set; }
        public string? TaxNumber { get; set; }
        public string? MobileNumber { get; set; }
        public string? Email { get; set; }
        public string? WebSite { get; set; }
        public long? CountryId { get; set; }
        public string? CountryNameAr { get; set; } 
        public string? CountryNameEn { get; set; } 
        public string? Symbol { get; set; }
        public long? CurrencyId { get; set; }
        public bool? MotherCompany { get; set; }
        public bool? UseHijri { get; set; }
        public string? Logo { get; set; }
        public bool? IsActive { get; set; }
        public string? SpecialNumber { get; set; }
        public string? Activity { get; set; }
        public byte? IntegrationType { get; set; }
        public string? BillType { get; set; }
        public string? GenCSRConfig { get; set; }
        public string? CSRBase64 { get; set; }
        public string? Certificate { get; set; }
        public string? SecretKey { get; set; }
        public string? RequestId { get; set; }
        public string? PCSID { get; set; }
        public string? ProductionSecretKey { get; set; }
        public string? ProductionRequestId { get; set; }
        public string? PublicKey { get; set; }
        public string? PrivateKey { get; set; }
        public string? CompanyType { get; set; }
        public string? Governorate { get; set; }
        public string? RegionCity { get; set; }
        public string? Street { get; set; }
        public string? BuildingNumber { get; set; }
        public string? Floor { get; set; }
        public string? Room { get; set; }
        public string? LandMark { get; set; }
        public string? AdditionalInformation { get; set; }
        public string? ClientId { get; set; }
        public string? ClientSecret { get; set; }
        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Company, CompanyDto>()
                 .ForMember(x => x.Symbol, expression => expression.MapFrom(x => x.Currency.Symbol))
                 
                 .ForMember(x => x.CountryNameAr, expression => expression.MapFrom(x => x.Country.NameAr))
               
                 .ForMember(x => x.CountryNameEn, expression => expression.MapFrom(x => x.Country.NameEn))

                .ReverseMap();

            configuration.CreateMap<CompanyDto,Company>()
                 .ForMember(dest => dest.Currency, opt => opt.Ignore())
                 .ForMember(dest => dest.Country, opt => opt.Ignore())
                .ReverseMap();
          //  configuration.CreateMap<List<Company>, List<CompanyDto>>();

        }
    }
   
}
