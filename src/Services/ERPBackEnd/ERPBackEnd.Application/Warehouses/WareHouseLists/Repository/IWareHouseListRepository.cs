using Common.Infrastructures;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.JournalsEntries.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Bills.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.WareHouseLists.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.WareHouseLists.Repository
{
    public interface IWareHouseListRepository
    {
        Task<int> DeleteAsync(long id);
        Task<int> DeleteListAsync(List<object> ids);
        Task<WareHouseListDto> FirstInclude(long id);
        Task<PaginatedList<WareHouseListDto>> GetAllList(Paging paging);
        Task<List<WarehouseListsDetailDto>> GetWarehouseListsDetail(WhareHouseListInput input);
        Task<WareHouseListDto> CreateWareHouseList(WareHouseListDto input);
        Task<WareHouseListDto> UpdateWareHouseList(WareHouseListDto input);
        Task<InventoryDynamicDeterminantList> GetDynamicDeterminantList(ItemCard.Dto.InventoryDynamicDeterminantInput input);
    }
}
