using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Determinants.Repository
{
    public interface  IDeterminantsDetailsRepository
    {
        Task<int> DeleteAsync(long id);
        Task<int> DeleteListAsync(List<object> ids);
    }
}
