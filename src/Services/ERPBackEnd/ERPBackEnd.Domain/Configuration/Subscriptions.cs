using Common.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Configuration
{
    public class Subscriptions : BaseTrackingEntity<long>
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Id { set; get; }
        public int? NumberOfCompany { get; set; }
        public int? NumberOfBranch { get; set; }
        public bool? MultiCompanies { get; set; }
        public bool? MultiBranches { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string? Applications { get; set; }
    }
}
