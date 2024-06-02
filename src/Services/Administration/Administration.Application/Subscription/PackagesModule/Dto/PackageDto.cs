using AutoMapper;
using Common.Mapper;
using ResortAppStore.Services.Administration.Application.Subscription.PromoCode.Dto;
using ResortAppStore.Services.Administration.Domain.Entities.Subscription;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Subscription.PackagesModule.Dto
{
    public class PackageDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public decimal MonthlyPrice { get; set; }
        public decimal YearlyPrice { get; set; }
        public decimal FullBuyPrice { get; set; }
        public int NumberOfUsers { get; set; }

        public int NumberOfCompanies { get; set; }

        public int NumberOfBranches { get; set; }


        public int? BillPattrenNumber { get; set; }

        public int? InstrumentPattrenNumber { get; set; }
        public bool IsCustomized { get; set; }

        public List<long> ModuleIds { get; set; }


        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Package, PackageDto>().ReverseMap();
        }

    }


    public class PackageListDto : IHaveCustomMapping
    {
        public long Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public decimal MonthlyPrice { get; set; }
        public decimal YearlyPrice { get; set; }
        public decimal FullBuyPrice { get; set; }
        public int NumberOfUsers { get; set; }

        public int NumberOfCompanies { get; set; }

        public int NumberOfBranches { get; set; }


        public int? BillPattrenNumber { get; set; }

        public int? InstrumentPattrenNumber { get; set; }
        public bool IsCustomized { get; set; }

        public List<ModuleDto> Modules { get; set; }= new List<ModuleDto>();


        public void CreateMappings(Profile configuration)
        {
            configuration.CreateMap<Package, PackageListDto>()
                .ForMember(x => x.Modules, src => src.Ignore())
                .ReverseMap();
        }

    }

    public class AddEditPackageDto
    {
        public long Id { get; set; }
        public string NameAr { get; set; }
        public string NameEn { get; set; }
        public decimal MonthlyPrice { get; set; }
        public decimal YearlyPrice { get; set; }
        public decimal FullBuyPrice { get; set; }
        public decimal ExtraUserMonthlyPrice { get; set; }
        public decimal ExtraUserYearlyPrice { get; set; }
        public decimal ExtraUserFullBuyPrice { get; set; }
        public bool IsCustomized { get; set; }

        public List<long> ModuleIds { get; set; }

    }
}
