using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.Constants
{
    //public  class Enums
    //{
    //}
    public enum InvoiceType
    {
        B2B = 1,
        B2C =2,
    }
    public enum InvoiceTypeCode 
    {
        StandardInvoice = 380,
        SimplifiedInvoice = 388,
        DebitNote = 383,
        CreditNote = 381,
        SelfBilledInvoice = 389

    }

    enum TransactionTypeCode  //NNPNES
    {
        StandardInvoice = 1, //01
        SimplifiedInvoice = 2, //02
        ThirdPartyInvoice = 0, //0 false , 1 true
        NominalInvoiceTransaction = 1, //0 false, 1 true
        ExportInvoice = 0, //0 false, 1 true
        SummaryInvoice = 0, //0 false , 1 true
        
        
    }

    public enum ZATCAPaymentMethods
    {
        CASH = 10,
        CREDIT = 30,
        BANK_ACCOUNT = 42,
        BANK_CARD = 48
    }

  public  enum ZATCAInvoiceTypes
    {
        INVOICE = 388,
        DEBIT_NOTE = 383,
        CREDIT_NOTE = 381
    }


    public enum PrintInvoiceType
    {
        B2BInvoice ,
        B2BCreditNote,
        B2BDebitNote,
        B2CInvoice ,
        B2CCreditNote,
        B2CDebitNote
    }
}
