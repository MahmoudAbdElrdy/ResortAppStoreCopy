using Common.Infrastructures;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.GeneralConfigurations.Dto;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.GeneralConfigurations.Repository
{
    public interface IGeneralConfigurationsRepository
    {
         Task<PaginatedList<GeneralConfigurationDto>> GetAllGeneralConfigurationWithPaginatio(GetAllGeneralConfigurationWithPagination request);
         Task<GeneralConfigurationDto> EditGeneralConfiguration(EditGeneralConfigurationCommand request);
        Task<GeneralConfigurationDto> GetById(int Id);

    }
}
