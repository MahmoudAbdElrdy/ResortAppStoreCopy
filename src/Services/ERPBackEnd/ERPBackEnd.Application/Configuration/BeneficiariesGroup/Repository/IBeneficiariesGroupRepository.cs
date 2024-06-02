using Configuration.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Determinants.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Configuration.Repository
{
    public interface  IBeneficiariesGroupRepository
    {
        Task<int> DeleteAsync(long id);
        Task<int> DeleteListAsync(List<object> ids);
        Task<BeneficiariesGroupDto> CreateBeneficiariesGroup(BeneficiariesGroupDto input);
        Task<BeneficiariesGroupDto> UpdateBeneficiariesGroup(BeneficiariesGroupDto input);
        Task<BeneficiariesGroupDto> FirstInclude(long id);
    }
}
