using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Domain.Entities.Auth
{
    public class UserPaymentOnline
    {
        [Key]
        public Guid CartId { get; set; }

        public string CartCurrencyCode { get; set;}


        public decimal CartAmount { get; set; }

        public string CartDecription { get;set;}

        public string PaymentIds { get; set; }

        public DateTime CreationDate { get; set; }


        public long OrganizationId { get; set; }
    }
}
