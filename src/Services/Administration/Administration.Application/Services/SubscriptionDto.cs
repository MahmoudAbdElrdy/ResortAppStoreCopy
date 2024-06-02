using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Services
{
    public class SubscriptionDto
    {
        public long Id { set; get; }
        public int? NumberOfCompany { get; set; }
        public int? NumberOfBranch { get; set; }
        public DateTime? ContractStartDate { get; set; }
        public DateTime? ContractEndDate { get; set; }
        public string? Applications { get; set; }
        public DateTime? CreatedAt { get; set; }
        public bool? MultiCompanies { get; set; }
        public bool? MultiBranches { get; set; }
    }
}
