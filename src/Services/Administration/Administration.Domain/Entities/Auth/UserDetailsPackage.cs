using AuthDomain.Entities.Auth;
using Common.Entity;
using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ResortAppStore.Services.Administration.Domain.Entities.Auth
{
    public class UserDetailsPackage : BaseTrackingEntity<long>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

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

        public ICollection<UserSubscriptionPromoCode> UserSubscriptionPromoCodes { get; set; }

    }
    
}
