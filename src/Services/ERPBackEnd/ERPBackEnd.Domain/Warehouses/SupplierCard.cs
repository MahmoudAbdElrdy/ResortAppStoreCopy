using Common.Entity;
using Configuration.Entities;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Warehouses.Entities
{
    public class SupplierCard : BaseTrackingEntity<long>
    {

        [MaxLength(50)]
        public string Code { get; set; }
        public long SupplierGroupId { get; set; }
       
        public long CompanyId { get; set; }
        //[ForeignKey(nameof(CompanyId))]
        //public virtual Company? Company { get; set; }
        public long BranchId { get; set; }
        //[ForeignKey(nameof(BranchId))]
        //public virtual Branch? Branch { get; set; }
        [MaxLength(50)]
        public string NameAr { get; set; }
        [MaxLength(50)]
        public string NameEn { get; set; }
        [MaxLength(250)]
        public string Address { get; set; }
        [MaxLength(20)]
        public string Phone { get; set; }
        [MaxLength(50)]
        public string ResponsiblePerson { get; set; }
        //public virtual Account Account { get; set; }
        [MaxLength(450)]
        public string? AccountId { get; set; }
        [MaxLength(20)]
        public string? Fax { get; set; }
        [MaxLength(250)]
        public string? Email { get; set; }
        public virtual Country Country { get; set; }
        public long? CountryId { get; set; }
        [MaxLength(50)]
        public string? TaxNumber { get; set; }
        [MaxLength(50)]
        public string? PostalCode { get; set; }
        [MaxLength(50)]
        public string? MailBox { get; set; }
        public double? CreditLimit { get; set; }
        public double? InitialBalance { get; set; }
        public Guid? Guid { get; set; }





    }
}