using Common.Entity;
using Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Domain.Configuration
{
    public class UserDetailsModule : BaseTrackingEntity<long>
    {
        //  [Key]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long UserModuleId { get; set; } 
        public int? Code { get; set; }
        // [ForeignKey("User")]
        public string UserId { get; set; }
        //public User User { get; set; }

        public string NameAr { get; set; }
        [MaxLength(1000)]
        public string NameEn { get; set; }

        [Column(TypeName = "decimal(18, 4)")]
        public decimal OtherUserMonthlySubscriptionPrice { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal OtherUserYearlySubscriptionPrice { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal OtherUserFullBuyingSubscriptionPrice { get; set; }
        public bool IsFree { get; set; }
        public long? OtherModuleId { get; set; }


        [Column(TypeName = "decimal(18, 4)")]
        public decimal? InstrumentPattrenPrice { get; set; }


        [Column(TypeName = "decimal(18, 4)")]
        public decimal? BillPattrenPrice { get; set; }

        public int? InstrumentPattrenNumber { get; set; }
        public int? BillPattrenNumber { get; set; }

        public int? NumberOfUser { get; set; }

        public int NumberOfCompanies { get; set; }

        public int NumberOfBranches { get; set; }

        public TypeOfSubscription TypeOfSubscription { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal SubscriptionPrice { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime? SubscriptionExpiaryDate { get; set; }

        public bool IsPackageModule { get; set; } = false;

        public int? PackgId { get; set; }
        public long? CompanyId { get; set; }

    }
}
