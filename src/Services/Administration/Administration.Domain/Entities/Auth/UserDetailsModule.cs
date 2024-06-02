using AuthDomain.Entities.Auth;
using Common.Entity;
using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ResortAppStore.Services.Administration.Domain.Entities.Auth
{
    public class UserDetailsModule : BaseTrackingEntity<long>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
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


        public ICollection<UserSubscriptionPromoCode> UserSubscriptionPromoCodes { get; set; }

    }
}
