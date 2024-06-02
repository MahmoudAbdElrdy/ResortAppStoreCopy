using Common.Infrastructures;
using ResortAppStore.Services.Administration.Application.Settings.GeneralConfigurations.Dto;
using ResortAppStore.Services.Administration.Domain.Entities.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Settings.Settings.Dto
{
    public interface ISettingsRepository
    {
        Task<PaginatedList<SettingDto>> GetAllSetting();
        Task<SettingDto> EditSetting(EditSettingDto request);
        Task<SettingDto> GetById(int Id);

    }
}
