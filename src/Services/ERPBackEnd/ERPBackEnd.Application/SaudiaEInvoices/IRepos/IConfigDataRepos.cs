using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaudiEinvoiceService.IRepos
{
    public interface IConfigDataRepos
    {
        List<string> GetDatabases(string conStr);
    }
}
