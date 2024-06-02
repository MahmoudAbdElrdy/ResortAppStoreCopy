using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.Constants
{
    public class EndPoints
    {
        //public static readonly string BaseUrlTest = "https://gw-apic-gov.gazt.gov.sa";
        public static readonly string BaseUrlTest = "https://gw-fatoora.zatca.gov.sa";
        public static readonly string BaseUrlProduction = "https://gw-fatoora.zatca.gov.sa";


        public static readonly string ReportSimpleSingleInvoice = "e-invoicing/simulation/invoices/reporting/single"; //For B2C
        public static readonly string ClearanceSingleInvoice = "e-invoicing/simulation/invoices/clearance/single"; //For B2B
        public static readonly string Complicance = "e-invoicing/simulation/compliance"; //To Issue Create Digital stamp request
        public static readonly string ValidateInvoice = "e-invoicing/simulation/compliance/invoices";
        public static readonly string IssueCsid = "e-invoicing/simulation/production/csids";


        public static readonly string ReportSimpleSingleInvoiceProd = "/e-invoicing/core/invoices/reporting/single"; //For B2C
        public static readonly string ClearanceSingleInvoiceProd = "/e-invoicing/core/invoices/clearance/single"; //For B2B
        public static readonly string ComplicanceProd = "/e-invoicing/core/compliance"; //To Issue Create Digital stamp request
        public static readonly string ValidateInvoiceProd = "/e-invoicing/core/compliance/invoices";
        public static readonly string IssueCsidProduction = "/e-invoicing/core/production/csids";


        public static readonly string ReportSimpleSingleInvoiceDev = "/e-invoicing/developer-portal/invoices/reporting/single"; //For B2C
        public static readonly string ClearanceSingleInvoiceDev = "/e-invoicing/developer-portal/invoices/clearance/single"; //For B2B
        public static readonly string ComplicanceDev = "/e-invoicing/developer-portal/compliance"; //To Issue Create Digital stamp request
        public static readonly string ValidateInvoiceDev = "/e-invoicing/developer-portal/compliance/invoices";
        public static readonly string IssueCsidProductionDev = "/e-invoicing/developer-portal/production/csids";


    }
}
