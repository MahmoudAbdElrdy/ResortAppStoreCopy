using Microsoft.EntityFrameworkCore;
using SaudiEinvoiceService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.IRepos
{
    public interface ITaxBillMasterRepos
    {
        IEnumerable<vwTaxBillMaster> GetAll();
        IEnumerable<vwTaxBillMaster> GetByType(Guid typeGuid);
        vwTaxBillMaster? GetByGuid(Guid guid);
        string UpdateInvoiceHashAndQR(string invoiceHash, string qrCode, Guid guid);        
        void SetDbName(string dbName);
        void SetDbContext(ResortContext context);
        string GetPreviousInvoiceHash(Guid typeGuid, int number, int branch);
        IEnumerable<vwTaxBillMaster> GetByType(Guid typeGuid, int number);
        

    }
}
