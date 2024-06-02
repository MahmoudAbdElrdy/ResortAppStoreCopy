using Common.Entity;
using Configuration.Entities;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouses.Entities
{
    public class CustomerCard : BaseTrackingEntity<long>
    {

        [MaxLength(50)]
        public string Code { get; set; }
        public long CustomerGroupId { get; set; }

        //[ForeignKey(nameof(CustomerGroupId))]
        //public virtual CustomerGroup CustomerGroup { get; set; }

        public long CompanyId { get; set; }
        //[ForeignKey(nameof(CompanyId))]
        //public virtual Company Company { get; set; }
        public long BranchId { get; set; }
        //[ForeignKey(nameof(BranchId))]
        //public virtual Branch Branch { get; set; }
        [MaxLength(50)]
        public string NameAr { get; set; }
        [MaxLength(50)]
        public string NameEn { get; set; }

        [MaxLength(1)]
        public string? CustomerType { get; set; }
        [MaxLength(250)]
        public string Address { get; set; }
        [MaxLength(20)]
        public string Phone { get; set; }
        [MaxLength(50)]
        public string ResponsiblePerson { get; set; }             
   
        [MaxLength(450)]
        public string? AccountId { get; set; }

        [ForeignKey("AccountId")]
        public virtual Account Account { get; set; }
        [MaxLength(20)]
        public string? Fax { get; set; }
        [MaxLength(250)]
        public string? Email { get; set; }
        public virtual Country Country { get; set; }
        public long? CountryId { get; set; }
        [MaxLength(50)]
        public string? TaxNumber { get; set; }
        [MaxLength(50)]
        public string? IdNumber { get; set; }
        [MaxLength(50)]
        public string? PostalCode { get; set; }
        [MaxLength(50)]
        public string? MailBox { get; set; }
        public double? CreditLimit { get; set; }
        public double? InitialBalance { get; set; }
        [MaxLength(50)]

        public string? City { get; set; }
        [MaxLength(50)]

        public string? District { get; set; }
        [MaxLength(50)]

        public string? Region { get; set; }
        [MaxLength(50)]

        public string? Street { get; set; }
        [MaxLength(50)]

        public string? AdditionalStreet { get; set; }
        [MaxLength(50)]

        public string? BuildingNo { get; set; }
        [MaxLength(50)]

        public string? AdditionalBuildingNo { get; set; }
        [MaxLength(50)]

        public string? CountryCode { get; set; }
        [MaxLength(5)]

        public string? RegisterationType { get; set; }
        [MaxLength(250)]

        public string? ZakaTaxCustomsAuthorityName { get; set; }
        public Guid? Guid { get; set; }
        public long? PriceListId { get; set; }
        [ForeignKey(nameof(PriceListId))]
        public virtual PriceListMaster? PriceListMaster { get; set; }

        [MaxLength(50)]
        public string? Governorate { get; set; }
        [MaxLength(250)]
        public string? RegionCity { get; set; }

        [MaxLength(50)]
        public string? Floor { get; set; }

        [MaxLength(50)]
        public string? Room { get; set; }

        [MaxLength(50)]
        public string? LandMark { get; set; }

        [MaxLength(250)]
        public string? AdditionalInformation { get; set; }






    }
}