using SaudiEinvoiceService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.IRepos
{
    public interface ITaxBillLineRepos
    {
        IEnumerable<vwTaxBillLine> GetByParentGuid(Guid parentGuid);
        void SetDbName(string dbName);
        void SetDbContext(ResortContext context);
    }
}
