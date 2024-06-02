using AutoMapper;
using Common.Mapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto
{
    public class ItemCardDto : IHaveCustomMapping
    {
      
        public long? Id { get; set; }
        [MaxLength(200)]
        public string Code { get; set; }
        public long BranchId { get; set; }
        public long CompanyId { get; set; }
        [MaxLength(250)]
        public string NameAr { get; set; }

        [MaxLength(250)]
        public string? NameEn { get; set; }
        public string? Barcode { get; set; }

        public long ItemGroupId { get; set; }

        public int ItemType { get; set; }

        public int? CostCalculateMethod { get; set; }

        [MaxLength(500)]
        public string? Notes { get; set; }
        public string? Image { get; set; }
        public byte[]? ImageData { get; set; }

        public bool IsActive { get; set; }

        [MaxLength(50)]
        public string? Model { get; set; }

        [MaxLength(50)]
        public string? Manufacturer { get; set; }

        public int? MaxLimit { get; set; }

        public int? MinLimit { get; set; }

        public int? ReorderLimit { get; set; }

        [MaxLength(500)]
        public string? Description { get; set; }

        public long? MainUnitId { get; set; }
        public long? DefaultUnitId { get; set; }
        public double? SellingPrice { get; set; }
        public double? MinSellingPrice { get; set; }
        public double? OpeningCostPrice { get; set; }
        public double? CostPrice { get; set; }
        public double? LifeTime { get; set; }

        public int? LifeTimeType { get; set; }

        public double? HeightFactor { get; set; }

        public double? WidthFactor { get; set; }

        public double? LengthFactor { get; set; }

        public double? WarrantyPeriod { get; set; }

        public int? WarrantyType { get; set; }

        public int? ItemNature { get; set; }
        public bool? CalculateTaxInCumulativeManner { get; set; }

        public string? TaxIds { get; set; }

        public string? Attachment { get; set; }
        public string? SalesAccountId { get; set; }
        public string? SalesReturnsAccountId { get; set; }
        public string? PurchasesAccountId { get; set; }
        public string? PurchasesReturnsAccountId { get; set; }
        public string? SalesCostAccountId { get; set; }
        public string? InventoryAccountId { get; set; }
        public long? DefaultStoreId { get; set; }

        public DateTime CreatedAt { get; set; }
        public string? CreatedBy { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        [MaxLength(36)]
        public string? UpdateBy { get; set; }
        public double? Quantity { get; set; }
        public Guid? Guid { get; set; }
        public long? CostCenterId { get; set; }
        public string? ItemCodeType { get; set; }
        public string? ItemCode { get; set; }
        public string? GPCCode { get; set; }

        public bool? IsUploaded { get; set; }
        public string? SubmissionNotes { get; set; }


        public virtual List<ItemCardAlternativeDto> ItemCardAlternatives { get; set; }
        public virtual List<ItemCardUnitDto> ItemCardUnits { get; set; }
        public virtual List<ItemCardBalanceDto> ItemCardBalances { get; set; }

        public virtual List<ItemCardDeterminantDto> ItemCardDeterminants { get; set; }
        public void CreateMappings(Profile configuration)
        {

            configuration.CreateMap<ItemCardDto, ResortAppStore.Services.ERPBackEnd.Domain.Warehouses.ItemCard>()
               .ReverseMap();
            configuration.CreateMap<ResortAppStore.Services.ERPBackEnd.Domain.Warehouses.ItemCard, ItemCardDto>().ReverseMap();
        }
    }
}
