
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Subscription
{
    public interface IModuleRepo
    {
        Task<List<ModuleDto>> GetAllModule();
        Task<ModuleDto> EditModule(EditModuleCommand request);
        Task<ModuleDto> GetModuleById(long id);
    }
}
