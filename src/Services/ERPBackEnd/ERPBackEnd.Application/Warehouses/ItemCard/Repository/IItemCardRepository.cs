using Common.Constants;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Repository
{
    public interface IItemCardRepository
    {
        Task<ItemCardDto> CreateItemCard(ItemCardDto input);
        Task<ItemCardDto> UpdateItemCard(ItemCardDto input);
        Task<int> DeleteAsync(long id);
        Task<int> DeleteListAsync(List<object> ids);
        Task<ItemCardDto> FirstInclude(long id);
        string getLastCodeByItemGroupId(long itemGroupId);
        Task<bool> CheckHaveDeterminant(long itemCardId);
        Task<ResponseResult<List<CalculateItemCardBalanceDto>>> CalculateItemBalances(long ItemCardId, long StoreId, DateTime? BillDate);

        Task<ResponseResult<List<ItemCardDto>>> ExecuteGetItemsCardInRefernces();
        List<ItemCardDto> GetItemsByItemGroupId(long ItemGroupId);
        List<ItemCardDto> GetPOSItemsByItemSearch(string item);
        List<ItemCardDto> GetPOSItems();
        Task UpdateGPCCode(long ItemGroupId, string GPCCode);
        Task<int> UploadItems(List<object> ids);


    }
}
