using AutoMapper;
using Common.Extensions;
using Common.Infrastructures;
using Common.Interfaces;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Bills.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.WareHouseLists.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.WareHouseLists.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using ResortAppStore.Services.ERPBackEnd.Domain.Inventory;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Warehouses.Entities;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.WarehouseLists.Repository
{
    public class WarehouseListRepository : GMappRepository<InventoryList, WareHouseListDto, long>, IWareHouseListRepository
    {
        private IGRepository<InventoryList> _warehouseList { get; set; }
        private IGRepository<InventoryDynamicDeterminant> _inventoryDynamicDeterminantList { get; set; }

        private IGRepository<ItemCardDeterminant> _itemCardDeterminantRepos { get; set; }
        private IGRepository<InventoryListsDetail> _warehouseListsDetail { get; set; }
        private IAuditService _auditService;
        private IMapper _mpper;

        public WarehouseListRepository(
            IGRepository<InventoryList> mainRepos,
            IAuditService auditService,
            IMapper mapper, DeleteService deleteService,
            IGRepository<InventoryListsDetail> warehouseListsDetail,
            IGRepository<InventoryDynamicDeterminant> inventoryDynamicDeterminantList,
            IGRepository<ItemCardDeterminant> itemCardDeterminantRepos


            )
            : base(mainRepos, mapper, deleteService)
        {
            _warehouseList = mainRepos;
            _auditService = auditService;
            _mpper = mapper;
            _warehouseListsDetail = warehouseListsDetail;
            _inventoryDynamicDeterminantList = inventoryDynamicDeterminantList;
            _itemCardDeterminantRepos = itemCardDeterminantRepos;



        }

        public async Task<PaginatedList<WareHouseListDto>> GetAllList(Paging paging)
        {
            var res = await base.GetAllIncluding(paging, c => c.WarehouseListsDetail);


            return res;
        }
        public async Task<WareHouseListDto> FirstInclude(long id)
        {
            var item = await _warehouseList.GetAll().Include(c => c.WarehouseListsDetail)
                .ThenInclude(c=>c.InventoryDynamicDeterminants).AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == id);


            var result = _mpper.Map<WareHouseListDto>(item);
            return result;
        }
        public async Task<int> DeleteAsync(long id)
        {
            var details = await _warehouseList.GetAllIncluding(c => c.WarehouseListsDetail).FirstOrDefaultAsync(c => c.Id == id);

            if (details.WarehouseListsDetail.Count > 0)
            {
                foreach (var item in details.WarehouseListsDetail)
                {

                    await _warehouseListsDetail.SoftDeleteWithoutSaveAsync(item.Id);
                }
            }

            return await base.ScriptCheckDeleteWithDetails(id, new List<string> { "WarehouseListsDetails" }, "WarehouseLists", "Id");
        }
        public async Task<int> DeleteListAsync(List<object> ids)
        {
            var details = _warehouseList.GetAllIncluding(c => c.WarehouseListsDetail).Where(c => ids.Contains(c.Id)).ToList();

            foreach (var item in details)
            {

                if (item.WarehouseListsDetail.Count > 0)
                {
                    foreach (var itemEntity in item.WarehouseListsDetail)
                    {

                        await _warehouseListsDetail.SoftDeleteWithoutSaveAsync(itemEntity.Id);
                    }
                }
            }

            return await base.ScriptCheckDeleteWithDetails(ids, new List<string> { "WarehouseListsDetails" }, "WarehouseLists", "Id");
        }
        public override string LastCode()
        {
            return base.LastCode();
        }
        public async Task<List<WarehouseListsDetailDto>> GetWarehouseListsDetail(WhareHouseListInput input)
        {
            var list = await _warehouseListsDetail.GetAll().Where(c => c.StoreId == input.StoreId  && input.Ids.Contains(c.WarehouseListId)).ToListAsync();

            var res = _mpper.Map<List<WarehouseListsDetailDto>>(list);
           
            var listAdded =new  List<WarehouseListsDetailDto>();

            var groupedResults = res
      .GroupBy(w => w.ItemId)
      .Select(g => new WarehouseListsDetailDto
      {
          ItemId = (long)(g?.FirstOrDefault().ItemId),
          Quantity = g.Sum(w => w.Quantity ?? 0),
          TotalCostPrice =  g?.FirstOrDefault().TotalCostPrice,
          // Add other properties as needed
          ItemDescription = g?.FirstOrDefault().ItemDescription,
          UnitId =  g?.FirstOrDefault().UnitId,
          Price =  g?.FirstOrDefault().Price,
          StoreId =  g?.FirstOrDefault().StoreId,
          ProjectId =  g?.FirstOrDefault().ProjectId,
          SellingPrice =  g?.FirstOrDefault().SellingPrice,
          MinSellingPrice =  g?.FirstOrDefault().MinSellingPrice,
          BarCode =  g?.FirstOrDefault().BarCode,
          QuantityComputer =  g?.FirstOrDefault().QuantityComputer,
          PriceComputer =  g?.FirstOrDefault().PriceComputer,
          ItemGroupId =  g?.FirstOrDefault().ItemGroupId,
          Notes =  null,
          Total = 0,
          WarehouseListId = g?.FirstOrDefault().WarehouseListId
      })
      .ToList();
            return groupedResults;
        }
        public async Task<WareHouseListDto> CreateWareHouseList(WareHouseListDto input) 
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if (input.TypeWarehouseList == 2)
                {
                    var longNumber = input.WarehouseListIds.Split(",").Select(long.Parse).ToList();
                    var list = await _warehouseList.GetAll().Where(c => c.StoreId == input.StoreId && longNumber.Contains(c.Id)).ToListAsync();
                    foreach(var item in list)
                    {
                        item.IsCollection = true;
                        
                       await _warehouseList.UpdateAsync(item);
                    }
                }
                var entity = _mpper.Map<InventoryList>(input);
                entity.IsActive = true;
                entity.IsDeleted = false;
                
                var result = await base.CreateTEntity(entity);
                input.Id = result.Id;
                scope.Complete();

                return input;
            }
        }
        public async Task<WareHouseListDto> UpdateWareHouseList(WareHouseListDto input) 
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var enityList =await FirstInclude(input.Id.Value);
                if (enityList.WarehouseListsDetail.Count > 0)
                {
                    foreach (var item in enityList.WarehouseListsDetail)
                    {

                        await _warehouseListsDetail.SoftDeleteWithoutSaveAsync(item.Id);
                        if (item.InventoryDynamicDeterminants.Count > 0)
                        {
                            foreach (var item2 in item.InventoryDynamicDeterminants)
                            {
                                await _inventoryDynamicDeterminantList.SoftDeleteWithoutSaveAsync(item2.Id);
                            }

                        }
                    }
                }
                if (input.TypeWarehouseList == 2)
                {
                    var longNumber = input.WarehouseListIds.Split(",").Select(long.Parse).ToList();
                    var list = await _warehouseList.GetAll().Where(c => c.StoreId == input.StoreId && longNumber.Contains(c.Id)).ToListAsync();
                    foreach (var item in list)
                    {
                        item.IsCollection = true;

                        await _warehouseList.UpdateAsync(item);
                    }
                }
                var entity = _mpper.Map<InventoryList>(input);
                entity.IsActive = true;
                entity.IsDeleted = false;

                var result = await _warehouseList.UpdateAsync(entity);
                await _warehouseList.SaveChangesAsync();
                input.Id = result.Id;
                scope.Complete();

                return input;
            }
        }

        public async Task<InventoryDynamicDeterminantList> GetDynamicDeterminantList(ItemCard.Dto.InventoryDynamicDeterminantInput input)
        {
            var resultOld = new InventoryDynamicDeterminantList();

            if (input.ItemCardId != null && input.ItemCardId > 0)
            {
                var itemCardDeterminant = await _itemCardDeterminantRepos.GetAllAsNoTracking().Include(c => c.DeterminantsMaster)
                    .ThenInclude(c => c.DeterminantsDetails)
                   .WhereIf(input.ItemCardId != null && input.ItemCardId > 0, c => c.ItemCardId == input.ItemCardId).ToListAsync();

                resultOld.ItemCardDeterminantListDto = new List<ItemCardDeterminantDto>();

                resultOld.ItemCardDeterminantListDto = _mpper.Map<List<ItemCardDeterminantDto>>(itemCardDeterminant);

                var result = new InventoryDynamicDeterminantList();
                var itemCardDeterminants = await _inventoryDynamicDeterminantList.GetAllIncluding()
                        .Where(c => c.ItemCardId == input.ItemCardId&&c.InventoryListsDetailId==input.InventoryListsDetailId)
                      
                        .ToListAsync();


                var res = _mpper.Map<List<InsertInventoryDynamicDeterminantDto>>(itemCardDeterminants);

                resultOld.DynamicDeterminantListDto = new List<InsertInventoryDynamicDeterminantDto>();
                resultOld.DynamicDeterminantListDto = res;

                return resultOld;
            }
            return resultOld;
        }

    }
}
