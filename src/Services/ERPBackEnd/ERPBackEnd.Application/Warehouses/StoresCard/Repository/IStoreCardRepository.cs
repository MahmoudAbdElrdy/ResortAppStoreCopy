using Common.Constants;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.StoresCard.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.StoresCard.Repository
{
    public interface IStoreCardRepository
    {
        Task<List<StoreCardTreeDto>> GetTrees(GetAllStoreCardTree request);
        Task<string> GetLastCode(GetLastCode request);
        Task<StoreCardDto> Create(StoreCardDto request);
        Task<StoreCardDto> Update(StoreCardDto request);
        Task<StoreCardDto> DeleteStoreCard(DeleteStoreCard request);
        Task<ResponseResult<List<StoreCardDto>>> ExecuteGetStoresInRefernces();
        Task<ResponseResult<List<StoreCardDto>>> ExecuteGetStoresInDocument();
    }
}
