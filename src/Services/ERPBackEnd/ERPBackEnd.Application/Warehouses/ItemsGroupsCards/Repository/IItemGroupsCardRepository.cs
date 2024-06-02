using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemsGroupsCard.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemsGroupsCardDto.Repository 
{
    public interface IItemGroupsCardRepository
    {
        Task<List<ItemGroupsCardTreeDto>> GetTrees(GetAllItemGroupsCardTree request);
        Task<string> GetLastCode(GetLastCode request);
        Task<ItemGroupsCardDto> Create(ItemGroupsCardDto request);
        Task<ItemGroupsCardDto> Update(ItemGroupsCardDto request);
        Task<ItemGroupsCardDto> DeleteItemGroupsCard(DeleteItemGroupsCard request);
    }
}
