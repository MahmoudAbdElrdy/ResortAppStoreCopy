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
    public class UserDetailsPackage : BaseTrackingEntity<long>
    {
        // [Key]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long UserPackageId { get; set; } 

        //[ForeignKey("User")]
        public string UserId { get; set; }
        //public User User { get; set; }

        [MaxLength(1000)]
        public string NameAr { get; set; }

        [MaxLength(1000)]
        public string NameEn { get; set; }
        public int NumberOfUsers { get; set; }

        public int NumberOfCompanies { get; set; }

        public int NumberOfBranches { get; set; }
   
        public int? BillPattrenNumber { get; set; }

        public int? InstrumentPattrenNumber { get; set; }
        public TypeOfSubscription TypeOfSubscription { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal SubscriptionPrice { get; set; }
        public DateTime SubscriptionStartDate { get; set; }
        public DateTime? SubscriptionExpiaryDate { get; set; }
        public long? CompanyId { get; set; }


    }
}
