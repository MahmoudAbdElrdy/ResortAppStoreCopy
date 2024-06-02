using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Auth.Payment.Dto
{
    public class PaymentResponseLinkDto
    {
        public string tran_ref { get; set; }

        public string tran_type { get; set; }

        public string cart_id { get; set; }
        public string cart_description { get; set; }
        public string cart_currency { get; set; }
        public string cart_amount { get; set; }
        public string tran_total { get; set; }

        public string Return { get; set; }
        public int serviceId { get; set; }
        public int profileId { get; set; }
        public int merchantId { get; set; }
        public string trace { get; set; }

        public string redirect_url { get; set; }

    }
}
