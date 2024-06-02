using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Egypt_EInvoice_Api.EInvoiceModel
{
    public class buyer
    {
        public string type { get; set; }//Mandatory, Type of the issuer - supported values - B for business in Egypt, P for natural person, F for foreigner
        public string id { get; set; }//Optional in all cases except when 1.type is B 2.type is P and totalAmount equals to or greater than a configured value(ex. 50000 EGP)
        public string name { get; set; }//Optional in all cases except when 1.type is B 2.type is P and totalAmount equals to or greater than a configured value(ex. 50000 EGP)
        public string mobileNumber { get; set; }//Optional, Mobile number of receiver
        public string paymentNumber { get; set; }

    }
}
