using Common.Entity;
using Configuration.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Accounting
{
    public class Account : BaseTrackingEntity<string>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Id { get; set; }
       
        [MaxLength(250)]
        public string NameAr { get; set; }
        [MaxLength(250)]
        public string? NameEn { get; set; }
        [MaxLength(200)]
        public string? Code { get; set; }
        public int LevelId { get; set; }
        public string TreeId { get; set; }

        public Decimal? OpenBalanceDebit { get; set; }
        public Decimal? OpenBalanceCredit { get; set; }
        public Decimal? DebitLimit { get; set; }
        public Decimal? CreditLimit { get; set; }
        public string? TaxNumber { get; set; }
        public string? NoteNotActive { get; set; } 
        public int? AccountType { get; set; }  
       
        public int? Budget { get; set; } 
        public bool? IsLeafAccount { get; set; }
        public long? AccountClassificationIdOfIncomeStatement { get; set; }
        [ForeignKey(nameof(AccountClassificationIdOfIncomeStatement))]
        public virtual AccountClassification? AccountClassification { get; set; }
        public int AccountClassificationId { get; set; }

        public long? CompanyId { get; set; }

        [ForeignKey(nameof(CompanyId))]
        public virtual Company? Company { get; set; }

        public long? AccountGroupId { get; set; }

        [ForeignKey(nameof(AccountGroupId))]
        public virtual AccountGroup? AccountGroup { get; set; }

        public long? CurrencyId { get; set; } 

        [ForeignKey(nameof(CurrencyId))]
        public virtual Currency? Currency { get; set; }

        public long? CostCenterId { get; set; } 

        [ForeignKey(nameof(CostCenterId))]
        public virtual CostCenter? CostCenter { get; set; }

        public string? ParentId { get; set; }

        [ForeignKey(nameof(ParentId))]
        public virtual Account Parent { get; set; }
        public Guid? Guid { get; set; }
        public virtual ICollection<Account> Children { get; set; }

        public virtual ICollection<CustomerCard> CutomerCards { get; set; }
    }
}
