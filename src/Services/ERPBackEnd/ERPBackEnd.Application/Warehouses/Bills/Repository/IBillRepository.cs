using Common.Constants;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Bills.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.WareHouseLists.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Bills.Repository
{
    public interface IBillRepository
    {
        Task<BillDto> CreateBill(BillDto input);
        Task<BillDto> UpdateBill(BillDto input);
        Task<int> DeleteAsync(long id);
        Task<int> DeleteListAsync(List<object> ids);
        Task<BillDto> FirstInclude(long id);
        Task<string> getLastBillCodeByTypeId(long typeId);
        Task<ResponseResult> generateEntry(List<long> ids);
        Task<ResponseResult<List<NotGenerateEntryBillDto>>> ExecuteGetNotGeneratedEntryBills();
        Task<ResponseResult<List<NotPostToWarehousesAutomaticallyBillDto>>> ExecuteGetNotPostToWarehousesAutomaticallyBills();
        Task postToWarehouses(List<long> ids);
        Task<BillDynamicDeterminantList> GetBillDynamicDeterminantList(BillDynamicDeterminantInput input);
        Task<ResponseResult<List<BillPaymentDto>>> GetBillPayments( int? billKind, long? customerId, long? supplierId,long? voucherTypeId);
        Task<ResponseResult<List<BillPaymentDto>>> GetBillPaid(int? billKind, long? customerId, long? supplierId);
        Task<bool> CheckQuantity(List<InsertBillDynamicDeterminantDto> billDynamicDeterminants);
        Task<ResponseResult<List<BillPaymentDto>>> GetInstallmentPaid(int? billKind, long? customerId, long? supplierId);
        Task<WarehouseListsDetailDto> GetBillItemByItemId(WhareHouseInput input);
        Task<List<WarehouseListsDetailDto>> GetBillItemByStoreId(long storeId, DateTime ?  billDate);
        Task<List<WarehouseListsDetailDto>> GetItemsByItemCodes(WhareHouseListInputCode input);
        Task<BillDynamicDeterminantList> GetDynamicDeterminantList(ItemCard.Dto.InventoryDynamicDeterminantInput input);
        Task<ResponseResult<List<UnSyncedElectronicBillsDto>>> GetUnsyncedElectronicBills();

    }
}
