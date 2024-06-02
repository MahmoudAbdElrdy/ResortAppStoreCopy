using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.BeneficiariesGroup.Repository
{
    public interface IBeneficiariesGroupDetailsRepository
    {
        Task<int> DeleteAsync(long id);
        Task<int> DeleteListAsync(List<object> ids);
    }
}
