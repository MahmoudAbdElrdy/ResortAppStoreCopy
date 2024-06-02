using ResortAppStore.Services.ERPBackEnd.Application.POS.Shifts.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.Shifts.Repository
{
    public  interface IShiftDetailsRepository
    {
        Task<int> DeleteAsync(long id);
        Task<int> DeleteListAsync(List<object> ids);
    }
}
