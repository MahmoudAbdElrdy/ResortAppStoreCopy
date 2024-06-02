using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Auth.Payment.Dto
{
    public class PaymentRequestLinkDto
    {
        public string profile_id { get; set; }
        public string tran_type { get; set; }
        public string tran_class { get; set; }
        public string cart_id { get; set; }
        public string cart_currency { get; set; }
        public decimal cart_amount { get; set; }
        public string cart_description { get; set; }

    }
}
