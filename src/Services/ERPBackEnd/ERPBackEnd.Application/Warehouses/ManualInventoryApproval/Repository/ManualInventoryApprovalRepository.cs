using AutoMapper;
using Common.Repositories;
using Common.Services.Service;
using MoreLinq.Extensions;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Bills.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ManualInventoryApprovals.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.WareHouseLists.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using ResortAppStore.Services.ERPBackEnd.Infrastructure.Migrations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ManualInventoryApprovals.Repository
{
    public class ManualInventoryApprovalRepository : GMappRepository<ManualInventoryApproval, ManualInventoryApprovalDto, long>, IManualInventoryApprovalRepository
    {
        private readonly IGRepository<ManualInventoryApproval> _manualInventoryApprovalRepos;
        private IBillRepository _billRepository;
        private readonly IGRepository<Bill> _billRepos;

        private readonly IGRepository<GeneralConfiguration> _generalConfiguration;
        private IWareHouseListRepository _warehouseListRepository { get; set; }

        private readonly IGRepository<InventoryListsDetail> _warehouseListsDetailRepos;

        private IMapper _mpper;

        public ManualInventoryApprovalRepository(IGRepository<ManualInventoryApproval> mainRepos, IMapper mapper, DeleteService deleteService,
            IWareHouseListRepository warehouseListRepository,
            IGRepository<Bill> billRepos,
            IGRepository<InventoryListsDetail> warehouseListsDetailRepos,
            IBillRepository billRepository, IGRepository<GeneralConfiguration> generalConfiguration)
            : base(mainRepos, mapper, deleteService)

        {
            _manualInventoryApprovalRepos = mainRepos;
            _billRepository = billRepository;
            _billRepos = billRepos;
            _generalConfiguration = generalConfiguration;
            _warehouseListRepository = warehouseListRepository;
            _warehouseListsDetailRepos = warehouseListsDetailRepos;
            _mpper = mapper;

        }
        public async Task<ManualInventoryApprovalDto> CreateManualInventoryApproval(ManualInventoryApprovalDto input)
        {

            var entity = _mpper.Map<ManualInventoryApproval>(input);
            var result = await base.CreateTEntity(entity);

            await addBill(input, result.Id);

            return input;
        }
        public async Task<ManualInventoryApprovalDto> UpdateManualInventoryApproval(ManualInventoryApprovalDto input)
        {

            var billResult = _billRepos.GetAll().Where(c => c.ManualInventoryApprovalId == input.Id && c.IsDeleted != true).ToList();
            if (billResult != null)
            {
                foreach (var item in billResult)
                {
                    await _billRepository.DeleteAsync(item.Id);

                }
            }
            var result = await base.UpdateWithoutCheckCode(input);

            await addBill(input, result.Id);

            return input;


        }

        public async Task addBill(ManualInventoryApprovalDto input, long ManualInventoryApprovalId)
        {

            BillDto bill = new BillDto();
            List<BillItemDto> inputBillItem = new List<BillItemDto>();
            List<BillItemDto> outputBillItem = new List<BillItemDto>();
            double totalInputBill = 0;
            double totalOutputBill = 0;


            if (input.WarehouseListId > 0)
            {
                var inventory = await _warehouseListRepository.FirstInclude(input.WarehouseListId);
                if (inventory != null && inventory.WarehouseListsDetail.Where(x => x.IsApproved != true).ToList().Count > 0)
                {
                    foreach (var item in inventory.WarehouseListsDetail.Where(x => x.IsApproved != true).ToList())
                    {
                        List<InsertBillDynamicDeterminantDto> BillDynamicDeterminants = new List<InsertBillDynamicDeterminantDto>();
                        if (item.InventoryDynamicDeterminants != null && item.InventoryDynamicDeterminants.Count > 0)
                        {
                            foreach (var determinantItem in item.InventoryDynamicDeterminants)
                            {
                                List<DeterminantDataDto> DeterminantDataList = new List<DeterminantDataDto>();

                                if (determinantItem.DeterminantsData != null && determinantItem.DeterminantsData.Count > 0)
                                {
                                    foreach (var DeterminantsDataItem in determinantItem.DeterminantsData)
                                    {
                                        DeterminantDataList.Add(new DeterminantDataDto
                                        {
                                            BillDynamicDeterminantSerial = DeterminantsDataItem.InventoryListDynamicDeterminantSerial,
                                            DeterminantId = DeterminantsDataItem.DeterminantId,
                                            Value = DeterminantsDataItem.Value,
                                            ValueType = DeterminantsDataItem.ValueType

                                        });
                                    }
                                }

                                List<DeterminantValueDto> DeterminantValueList = new List<DeterminantValueDto>();


                                if (determinantItem.DeterminantsValue != null && determinantItem.DeterminantsValue.Count > 0)
                                {
                                    foreach (var DeterminantsValueItem in determinantItem.DeterminantsValue)
                                    {
                                        DeterminantValueList.Add(new DeterminantValueDto
                                        {
                                            DeterminantId = DeterminantsValueItem.DeterminantId,
                                            Value = DeterminantsValueItem.Value,



                                        });
                                    }
                                }

                                BillDynamicDeterminants.Add(
                                    new InsertBillDynamicDeterminantDto
                                    {
                                        BillItemId = 0,
                                        DeterminantsData = DeterminantDataList,
                                        IssuedQuantity = determinantItem.IssuedQuantity,
                                        AddedQuantity = determinantItem.AddedQuantity,
                                        ConvertedAddedQuantity = determinantItem.ConvertedAddedQuantity,
                                        ConvertedIssuedQuantity = determinantItem.ConvertedIssuedQuantity,
                                        DeterminantsValue = DeterminantValueList,
                                        ItemCardId = determinantItem.ItemCardId,
                                    }
                                    );


                            }

                        }

                        if (item.Quantity > item.QuantityComputer)
                        {


                            inputBillItem.Add(new BillItemDto
                            {
                                Id = 0,
                                BillId = 0,
                                ItemId = item.ItemId,
                                ItemDescription = item.ItemDescription,
                                UnitId = item.UnitId,
                                UnitTransactionFactor = 1,
                                AddedQuantity = item.Quantity - item.QuantityComputer,
                                ConvertedAddedQuantity = item.Quantity - item.QuantityComputer,
                                Price = (double)item.PriceComputer,
                                Total = (item.Quantity - item.QuantityComputer) * item.PriceComputer,
                                StoreId = (long)item.StoreId,
                                ProjectId = item.ProjectId,
                                TotalCostPrice = (item.Quantity - item.QuantityComputer) * item.PriceComputer,
                                BillDynamicDeterminants = BillDynamicDeterminants



                            });
                            totalInputBill += (item.Quantity.Value - item.QuantityComputer.Value) * item.PriceComputer.Value;

                            item.IsApproved = true;

                            var entity = _mpper.Map<InventoryListsDetail>(item);

                            await _warehouseListsDetailRepos.UpdateAsync(entity);
                            await _warehouseListsDetailRepos.SaveChangesAsync();






                        }
                        if (item.Quantity < item.QuantityComputer)
                        {

                            outputBillItem.Add(new BillItemDto
                            {
                                Id = 0,
                                BillId = 0,
                                ItemId = item.ItemId,
                                ItemDescription = item.ItemDescription,
                                UnitId = item.UnitId,
                                UnitTransactionFactor = 1,
                                IssuedQuantity = item.QuantityComputer - item.Quantity,
                                ConvertedIssuedQuantity = item.QuantityComputer - item.Quantity,
                                Price = (double)item.PriceComputer,
                                Total = (item.QuantityComputer - item.Quantity) * item.PriceComputer,
                                StoreId = (long)item.StoreId,
                                ProjectId = item.ProjectId,
                                TotalCostPrice = (item.QuantityComputer - item.Quantity) * item.PriceComputer,
                                BillDynamicDeterminants = BillDynamicDeterminants



                            });

                            totalOutputBill += (item.QuantityComputer.Value - item.Quantity.Value) * item.PriceComputer.Value;
                            item.IsApproved = true;

                            var entity = _mpper.Map<InventoryListsDetail>(item);

                            await _warehouseListsDetailRepos.UpdateAsync(entity);
                            await _warehouseListsDetailRepos.SaveChangesAsync();


                        }
                    }
                }

                if (input.InputBillTypeId > 0 && inputBillItem.Count > 0)
                {
                    var inputBillCode = await _billRepository.getLastBillCodeByTypeId(input.InputBillTypeId.Value);

                    bill.Code = inputBillCode;
                    bill.CompanyId = input.CompanyId;
                    bill.BranchId = input.BranchId;
                    bill.BillTypeId = input.InputBillTypeId.Value;
                    bill.FiscalPeriodId = input.FiscalPeriodId;
                    bill.Date = input.Date;
                    bill.CurrencyId = inventory.CurrencyId;
                    bill.CurrencyValue = inventory.CurrencyValue;
                    bill.StoreId = inventory.StoreId.Value;
                    bill.Total = totalInputBill;
                    bill.PostToWarehouses = true;
                    bill.ManualInventoryApprovalId = ManualInventoryApprovalId;

                    bill.BillItems = inputBillItem;

                    await _billRepository.CreateBill(bill);


                }
                if (input.OutputBillTypeId > 0 && outputBillItem.Count > 0)
                {
                    var outputBillCode = await _billRepository.getLastBillCodeByTypeId(input.OutputBillTypeId.Value);

                    bill.Code = outputBillCode;
                    bill.CompanyId = input.CompanyId;
                    bill.BranchId = input.BranchId;
                    bill.BillTypeId = input.OutputBillTypeId.Value;
                    bill.FiscalPeriodId = input.FiscalPeriodId;
                    bill.Date = input.Date;
                    bill.CurrencyId = inventory.CurrencyId;
                    bill.CurrencyValue = inventory.CurrencyValue;
                    bill.StoreId = inventory.StoreId.Value;
                    bill.Total = totalOutputBill;
                    bill.PostToWarehouses = true;
                    bill.ManualInventoryApprovalId = ManualInventoryApprovalId;

                    bill.BillItems = outputBillItem;

                    await _billRepository.CreateBill(bill);


                }

            }

        }

        public async Task<int> DeleteAsync(long id)
        {
            var billResult = _billRepos.GetAll().Where(c => c.ManualInventoryApprovalId == id && c.IsDeleted != true).ToList();
            if (billResult != null)
            {
                foreach (var item in billResult)
                {
                    await _billRepository.DeleteAsync(item.Id);

                }
            }
            return await base.SoftDeleteAsync(id, "ManualInventoryApprovals", "Id");


        }
        public async Task<int> DeleteListAsync(List<object> ids)
        {
            foreach (var id in ids)
            {
                var billResult = _billRepos.GetAll().Where(c => c.ManualInventoryApprovalId == (long)id && c.IsDeleted != true).ToList();
                if (billResult != null)
                {
                    foreach (var item in billResult)
                    {
                        await _billRepository.DeleteAsync(item.Id);

                    }
                }
            }
            return await base.SoftDeleteListAsync(ids, "ManualInventoryApprovals", "Id");

        }
    }
}
