using ResortAppStore.Services.Administration.Domain.Entities.Subscription;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Domain.Entities.Auth
{
    public class UserSubscriptionPromoCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string UserId { get; set; }


        [ForeignKey("UserDetailsPackage")]
        public long?  PackageUserDetailsId { get; set; }

     
        public long? ModuleUserDetailsCode { get; set; }

        [ForeignKey("PromoCodes")]
        public long PromoCodeId { get; set; }

        public DateTime SubscriptionDate { get; set; }


        public UserDetailsPackage UserDetailsPackage { get; set; }

     

        public PromoCodes  PromoCodes { get; set; }



    }
}
