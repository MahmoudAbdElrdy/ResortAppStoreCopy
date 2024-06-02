using AutoMapper;
using Common.Extensions;
using Common.Interfaces;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.POS.Bills.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Shared.Enums;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.Bills.Repository
{
    public class POSBillRepository : GMappRepository<POSBill, POSBillDto, long>, IPOSBillRepository
    {
        private readonly IGRepository<POSBill> _billRepos;
        private IGRepository<POSBillItem> _billItemRepos { get; set; }
        private IGRepository<POSBillDynamicDeterminant> _posBillDynamicDeterminantRepos { get; set; }
        private IGRepository<ItemCardDeterminant> _itemCardDeterminantRepos { get; set; }

        private IGRepository<POSBillItemTax> _billItemTaxRepos { get; set; }
        private IGRepository<POSBillPaymentDetail> _billPaymentDetailRepos { get; set; }
        private readonly IGRepository<POSBillType> _billTypeRepos;
        private IGRepository<POSTable> _posTableRepos { get; set; }

        private IAuditService _auditService;
        private IMapper _mpper;
        private readonly IGRepository<GeneralConfiguration> _generalConfiguration;
        private readonly IConfiguration _configuration;


        public POSBillRepository(
            IGRepository<POSBill> mainRepos,
            IAuditService auditService,
            IMapper mapper, DeleteService deleteService,
            IGRepository<GeneralConfiguration> generalConfiguration,
            IGRepository<POSBillItem> billItemRepos,
            IGRepository<POSBillDynamicDeterminant> posBillDynamicDeterminantRepos,
             IGRepository<POSBillItemTax> billItemTaxRepos,
            IGRepository<POSBillPaymentDetail> billPaymentDetailRepos,
             IGRepository<POSBillType> billTypeRepos,
            IConfiguration configuration,
            IGRepository<POSTable> posTableRepos,
            IGRepository<ItemCardDeterminant> itemCardDeterminantRepos

            )
            : base(mainRepos, mapper, deleteService)
        {
            _billRepos = mainRepos;
            _auditService = auditService;
            _mpper = mapper;
            _billItemRepos = billItemRepos;
            _posBillDynamicDeterminantRepos = posBillDynamicDeterminantRepos;
            _billItemTaxRepos = billItemTaxRepos;
            _generalConfiguration = generalConfiguration;
            _billPaymentDetailRepos = billPaymentDetailRepos;
            _billTypeRepos = billTypeRepos;
            _configuration = configuration;
            _posTableRepos = posTableRepos;
            _itemCardDeterminantRepos = itemCardDeterminantRepos;


        }

        public async Task<POSBillDto> CreatePOSBill(POSBillDto input)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var billTypeResult = await _billTypeRepos.GetAll().Where(x => x.Id == input.BillTypeId).FirstOrDefaultAsync();

                input.Id = 0;
                double paymentMethodTotal = 0;

                if (input.POSBillItems is not null)
                {
                    foreach (var item in input.POSBillItems)
                    {
                        item.Id = 0;
                        if (item.POSBillItemTaxes is not null)
                        {
                            foreach (var Tax in item.POSBillItemTaxes)
                            {
                                Tax.Id = 0;
                            }
                        }
                        if (item.POSBillDynamicDeterminants is not null)
                        {
                            foreach (var determinantItem in item.POSBillDynamicDeterminants)
                            {
                                determinantItem.Id = 0;
                            }
                        }
                    }
                }
                if (input.POSBillPaymentDetails is not null)
                {
                    foreach (var item in input.POSBillPaymentDetails)
                    {
                        item.Id = 0;
                        paymentMethodTotal += item.Amount;
                    }

                    if (paymentMethodTotal >= input.Total)
                    {
                        input.Paid = true;
                    }
                    else
                    {
                        input.Paid = false;

                    }
                }
                var entity = _mpper.Map<POSBill>(input);
                entity.CreatedBy = _auditService.UserId;
                entity.CreatedAt = DateTime.Now;
                var result = await base.CreateTEntity(entity);

                if (result.POSTableId > 0 && billTypeResult.Kind == (int)POSBillKindEnum.Sales && billTypeResult.Type == (int)POSBillTypeEnum.Restaurant)
                {
                    int? Status;
                    if (paymentMethodTotal < result.Total)
                    {
                        Status = (int)POSTableEnum.Booked;
                    }
                    else
                    {
                        Status = null;
                    }

                    await UpdatePOSTable(result.POSTableId.Value, Status);
                }


                scope.Complete();

                return result;
            }
        }
        public async Task<POSBillDto> UpdatePOSBill(POSBillDto input)
        {
            var billTypeResult = await _billTypeRepos.GetAll().Where(x => x.Id == input.BillTypeId).FirstOrDefaultAsync();

            var billResult = await FirstInclude(input.Id);
            double paymentMethodTotal = 0;


            input.CompanyId = Convert.ToInt64(_auditService.CompanyId);
            input.BranchId = Convert.ToInt64(_auditService.BranchId);


            if (billResult.POSBillItems != null)
            {
                foreach (var item in billResult.POSBillItems.ToList())
                {
                    if (item.POSBillItemTaxes != null)
                    {
                        foreach (var billItemTaxesItem in item.POSBillItemTaxes)
                        {
                            await _billItemTaxRepos.SoftDeleteWithoutSaveAsync(billItemTaxesItem.Id);
                        }
                    }
                    if (item.POSBillDynamicDeterminants != null)
                    {

                        foreach (var determinantItem in item.POSBillDynamicDeterminants)
                        {
                            await _posBillDynamicDeterminantRepos.SoftDeleteAsync(determinantItem.Id);
                        }

                    }

                    await _billItemRepos.SoftDeleteWithoutSaveAsync(item.Id);

                }

            }

            if (billResult.POSBillPaymentDetails != null)
            {
                foreach (var item in billResult.POSBillPaymentDetails.ToList())
                {

                    await _billPaymentDetailRepos.SoftDeleteWithoutSaveAsync(item.Id);
                }
            }


            if (input.POSBillItems is not null)
            {
                foreach (var item in input.POSBillItems)
                {
                    item.Id = 0;
                    if (item.POSBillItemTaxes is not null)
                    {
                        foreach (var Tax in item.POSBillItemTaxes)
                        {
                            Tax.Id = 0;
                        }
                    }
                    if (item.POSBillDynamicDeterminants != null)
                    {
                        foreach (var determinantItem in item.POSBillDynamicDeterminants)
                        {
                            determinantItem.Id = 0;
                        }
                    }
                }
            }
            if (input.POSBillPaymentDetails is not null)
            {
                foreach (var item in input.POSBillPaymentDetails)
                {
                    item.Id = 0;
                    paymentMethodTotal += item.Amount;

                }
                if (paymentMethodTotal >= input.Total)
                {
                    input.Paid = true;
                }
                else
                {
                    input.Paid = false;

                }
            }


            input.CreatedBy = billResult.CreatedBy;
            input.CreatedAt = billResult.CreatedAt;
            input.SystemBillDate = billResult.SystemBillDate;


            input.UpdateBy = _auditService.UserId;
            input.UpdatedAt = DateTime.Now;
            var result = await base.UpdateWithoutCheckCode(input);

            if (result.POSTableId > 0 && billTypeResult.Kind == (int)POSBillKindEnum.Sales && billTypeResult.Type == (int)POSBillTypeEnum.Restaurant)
            {
                int? Status;
                if (paymentMethodTotal < result.Total)
                {
                    Status = (int)POSTableEnum.Booked;
                }
                else
                {
                    Status = null;
                }

                await UpdatePOSTable(result.POSTableId.Value, Status);
            }

            return result;

        }

        public async Task UpdatePOSTable(long POSTableId, int? Status)
        {
            var posTable = await _posTableRepos.FirstOrDefaultAsync(POSTableId);
            if (posTable != null)
            {
                posTable.Status = Status;
                await _posTableRepos.UpdateAsync(posTable);
                await _posTableRepos.SaveChangesAsync();

            }
        }
        public async Task<POSBillDto> FirstInclude(long id)
        {
            var item = await _billRepos.GetAll().Include(c => c.POSBillItems).ThenInclude(c => c.POSBillItemTaxes).
                Include(c => c.POSBillItems).ThenInclude(c => c.POSBillDynamicDeterminants).
                Include(c => c.POSBillPaymentDetails).
                AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            var result = _mpper.Map<POSBillDto>(item);
            return result;
        }
        public async Task<int> DeleteAsync(long id)
        {

            var billResult = await FirstInclude(id);


            if (billResult.POSBillItems != null)
            {
                foreach (var item in billResult.POSBillItems.ToList())
                {
                    if (item.POSBillItemTaxes != null)
                    {
                        foreach (var billItemTaxesItem in item.POSBillItemTaxes)
                        {
                            await _billItemTaxRepos.SoftDeleteWithoutSaveAsync(billItemTaxesItem.Id);
                        }
                    }

                    if (item.POSBillDynamicDeterminants != null)
                    {

                        foreach (var determinantItem in item.POSBillDynamicDeterminants)
                        {
                            await _posBillDynamicDeterminantRepos.SoftDeleteAsync(determinantItem.Id);
                        }

                    }

                    await _billItemRepos.SoftDeleteWithoutSaveAsync(item.Id);

                }

            }

            if (billResult.POSBillPaymentDetails != null)
            {
                foreach (var item in billResult.POSBillPaymentDetails.ToList())
                {

                    await _billPaymentDetailRepos.SoftDeleteWithoutSaveAsync(item.Id);
                }
            }

            if (billResult.POSTableId > 0)
            {
                var posTable = await _posTableRepos.FirstOrDefaultAsync(billResult.POSTableId);
                if (posTable != null)
                {
                    posTable.Status = null;
                    await _posTableRepos.UpdateAsync(posTable);
                    await _posTableRepos.SaveChangesAsync();

                }

            }




            return await base.ScriptCheckDeleteWithDetails(id, new List<string> { "POSBillDynamicDeterminants", "POSBillItemTaxes", "POSBillItems", "POSBillPaymentDetails" }, "POSBills", "Id");

        }
        public async Task<int> DeleteListAsync(List<object> ids)
        {
            foreach (var id in ids)
            {

                var billResult = await FirstInclude(Convert.ToInt64(id));

                if (billResult.POSBillItems != null)
                {
                    foreach (var item in billResult.POSBillItems.ToList())
                    {
                        if (item.POSBillItemTaxes != null)
                        {
                            foreach (var billItemTaxesItem in item.POSBillItemTaxes)
                            {
                                await _billItemTaxRepos.SoftDeleteWithoutSaveAsync(billItemTaxesItem.Id);
                            }
                        }
                        if (item.POSBillDynamicDeterminants != null)
                        {

                            foreach (var determinantItem in item.POSBillDynamicDeterminants)
                            {
                                await _posBillDynamicDeterminantRepos.SoftDeleteAsync(determinantItem.Id);
                            }

                        }
                        await _billItemRepos.SoftDeleteWithoutSaveAsync(item.Id);

                    }

                }

                if (billResult.POSBillPaymentDetails != null)
                {
                    foreach (var item in billResult.POSBillPaymentDetails.ToList())
                    {

                        await _billPaymentDetailRepos.SoftDeleteWithoutSaveAsync(item.Id);
                    }
                }

                if (billResult.POSTableId > 0)
                {
                    var posTable = await _posTableRepos.FirstOrDefaultAsync(billResult.POSTableId);
                    if (posTable != null)
                    {
                        posTable.Status = null;
                        await _posTableRepos.UpdateAsync(posTable);
                        await _posTableRepos.SaveChangesAsync();

                    }

                }



            }
            return await base.ScriptCheckDeleteWithDetails(ids, new List<string> { "POSBillDynamicDeterminants", "POSBillItemTaxes", "POSBillItems", "POSBillPaymentDetails" }, "POSBills", "Id");
        }

        #region Get Last Code  
        public async Task<string> getLastPOSBillCodeByTypeId(long typeId)
        {
            var code = "";
            var CodingPolicy = 0;

            var billTypeResult = await _billTypeRepos.GetAll().Where(x => x.Id == typeId).FirstOrDefaultAsync();

            if(billTypeResult != null)
            {
                CodingPolicy = billTypeResult.CodingPolicy;
            }


            List<POSBill> billsList = new List<POSBill>();

            if(CodingPolicy == (int)CodingPolicyEnum.Automatic)
            {
                billsList = _billRepos.GetAll().Where(x => x.BillTypeId == typeId && x.IsDeleted != true).ToList();
            }
            else if (CodingPolicy == (int)CodingPolicyEnum.AutomaticDependingOnTheUser)
            {
                var userId = _auditService.UserId;
                billsList = _billRepos.GetAll().Where(x => x.BillTypeId == typeId && x.IsDeleted != true && x.CreatedBy == userId).ToList();

            }



            if (billsList.Count > 0 && billsList != null)
            {
                code = billsList.Select(x => x.Code).LastOrDefault().ToString();

                code = (long.Parse(code) + 1).ToString();

            }
            else
            {
                code = "1";
                return code;
            }


            return code;

        }
        #endregion

        public async Task<POSBillDynamicDeterminantList> GetPOSBillDynamicDeterminantList(POSBillDynamicDeterminantInput input)
        {
            var result = new POSBillDynamicDeterminantList();

            if (input.BillItemId != null && input.BillItemId > 0)
            {
                var billDynamicDeterminant = await _posBillDynamicDeterminantRepos.GetAllAsNoTracking()


                    .WhereIf(input.BillItemId != null && input.BillItemId > 0, c => c.BillItemId == input.BillItemId).ToListAsync();

                result.DynamicDeterminantListDto = new List<InsertPOSBillDynamicDeterminantDto>();

                result.DynamicDeterminantListDto = _mpper.Map<List<InsertPOSBillDynamicDeterminantDto>>(billDynamicDeterminant);

                var itemCardDeterminant = await _itemCardDeterminantRepos.GetAllAsNoTracking().Include(c => c.DeterminantsMaster)
                    .ThenInclude(c => c.DeterminantsDetails)
                     .WhereIf(input.ItemCardId != null && input.ItemCardId > 0, c => c.ItemCardId == input.ItemCardId)
                .ToListAsync();

                result.ItemCardDeterminantListDto = new List<ItemCardDeterminantDto>();

                result.ItemCardDeterminantListDto = _mpper.Map<List<ItemCardDeterminantDto>>(itemCardDeterminant);

                return result;

            }
            else if (input.ItemCardId != null && input.ItemCardId > 0)
            {
                var itemCardDeterminant = await _itemCardDeterminantRepos.GetAllAsNoTracking().Include(c => c.DeterminantsMaster)
                    .ThenInclude(c => c.DeterminantsDetails)
                   .WhereIf(input.ItemCardId != null && input.ItemCardId > 0, c => c.ItemCardId == input.ItemCardId).ToListAsync();

                result.ItemCardDeterminantListDto = new List<ItemCardDeterminantDto>();

                result.ItemCardDeterminantListDto = _mpper.Map<List<ItemCardDeterminantDto>>(itemCardDeterminant);

                return result;

            }


            return result;
        }


    }
}
