using Common.Entity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ResortAppStore.Services.Administration.Domain.Entities.Auth;
using ResortAppStore.Services.Administration.Domain.Entities.Subscription;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ResortAppStore.Services.Administration.Domain
{
    public class Module : BaseTrackingEntity<long>
    {
        public Module()
        {
            MonthlySubscriptionPrice = default;
            YearlySubscriptionPrice = default;
            FullBuyingSubscriptionPrice = default;
            OtherUserMonthlySubscriptionPrice = default;
            OtherUserYearlySubscriptionPrice = default;
            OtherUserFullBuyingSubscriptionPrice = default;
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [MaxLength(1000)]
        public string NameAr { get; set; }
        [MaxLength(1000)]
        public string NameEn { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal MonthlySubscriptionPrice { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal YearlySubscriptionPrice { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal FullBuyingSubscriptionPrice { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal OtherUserMonthlySubscriptionPrice { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal OtherUserYearlySubscriptionPrice { get; set; }
        [Column(TypeName = "decimal(18, 4)")]
        public decimal OtherUserFullBuyingSubscriptionPrice { get; set; }
        public bool IsFree { get; set; }
        public long?  OtherModuleId { get; set; }


        [Column(TypeName = "decimal(18, 4)")]
        public decimal? InstrumentPattrenPrice { get; set; }


        [Column(TypeName = "decimal(18, 4)")]
        public decimal? BillPattrenPrice { get; set; }

        public ICollection<PackagesModules> PackagesModules { get; set; }
      

        // Navigation property for modules
        public ICollection<UserDetailsModule> UserDetailsModules { get; set; } 
    }
}
