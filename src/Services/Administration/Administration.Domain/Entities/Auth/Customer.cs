using AuthDomain.Entities.Auth;
using Common.Entity;
using Microsoft.VisualBasic;
using ResortAppStore.Services.Administration.Domain.Entities.LookUp;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Domain.Entities.Auth
{
    public class Customer :BaseTrackingEntity<long>
    {
        public Customer()
        {
            CustomerSubscriptions = new List<CustomerSubscription>();
        }

        [MaxLength(500)]
        public string? NameAr { get; set; }
        [MaxLength(500)]
        public string? NameEn { get; set; }
      
        [ForeignKey(nameof(CountryId))]
        public Country Country { get; set; }
        public long CountryId { get; set; }

        [ForeignKey(nameof(BusinessId))]
        public Business? Business { get; set; }
        
        public long? BusinessId { get; set; }
        [MaxLength(500)]
        public string? Code { get; set; }
        [MaxLength(50)]
        public string? PhoneNumber { get; set; }

        [MaxLength(50)]
        public string? Email { get; set; }
        public string? CompanySize { get; set; }
        public bool? MultiCompanies { get; set; }

        public bool? MultiBranches { get; set; } 
        public int? NumberOfCompany { get; set; }
        public int? NumberOfBranch { get; set; }
        public string? PassWord { get; set; }
        public bool? IsVerifyCode { get; set; }
        [MaxLength(500)]
        public string? VerifyCode { get; set; }
        public string? ServerName { get; set; }
        public string? DatabaseName { get; set; }
        public string? SubDomain { get; set; } 
        public virtual ICollection<CustomerSubscription> CustomerSubscriptions { get; set; }
      

    }
}
