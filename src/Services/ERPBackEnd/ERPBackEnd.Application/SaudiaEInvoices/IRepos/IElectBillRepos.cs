using SaudiEinvoiceService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.IRepos
{
    public interface IElectBillRepos
    {
        bool Add(ElectBill electBill);
        void SetDbName(string dbName);
        ElectBill GetByBillGuid(Guid billGuid);
        List<ElectBill> GetInvoices();
        bool Update(ElectBill electBill);
        void SetDbContext(ResortContext context);

    }
}
