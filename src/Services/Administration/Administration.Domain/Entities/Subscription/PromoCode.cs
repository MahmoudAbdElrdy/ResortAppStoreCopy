using Common.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResortAppStore.Services.Administration.Domain.Entities.Auth;

namespace ResortAppStore.Services.Administration.Domain.Entities.Subscription
{
    public class PromoCodes : BaseTrackingEntity<long>
    {
        public PromoCodes()
        {
            DiscountPercentage = default;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [MaxLength(1000)]
        public string NameAr { get; set; }
        [MaxLength(1000)]
        public string NameEn { get; set; }

        [MaxLength(1000)]
        public string PromoCode { get; set; }
      
        [Column(TypeName = "decimal(18, 2)")]
        public decimal DiscountPercentage { get; set; }
        public DateTime FromDate { get; set; }
        public DateTime ToDate { get; set; }

        public ICollection<UserSubscriptionPromoCode> UserSubscriptionPromoCodes { get; set; }


    }
}
