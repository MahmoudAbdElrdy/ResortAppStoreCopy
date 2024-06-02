using Common.Entity;
using System.ComponentModel.DataAnnotations;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Accounting
{
    public class IncomingChequeMaster  : BaseTrackingEntity<long>
    {
        [MaxLength(50)]
        public string Code { get; set; }
        public  DateTime Date { get; set; }
        public DateTime DueDate { get; set; } 
        public long CompanyId { get; set; }
        //[ForeignKey(nameof(CompanyId))]
        //public virtual Company Company { get; set; }
        public long BranchId { get; set; }
        //[ForeignKey(nameof(BranchId))]
        //public virtual Branch Branch { get; set; }
        public string? BankAccountId { get; set; }

        //[ForeignKey(nameof(AccountId))]
        //public virtual Account Account { get; set; }
        public long? CurrencyId { get; set; }

        //[ForeignKey(nameof(CurrencyId))]
        //public virtual Currency Currency { get; set; } 

        public string? CheckIssuerDetails { get; set; }

        public double? Amount { get; set; }
        public double? AmountLocal { get; set; }
        public double? CurrencyFactor { get; set; }
        
        [MaxLength(250)]
        public string? Description { get; set;}
        public long FiscalPeriodId { get; set; }
        public long? CostCenterId { get; set; }
        public long? ProjectId { get; set; }


        public int Status { get; set; }
        public Guid? Guid { get; set; }
        public virtual List<IncomingChequeDetail> IncomingChequeDetail { get; set; }
        public virtual List<IncomingChequeStatusDetail> IncomingChequeStatusDetail { get; set; }


    }
}
