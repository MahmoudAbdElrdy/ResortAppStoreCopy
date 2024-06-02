using ResortAppStore.Services.ERPBackEnd.Application.POS.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.POS.Shifts.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.Shifts.Repository
{
    public interface IShiftMasterRepository
    {
        Task<int> DeleteAsync(long id);
        Task<int> DeleteListAsync(List<object> ids);
        Task<ShiftMasterDto> CreateShift(ShiftMasterDto input);
        Task<ShiftMasterDto> UpdateShift(ShiftMasterDto input);
        Task<ShiftMasterDto> FirstInclude(long id);
        Task<ShiftMasterDto> GetShiftDetailsOfDefaultShift();

    }
}

