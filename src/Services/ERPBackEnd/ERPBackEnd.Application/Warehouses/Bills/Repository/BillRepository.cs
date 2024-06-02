using AutoMapper;
using Common.Constants;
using Common.Extensions;
using Common.Interfaces;
using Common.Repositories;
using Common.Services.Service;
using Egypt_EInvoice_Api.EInvoiceModel;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.JournalsEntries.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.VoucherTypes.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.EInvoices.BLL;
using ResortAppStore.Services.ERPBackEnd.Application.Shared.Enums;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Bills.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.ItemCard.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.WareHouseLists.Dto;
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
using Bill = ResortAppStore.Services.ERPBackEnd.Domain.Warehouses.Bill;
using BillDynamicDeterminant = ResortAppStore.Services.ERPBackEnd.Domain.Warehouses.BillDynamicDeterminant;
using BillItem = ResortAppStore.Services.ERPBackEnd.Domain.Warehouses.BillItem;
using BillType = ResortAppStore.Services.ERPBackEnd.Domain.Warehouses.BillType;

namespace ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Bills.Repository
{
    public class BillRepository : GMappRepository<Bill, BillDto, long>, IBillRepository
    {
        private readonly IGRepository<Bill> _billRepos;
        private IGRepository<BillItem> _billItemRepos { get; set; }
        private IGRepository<BillItemTax> _billItemTaxRepos { get; set; }
        private IGRepository<BillAdditionAndDiscount> _billAdditionAndDiscountRepos { get; set; }
        private IGRepository<BillPaymentDetail> _billPaymentDetailsRepos { get; set; }

        private readonly IGRepository<BillType> _billTypeRepos;
        private IGRepository<JournalEntriesMaster> _journalEntriesRepository { get; set; }
        private IGRepository<CustomerCard> _customerCardRepository { get; set; }
        private IGRepository<SupplierCard> _supplierCardRepository { get; set; }
        private IJournalEntriesDetailsRepository _journalEntriesDetailRepos;
        private IGRepository<JournalEntriesDetail> _journalEntriesDetailRepository { get; set; }
        private IGRepository<Domain.Warehouses.ItemCard> _itemCardRepos { get; set; }
        private IItemCardRepository _itemCardRepository;
        private IGRepository<ItemCardDeterminant> _itemCardDeterminantRepos { get; set; }
        private IGRepository<BillDynamicDeterminant> _billDynamicDeterminantRepos { get; set; }
        private readonly IGRepository<Voucher> _voucherRepos;
        private IVoucherRepository _voucherRepository;
        private IVoucherTypeRepository _voucherTypeRepository;
        private IAuditService _auditService;
        private IMapper _mpper;
        private readonly IGRepository<GeneralConfiguration> _generalConfiguration;
        private readonly IDapperRepository<CalculateCostPriceDto> _query;
        private IDapperRepository<DeterminantQueryOutput> _determinantQuery;
        private IGRepository<TaxMaster> _taxMasterRepository { get; set; }
        private IGRepository<BillInstallmentDetail> _billInstallmentDetailRepos { get; set; }
        private IGRepository<ItemCardBalance> _itemCardBalanceRepos { get; set; }
        private readonly string _connectionString;
        private readonly IConfiguration _configuration;
        private IGRepository<InventoryDynamicDeterminant> _inventorylDynamicDeterminantRepos { get; set; }
        private IGRepository<PaymentMethod> _paymentMethodRepos { get; set; }
        public IEInvoiceGovManager _invoiceGovManager { get; set; }


        public BillRepository(
            IGRepository<Bill> mainRepos,
            IAuditService auditService,
            IMapper mapper, DeleteService deleteService,
            IGRepository<GeneralConfiguration> generalConfiguration,
            IGRepository<BillItem> billItemRepos,
             IGRepository<BillItemTax> billItemTaxRepos,
            IGRepository<BillAdditionAndDiscount> billAdditionAndDiscountRepos,
            IGRepository<BillPaymentDetail> billPaymentDetailsRepos,
             IGRepository<BillType> billTypeRepos,
             IGRepository<JournalEntriesMaster> journalEntriesRepository,
             IGRepository<CustomerCard> customerCardRepository,
             IGRepository<SupplierCard> supplierCardRepository,
             IJournalEntriesDetailsRepository journalEntriesDetailRepos,
             IGRepository<Domain.Warehouses.ItemCard> itemCardRepos,
             IGRepository<TaxMaster> taxMasterRepository,
              IGRepository<JournalEntriesDetail> journalEntriesDetailRepository,
              IItemCardRepository itemCardRepository,
               IDapperRepository<CalculateCostPriceDto> query,
               IGRepository<Voucher> voucherRepos,
               IVoucherTypeRepository voucherTypeRepository,
               IDapperRepository<DeterminantQueryOutput> determinantQuery,

            IGRepository<ItemCardDeterminant> itemCardDeterminantRepos,
            IGRepository<BillDynamicDeterminant> billDynamicDeterminantRepos,
            IGRepository<BillInstallmentDetail> billInstallmentDetailRepos,
            IGRepository<ItemCardBalance> itemCardBalanceRepos,
            IConfiguration configuration,
            IGRepository<InventoryDynamicDeterminant> inventorylDynamicDeterminantRepos,
            IGRepository<PaymentMethod> paymentMethodRepos,
            IEInvoiceGovManager invoiceGovManager

            )
            : base(mainRepos, mapper, deleteService)
        {
            _billRepos = mainRepos;
            _auditService = auditService;
            _mpper = mapper;
            _billItemRepos = billItemRepos;
            _billItemTaxRepos = billItemTaxRepos;
            _generalConfiguration = generalConfiguration;
            _billAdditionAndDiscountRepos = billAdditionAndDiscountRepos;
            _billPaymentDetailsRepos = billPaymentDetailsRepos;
            _billTypeRepos = billTypeRepos;
            _journalEntriesRepository = journalEntriesRepository;
            _customerCardRepository = customerCardRepository;
            _supplierCardRepository = supplierCardRepository;
            _journalEntriesDetailRepos = journalEntriesDetailRepos;
            _itemCardRepos = itemCardRepos;
            _taxMasterRepository = taxMasterRepository;
            _journalEntriesDetailRepository = journalEntriesDetailRepository;
            _itemCardRepository = itemCardRepository;
            _query = query;
            _itemCardDeterminantRepos = itemCardDeterminantRepos;
            _voucherRepos = voucherRepos;
            _voucherTypeRepository = voucherTypeRepository;
            _billDynamicDeterminantRepos = billDynamicDeterminantRepos;
            _determinantQuery = determinantQuery;
            _billInstallmentDetailRepos = billInstallmentDetailRepos;
            _itemCardBalanceRepos = itemCardBalanceRepos;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
            _inventorylDynamicDeterminantRepos = inventorylDynamicDeterminantRepos;
            _paymentMethodRepos = paymentMethodRepos;
            _invoiceGovManager = invoiceGovManager;
        }

        public async Task<BillDto> CreateBill(BillDto input)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                input.Id = 0;
               

                if (input.BillItems is not null)
                {
                    foreach (var item in input.BillItems)
                    {
                        item.Id = 0;
                        if (item.BillItemTaxes is not null)
                        {
                            foreach (var itemTax in item.BillItemTaxes)
                            {
                                itemTax.Id = 0;
                            }
                        }
                        if (item.BillDynamicDeterminants is not null)
                        {
                            foreach (var determinantItem in item.BillDynamicDeterminants)
                            {
                                determinantItem.Id = 0;
                            }
                        }

                    }
                }

                if (input.BillAdditionAndDiscounts is not null)
                {
                    foreach (var item in input.BillAdditionAndDiscounts)
                    {
                        item.Id = 0;

                    }
                }


                if (input.BillPaymentDetails is not null)
                {
                    foreach (var item in input.BillPaymentDetails)
                    {
                        item.Id = 0;

                    }
                }


                if (input.BillInstallmentDetails is not null)
                {
                    foreach (var item in input.BillInstallmentDetails)
                    {
                        item.Id = 0;

                    }
                }
                var entity = _mpper.Map<Bill>(input);
                entity.CreatedBy = _auditService.UserId;
                entity.CreatedAt = DateTime.Now;
                var result = await base.CreateTEntity(entity);
                await doRelations(result);
                scope.Complete();

                return result;
            }
        }
        public async Task<BillDto> UpdateBill(BillDto input)
        {
            var billTypeResult = await _billTypeRepos.GetAll().Where(x => x.Id == input.BillTypeId).FirstOrDefaultAsync();

            var billResult = await FirstInclude(input.Id);

            //using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            //{
            int? parentType = 0;
            if (billTypeResult.Kind != (int)BillKindEnum.FirstPeriodGoods)
            {
                if (billTypeResult.Kind == (int)BillKindEnum.Sales)
                {
                    parentType = (int)EntryTypesEnum.SalesBill;
                }

                if (billTypeResult.Kind == (int)BillKindEnum.SalesReturn)
                {
                    parentType = (int)EntryTypesEnum.SalesReturnBill;
                }
                if (billTypeResult.Kind == (int)BillKindEnum.Purchases)
                {
                    parentType = (int)EntryTypesEnum.PurchasesBill;
                }

                if (billTypeResult.Kind == (int)BillKindEnum.PurchasesReturn)
                {
                    parentType = (int)EntryTypesEnum.PurchasesReturnBill;
                }
                var journalEntriesItem = await _journalEntriesRepository.GetAllIncluding(c => c.JournalEntriesDetail).AsNoTracking().FirstOrDefaultAsync(c => c.ParentType == parentType & c.ParentTypeId == input.Id && c.IsDeleted != true);

                if (journalEntriesItem != null)
                {
                    foreach (var item in journalEntriesItem.JournalEntriesDetail.ToList())
                    {

                        await _journalEntriesDetailRepository.SoftDeleteWithoutSaveAsync(item.Id);
                    }
                    await _journalEntriesRepository.SoftDeleteWithoutSaveAsync(journalEntriesItem.Id);
                }
            }

            input.CompanyId = Convert.ToInt64(_auditService.CompanyId);
            input.BranchId = Convert.ToInt64(_auditService.BranchId);


            if (billResult.BillItems != null)
            {
                foreach (var item in billResult.BillItems.ToList())
                {
                    if (item.BillItemTaxes != null)
                    {
                        foreach (var billItemTaxesItem in item.BillItemTaxes)
                        {
                            await _billItemTaxRepos.SoftDeleteWithoutSaveAsync(billItemTaxesItem.Id);
                        }
                    }
                    if (item.BillDynamicDeterminants != null)
                    {

                        foreach (var determinantItem in item.BillDynamicDeterminants)
                        {
                            await _billDynamicDeterminantRepos.SoftDeleteAsync(determinantItem.Id);
                        }

                    }
                    await _billItemRepos.SoftDeleteWithoutSaveAsync(item.Id);



                }



            }

            if (billResult.BillAdditionAndDiscounts != null)
            {
                foreach (var item in billResult.BillAdditionAndDiscounts.ToList())
                {

                    await _billAdditionAndDiscountRepos.SoftDeleteWithoutSaveAsync(item.Id);
                }
            }

            if (billResult.BillPaymentDetails != null)
            {
                foreach (var item in billResult.BillPaymentDetails.ToList())
                {

                    await _billPaymentDetailsRepos.SoftDeleteWithoutSaveAsync(item.Id);
                }
            }

            if (input.BillItems != null)
            {
                foreach (var item in input.BillItems)
                {
                    item.Id = 0;
                    if (item.BillItemTaxes != null)
                    {
                        foreach (var itemTax in item.BillItemTaxes)
                        {
                            itemTax.Id = 0;
                        }
                    }
                    if (item.BillDynamicDeterminants != null)
                    {
                        foreach (var determinantItem in item.BillDynamicDeterminants)
                        {
                            determinantItem.Id = 0;
                        }
                    }

                }
            }

            if (input.BillAdditionAndDiscounts != null)
            {
                foreach (var item in input.BillAdditionAndDiscounts)
                {
                    item.Id = 0;

                }
            }

            if (input.BillPaymentDetails is not null)
            {
                foreach (var item in input.BillPaymentDetails)
                {
                    item.Id = 0;

                }
            }
            if (billResult.BillInstallmentDetails != null)
            {
                foreach (var item in billResult.BillInstallmentDetails.ToList())
                {

                    await _billInstallmentDetailRepos.SoftDeleteWithoutSaveAsync(item.Id);
                }
            }

            if (input.BillInstallmentDetails != null)
            {
                foreach (var item in input.BillInstallmentDetails)
                {
                    item.Id = 0;

                }
            }
            input.CreatedBy = billResult.CreatedBy;
            input.CreatedAt = billResult.CreatedAt;
            input.UpdateBy = _auditService.UserId;
            input.UpdatedAt = DateTime.Now;
            var result = await base.UpdateWithoutCheckCode(input);
            await doRelations(result);


            return result;

        }

        public async Task doRelations(BillDto result)
        {
            if (result.Id > 0)
            {
                var billTypeResult = await _billTypeRepos.GetAll().Where(x => x.Id == result.BillTypeId).FirstOrDefaultAsync();

                if (result.BillItems.Count() > 0)
                {
                    foreach (var item in result.BillItems)
                    {
                        var itemCardBalances = await _itemCardBalanceRepos.GetAll().Where(x => x.StoreId == item.StoreId && x.ItemCardId == item.ItemId).FirstOrDefaultAsync();
                        if (itemCardBalances == null)
                        {
                            ItemCardBalance itemCardBalanceDto = new ItemCardBalance();
                            itemCardBalanceDto.ItemCardId = item.ItemId;
                            itemCardBalanceDto.StoreId = item.StoreId;

                            await _itemCardBalanceRepos.InsertAsync(itemCardBalanceDto);
                            await _itemCardBalanceRepos.SaveChangesAsync();



                        }

                    }

                }



                await UpdateCostPriceForItems(result, billTypeResult);
                List<long> lst = new List<long>();

                if (billTypeResult.AccountingEffect == (int)CreateFinancialEntryEnum.CreateTheEntryAutomatically && result.FiscalPeriodId > 0)
                {
                    lst.Add(result.Id);
                    await generateEntry(lst);
                }
                //if (result.PayWay == (int)PayWaysEnum.Cash && BillTypeResult.IsGenerateVoucherIfPayWayIsCash == true)
                //{
                //  //  await generateVoucher(result,1);

                //}
                //if (BillTypeResult.PayTheAdvancePayments == true && result.FirstPayment != 0 && result.FirstPayment!=null && result.PaymentDate.HasValue && result.PaymentDate.Value.Date <= DateTime.UtcNow.Date )
                //{
                //    await generateVoucher(result,2);
                //}
            }
        }
        public async Task generateVoucher(BillDto result, int type)
        {
            try
            {

                int beneficiaryTypeId = 0;
                long beneficiaryId = 0;
                string? beneficiaryAccountId;
                double credit = 0;
                double debit = 0;
                double creditLocal = 0;
                double debitLocal = 0;

                var _billType = await _billTypeRepos.FirstOrDefaultAsync(x => x.Id == result.BillTypeId);


                var voucherTypeId = type == 1 ? (long)_billType.VoucherTypeIdOfPayWayIsCash : (long)_billType.VoucherTypeIdOfAdvancePayments;


                var voucherType = await _voucherTypeRepository.FirstInclude(voucherTypeId);

                var voucherCode = await _voucherRepository.getLastVoucherCodeByTypeId(voucherTypeId);

                VoucherDto voucherDto = new VoucherDto();
                voucherDto.CompanyId = result.CompanyId;
                voucherDto.BranchId = result.BranchId;
                voucherDto.VoucherTypeId = voucherTypeId;
                voucherDto.Code = voucherCode;
                voucherDto.VoucherDate = result.Date;
                if (result.CashAccountId != null)
                {
                    voucherDto.CashAccountId = long.Parse(result.CashAccountId);

                }
                voucherDto.CostCenterId = result.CostCenterId;
                voucherDto.ProjectId = result.ProjectId;
                voucherDto.CurrencyId = result.CurrencyId.Value;
                voucherDto.CurrencyFactor = result.CurrencyValue.Value;
                voucherDto.VoucherTotal = (double)result.FirstPayment;
                voucherDto.VoucherTotalLocal = (double)result.FirstPayment * result.CurrencyValue.Value;
                voucherDto.FiscalPeriodId = result.FiscalPeriodId;
                voucherDto.PaymentType = (int)result.PayWay;
                voucherDto.SalesPersonId = result.SalesPersonId;


                if (result.CustomerId > 0)
                {
                    var Customer = await _customerCardRepository.GetAll().Where(X => X.Id == result.CustomerId).FirstOrDefaultAsync();
                    beneficiaryTypeId = (int)BeneficiaryTypeEnum.Client;
                    beneficiaryId = result.CustomerId.Value;
                    beneficiaryAccountId = Customer.AccountId;


                }
                else if (result.SupplierId > 0)
                {
                    var Supplier = await _supplierCardRepository.GetAll().Where(X => X.Id == result.SupplierId).FirstOrDefaultAsync();
                    beneficiaryTypeId = (int)BeneficiaryTypeEnum.Supplier;
                    beneficiaryId = result.SupplierId.Value;
                    beneficiaryAccountId = Supplier.AccountId;

                }
                else
                {
                    beneficiaryTypeId = (int)BeneficiaryTypeEnum.Account;
                    beneficiaryId = long.Parse(result.CashAccountId);
                    beneficiaryAccountId = result.CashAccountId;



                }
                if (voucherType.VoucherKindId == (int)VoucherTypesEnum.SimpleDeposit || voucherType.VoucherKindId == (int)VoucherTypesEnum.Deposit)
                {
                    credit = result.Net.Value;
                    creditLocal = result.Net.Value * result.CurrencyValue.Value;

                }
                if (voucherType.VoucherKindId == (int)VoucherTypesEnum.Withdrawal || voucherType.VoucherKindId == (int)VoucherTypesEnum.SimpleWithdrawal)
                {
                    debit = result.Net.Value;
                    debitLocal = result.Net.Value * result.CurrencyValue.Value;
                }



                voucherDto.VoucherDetail.Add(new VoucherDetailDto
                {
                    BeneficiaryTypeId = beneficiaryTypeId,
                    BeneficiaryId = beneficiaryId,
                    BeneficiaryAccountId = beneficiaryAccountId,
                    Credit = credit,
                    CreditLocal = creditLocal,
                    Debit = debit,
                    DebitLocal = debitLocal,
                    CurrencyId = result.CurrencyId,
                    CurrencyConversionFactor = result.CurrencyValue,
                    CostCenterId = result.CostCenterId.Value,
                    ProjectId = result.ProjectId.Value


                });

                var voucherResult = await _voucherRepository.CreateVoucher(voucherDto);

            }
            catch (Exception ex)
            {

                throw ex.InnerException;
            }



        }
        public async Task UpdateCostPriceForItems(BillDto result, BillType billTypeResult)
        {
            if (billTypeResult.AffectOnCostPrice == true && (billTypeResult.Kind == (int)BillKindEnum.Purchases || billTypeResult.Kind == (int)BillKindEnum.PurchasesReturn || billTypeResult.Kind == (int)BillKindEnum.Transferred || billTypeResult.Kind == (int)BillKindEnum.AddonSettlement || billTypeResult.Kind == (int)BillKindEnum.DiscountSettlement) || billTypeResult.Kind == (int)BillKindEnum.FirstPeriodGoods)
            {

                if (result.BillItems != null)
                {
                    foreach (var item in result.BillItems)
                    {
                        var itemCard = await _itemCardRepos.GetAllIncluding(c => c.ItemCardUnits).Include(c => c.ItemCardAlternatives).Include(c => c.ItemCardDeterminants).Include(c => c.ItemCardBalances).AsNoTracking().FirstOrDefaultAsync(c => c.Id == item.ItemId);

                        double costPrice = 0;

                        if (itemCard != null)
                        {
                            if (itemCard.CostCalculateMethod == (int)CostCalculateMethodsEnum.OpeningPrice)
                            {
                                UpdateCostPrice(itemCard.Id, itemCard.OpeningCostPrice.Value);

                                if (item.StoreId > 0 && item.ItemId > 0)
                                {
                                    var itemCardBalances = await _itemCardBalanceRepos.GetAll().Where(x => x.StoreId == item.StoreId && x.ItemCardId == item.ItemId).FirstOrDefaultAsync();


                                    if (itemCardBalances.OpeningCostPrice != null)
                                    {
                                        itemCardBalances.CostPrice = itemCardBalances.OpeningCostPrice.Value;

                                    }
                                    else
                                    {
                                        itemCardBalances.CostPrice = 0;
                                    }
                                    await _itemCardBalanceRepos.UpdateAsync(itemCardBalances);
                                    await _itemCardBalanceRepos.SaveChangesAsync();
                                }

                            }
                            else if (itemCard.CostCalculateMethod == (int)CostCalculateMethodsEnum.ActualAverage || itemCard.CostCalculateMethod == (int)CostCalculateMethodsEnum.TheHighestPrice)
                            {

                                int? costCalculateMethod = itemCard.CostCalculateMethod;
                                List<CalculateCostPriceDto> _CalculateCostPrice = await CalculateCostPrice(itemCard.Id, null, costCalculateMethod.Value);
                                if (_CalculateCostPrice[0].CostPrice != null)
                                {
                                    itemCard.CostPrice = _CalculateCostPrice[0].CostPrice.Value;

                                }
                                else
                                {
                                    itemCard.CostPrice = 0;
                                }

                                UpdateCostPrice(itemCard.Id, itemCard.CostPrice.Value);

                                if (item.StoreId > 0 && item.ItemId > 0)
                                {
                                    var itemCardBalances = await _itemCardBalanceRepos.GetAll().Where(x => x.StoreId == item.StoreId && x.ItemCardId == item.ItemId).FirstOrDefaultAsync();

                                    List<CalculateCostPriceDto> calculateCostPriceForStore = await CalculateCostPrice(itemCard.Id, item.StoreId, costCalculateMethod.Value);

                                    if (calculateCostPriceForStore[0].CostPrice != null)
                                    {
                                        itemCardBalances.CostPrice = calculateCostPriceForStore[0].CostPrice.Value;

                                    }
                                    else
                                    {
                                        itemCardBalances.CostPrice = 0;
                                    }
                                    await _itemCardBalanceRepos.UpdateAsync(itemCardBalances);
                                    await _itemCardBalanceRepos.SaveChangesAsync();
                                }

                            }
                            else if (itemCard.CostCalculateMethod == (int)CostCalculateMethodsEnum.LastPurchasePrice)
                            {
                                if (billTypeResult.Kind == (int)BillKindEnum.Purchases)
                                {
                                    costPrice = (item.TotalCostPrice.Value * result.CurrencyValue.Value);
                                    if (costPrice != null)
                                    {
                                        itemCard.CostPrice = costPrice;

                                    }
                                    else
                                    {
                                        itemCard.CostPrice = 0;
                                        costPrice = 0;

                                    }

                                    UpdateCostPrice(itemCard.Id, itemCard.CostPrice.Value);


                                    if (item.StoreId > 0 && item.ItemId > 0)
                                    {
                                        var itemCardBalances = await _itemCardBalanceRepos.GetAll().Where(x => x.StoreId == item.StoreId && x.ItemCardId == item.ItemId).FirstOrDefaultAsync();


                                        itemCardBalances.CostPrice = costPrice;
                                        await _itemCardBalanceRepos.UpdateAsync(itemCardBalances);
                                        await _itemCardBalanceRepos.SaveChangesAsync();



                                    }
                                }

                            }



                        }

                    }

                }
            }

        }
        public async Task<BillDto> FirstInclude(long id)
        {
            var item = await _billRepos.GetAll().Include(c => c.BillItems).ThenInclude(c => c.BillItemTaxes).
                Include(c => c.BillItems).ThenInclude(c => c.BillDynamicDeterminants).
                Include(c => c.BillAdditionAndDiscounts).
                Include(c => c.BillInstallmentDetails).Include(c => c.BillPaymentDetails).
                AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
            var result = _mpper.Map<BillDto>(item);
            return result;
        }
        public async Task<int> DeleteAsync(long id)
        {

            var billResult = await FirstInclude(id);
            int? parentType = 0;
            var billTypeResult = await _billTypeRepos.GetAll().Where(x => x.Id == billResult.BillTypeId).FirstOrDefaultAsync();

            if (billTypeResult.Kind != (int)BillKindEnum.FirstPeriodGoods)
            {
                if (billTypeResult.Kind == (int)BillKindEnum.Sales)
                {
                    parentType = (int)EntryTypesEnum.SalesBill;
                }

                if (billTypeResult.Kind == (int)BillKindEnum.SalesReturn)
                {
                    parentType = (int)EntryTypesEnum.SalesReturnBill;
                }
                if (billTypeResult.Kind == (int)BillKindEnum.Purchases)
                {
                    parentType = (int)EntryTypesEnum.PurchasesBill;
                }

                if (billTypeResult.Kind == (int)BillKindEnum.PurchasesReturn)
                {
                    parentType = (int)EntryTypesEnum.PurchasesReturnBill;
                }
                var journalEntriesItem = await _journalEntriesRepository.GetAllIncluding(c => c.JournalEntriesDetail).AsNoTracking().FirstOrDefaultAsync(c => c.ParentType == parentType & c.ParentTypeId == billResult.Id && c.IsDeleted != true);

                if (journalEntriesItem != null)
                {
                    foreach (var item in journalEntriesItem.JournalEntriesDetail.ToList())
                    {

                        await _journalEntriesDetailRepository.SoftDeleteWithoutSaveAsync(item.Id);
                    }
                    await _journalEntriesRepository.SoftDeleteWithoutSaveAsync(journalEntriesItem.Id);
                }
            }

            if (billResult.BillItems != null)
            {
                foreach (var item in billResult.BillItems.ToList())
                {
                    if (item.BillItemTaxes != null)
                    {
                        foreach (var billItemTaxesItem in item.BillItemTaxes)
                        {
                            await _billItemTaxRepos.SoftDeleteWithoutSaveAsync(billItemTaxesItem.Id);
                        }
                    }

                    if (item.BillDynamicDeterminants != null)
                    {

                        foreach (var item1 in item.BillDynamicDeterminants)
                        {
                            await _billDynamicDeterminantRepos.SoftDeleteAsync(item1.Id);
                        }

                    }
                    await _billItemRepos.SoftDeleteWithoutSaveAsync(item.Id);



                }


            }

            if (billResult.BillAdditionAndDiscounts != null)
            {
                foreach (var item in billResult.BillAdditionAndDiscounts.ToList())
                {

                    await _billAdditionAndDiscountRepos.SoftDeleteWithoutSaveAsync(item.Id);
                }
            }

            if (billResult.BillPaymentDetails != null)
            {
                foreach (var item in billResult.BillPaymentDetails.ToList())
                {

                    await _billPaymentDetailsRepos.SoftDeleteWithoutSaveAsync(item.Id);
                }
            }

            await UpdateCostPriceForItems(billResult, billTypeResult);



            return await base.ScriptCheckDeleteWithDetails(id, new List<string> { "BillDynamicDeterminants" , "BillItemTaxes", "BillItems", "BillAdditionAndDiscounts","BillPaymentDetails" }, "Bills", "Id");

        }
        public async Task<int> DeleteListAsync(List<object> ids)
        {
            foreach (var id in ids)
            {

                var billResult = await FirstInclude(Convert.ToInt64(id));

                int? parentType = 0;

                var billTypeResult = await _billTypeRepos.GetAll().Where(x => x.Id == billResult.BillTypeId).FirstOrDefaultAsync();
                if (billTypeResult.Kind != (int)BillKindEnum.FirstPeriodGoods)
                {
                    if (billTypeResult.Kind == (int)BillKindEnum.Sales)
                    {
                        parentType = (int)EntryTypesEnum.SalesBill;
                    }

                    if (billTypeResult.Kind == (int)BillKindEnum.SalesReturn)
                    {
                        parentType = (int)EntryTypesEnum.SalesReturnBill;
                    }
                    if (billTypeResult.Kind == (int)BillKindEnum.Purchases)
                    {
                        parentType = (int)EntryTypesEnum.PurchasesBill;
                    }

                    if (billTypeResult.Kind == (int)BillKindEnum.PurchasesReturn)
                    {
                        parentType = (int)EntryTypesEnum.PurchasesReturnBill;
                    }
                    var journalEntriesItem = await _journalEntriesRepository.GetAllIncluding(c => c.JournalEntriesDetail).AsNoTracking().FirstOrDefaultAsync(c => c.ParentType == parentType & c.ParentTypeId == billResult.Id && c.IsDeleted != true);

                    if (journalEntriesItem != null)
                    {
                        foreach (var item in journalEntriesItem.JournalEntriesDetail.ToList())
                        {

                            await _journalEntriesDetailRepository.SoftDeleteWithoutSaveAsync(item.Id);
                        }
                        await _journalEntriesRepository.SoftDeleteWithoutSaveAsync(journalEntriesItem.Id);
                    }
                }
                if (billResult.BillItems != null)
                {
                    foreach (var item in billResult.BillItems.ToList())
                    {
                        if (item.BillItemTaxes != null)
                        {
                            foreach (var billItemTaxesItem in item.BillItemTaxes)
                            {
                                await _billItemTaxRepos.SoftDeleteWithoutSaveAsync(billItemTaxesItem.Id);
                            }
                        }
                        if (item.BillDynamicDeterminants != null)
                        {

                            foreach (var determinantItem in item.BillDynamicDeterminants)
                            {
                                await _billDynamicDeterminantRepos.SoftDeleteAsync(determinantItem.Id);
                            }

                        }

                        await _billItemRepos.SoftDeleteWithoutSaveAsync(item.Id);



                    }


                }

                if (billResult.BillAdditionAndDiscounts != null)
                {
                    foreach (var item in billResult.BillAdditionAndDiscounts.ToList())
                    {

                        await _billAdditionAndDiscountRepos.SoftDeleteWithoutSaveAsync(item.Id);
                    }

                }
                if (billResult.BillPaymentDetails != null)
                {
                    foreach (var item in billResult.BillPaymentDetails.ToList())
                    {

                        await _billPaymentDetailsRepos.SoftDeleteWithoutSaveAsync(item.Id);
                    }
                }

                await UpdateCostPriceForItems(billResult, billTypeResult);


            }
            return await base.ScriptCheckDeleteWithDetails(ids, new List<string> { "BillDynamicDeterminants", "BillItemTaxes", "BillItems", "BillAdditionAndDiscounts" , "BillPaymentDetails"  }, "Bills", "Id");
        }
        public override string LastCode()
        {
            return base.LastCode();
        }
        #region Get Last Code  
        public async Task<string> getLastBillCodeByTypeId(long typeId)
        {
            var code = "";
            var CodingPolicy = 0;

            var billTypeResult = await _billTypeRepos.GetAll().Where(x => x.Id == typeId).FirstOrDefaultAsync();

            List<Bill> billsList = new List<Bill>();

            if (billTypeResult != null)
            {
                CodingPolicy = billTypeResult.CodingPolicy;
            }

            if (CodingPolicy == (int)CodingPolicyEnum.Automatic)
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
        public async Task<ResponseResult> generateEntry(List<long> ids)
        {
            var result = new ResponseResult();

            try
            {

                var userId = _auditService.UserId;
                List<BillType> billType = new List<BillType>();
                long JournalEntryId = 0;

                if (ids != null)
                {
                    var code = "";
                    int inventorySystem = 0;
                    var fiscalPeriodResult = await _generalConfiguration.GetAll().Where(x => x.Code == "7").FirstOrDefaultAsync();
                    var inventorySystemResult = await _generalConfiguration.GetAll().Where(x => x.Code == "9").FirstOrDefaultAsync();

                    if (inventorySystemResult != null)
                    {
                        if (!string.IsNullOrEmpty(inventorySystemResult.Value))
                        {
                            inventorySystem = int.Parse(inventorySystemResult.Value.ToString());
                        }
                    }



                    foreach (var id in ids)
                    {
                        var bill = await FirstInclude(id);

                        billType = _billTypeRepos.GetAll().Where(x => x.Id == bill.BillTypeId).ToList();

                        if (billType != null)
                        {
                            List<JournalEntriesMaster> journalEntriesList = _journalEntriesRepository.GetAll().Where(x => x.IsDeleted != true).ToList();
                            if (journalEntriesList.Count > 0)
                            {
                                code = journalEntriesList.Select(x => x.Code).LastOrDefault().ToString();
                                code = (long.Parse(code) + 1).ToString();

                            }
                            else
                            {
                                code = "1";

                            }


                            JournalEntriesMaster journalEntriesMaster = new JournalEntriesMaster();
                            journalEntriesMaster.Code = code.ToString();
                            journalEntriesMaster.CompanyId = bill.CompanyId;
                            journalEntriesMaster.BranchId = bill.BranchId;
                            journalEntriesMaster.Date = bill.Date;
                            if (billType[0].Kind == (int)BillKindEnum.Sales)
                            {
                                journalEntriesMaster.ParentType = (int)EntryTypesEnum.SalesBill;

                            }
                            else if (billType[0].Kind == (int)BillKindEnum.Purchases)
                            {
                                journalEntriesMaster.ParentType = (int)EntryTypesEnum.PurchasesBill;

                            }
                            else if (billType[0].Kind == (int)BillKindEnum.SalesReturn)
                            {
                                journalEntriesMaster.ParentType = (int)EntryTypesEnum.SalesReturnBill;

                            }
                            else if (billType[0].Kind == (int)BillKindEnum.PurchasesReturn)
                            {
                                journalEntriesMaster.ParentType = (int)EntryTypesEnum.PurchasesReturnBill;

                            }
                            journalEntriesMaster.ParentTypeId = bill.Id;
                            if (fiscalPeriodResult != null)
                            {
                                if (!string.IsNullOrEmpty(fiscalPeriodResult.Value))
                                {
                                    journalEntriesMaster.FiscalPeriodId = long.Parse(fiscalPeriodResult.Value);
                                }
                                else
                                {
                                    //if(lang=="en-us")
                                    //{
                                    throw new UserFriendlyException("Financial Year is required");

                                    //}
                                    //else
                                    //{

                                    //    throw new UserFriendlyException("السنة المالية مطلوبة");

                                    //}

                                }
                            }
                            else
                            {

                                //if (lang == "en-us")
                                //{
                                throw new UserFriendlyException("Financial Year is required");

                                //}
                                //else
                                //{

                                //    throw new UserFriendlyException("السنة المالية مطلوبة");

                                //}
                            }

                            if (billType[0].PostingToAccountsAutomatically == true)
                            {
                                journalEntriesMaster.PostType = (int)PostTypeEnum.Post;
                            }
                            else
                            {
                                journalEntriesMaster.PostType = (int)PostTypeEnum.NotPost;

                            }


                            if (billType[0].JournalId > 0)
                            {
                                journalEntriesMaster.JournalId = billType[0].JournalId;

                            }
                            else
                            {
                                //if (lang == "en-us")
                                //{

                                throw new UserFriendlyException("Journal  is required");
                                //}
                                //else
                                //{
                                //    throw new UserFriendlyException("اليومية مطلوبة");

                                //}
                            }



                            await _journalEntriesRepository.InsertAsync(journalEntriesMaster);
                            _journalEntriesRepository.SaveChanges();
                            JournalEntryId = journalEntriesMaster.Id;
                            byte i = 0;
                            int? payWay = bill.PayWay;
                            //  string cashAccountId;
                            string customerAccountId;
                            string supplierAccountId;
                            string salesAccountId;
                            string salesReturnAccountId;
                            string purchasesAccountId;
                            string purchasesReturnAccountId;
                            var inventoryAccountId = "";
                            var salesCostAccountId = "";
                            var taxAccountId = "";

                            decimal cost = 0;
                            decimal costLocal = 0;
                            decimal totalTax = 0;
                            decimal totalTaxLocal = 0;



                            if (billType[0].Kind == (int)BillKindEnum.Sales)
                            {

                                foreach (var item in bill.BillItems)
                                {

                                    var itemCard = await _itemCardRepos.GetAll().Where(X => X.Id == item.ItemId).FirstOrDefaultAsync();
                                    if (itemCard.SalesAccountId != null)
                                    {
                                        salesAccountId = itemCard.SalesAccountId;
                                    }
                                    else
                                    {
                                        salesAccountId = bill.SalesAccountId;
                                    }
                                    if (inventorySystem == (int)InventorySystemEnum.ContinuousInventory)
                                    {
                                        inventoryAccountId = itemCard.InventoryAccountId;
                                        salesCostAccountId = itemCard.SalesCostAccountId;

                                        if (string.IsNullOrEmpty(inventoryAccountId))
                                        {
                                            if (billType[0].BillTypeDefaultValueUsers != null)
                                            {
                                                var userData = billType[0].BillTypeDefaultValueUsers.Where(x => x.UserId == userId).ToList();
                                                if (userData != null && userData.Count > 0)
                                                {
                                                    if (string.IsNullOrEmpty(userData[0].InventoryAccountId))
                                                    {
                                                        inventoryAccountId = billType[0].InventoryAccountId;

                                                    }
                                                    else
                                                    {
                                                        inventoryAccountId = userData[0].InventoryAccountId;

                                                    }

                                                }
                                                else
                                                {
                                                    inventoryAccountId = billType[0].InventoryAccountId;

                                                }
                                            }
                                            else
                                            {
                                                inventoryAccountId = billType[0].InventoryAccountId;


                                            }

                                        }
                                        if (string.IsNullOrEmpty(salesCostAccountId))
                                        {
                                            if (billType[0].BillTypeDefaultValueUsers != null)
                                            {
                                                var userData = billType[0].BillTypeDefaultValueUsers.Where(x => x.UserId == userId).ToList();
                                                if (userData != null && userData.Count > 0)
                                                {
                                                    if (string.IsNullOrEmpty(userData[0].SalesCostAccountId))
                                                    {
                                                        salesCostAccountId = billType[0].SalesCostAccountId;

                                                    }
                                                    else
                                                    {
                                                        salesCostAccountId = userData[0].SalesCostAccountId;

                                                    }

                                                }
                                                else
                                                {
                                                    salesCostAccountId = billType[0].SalesCostAccountId;

                                                }
                                            }
                                            else
                                            {
                                                salesCostAccountId = billType[0].SalesCostAccountId;


                                            }

                                        }
                                    }



                                    await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, salesAccountId.ToString(), decimal.Parse(item.TotalBeforeTax.Value.ToString()), 0, decimal.Parse(item.TotalBeforeTax.Value.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()), 0, decimal.Parse(bill.CurrencyValue.Value.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);

                                    if (billType[0].CalculatingTax == true)
                                    {
                                        //if (billType[0].CalculatingTaxManual != true)
                                        //{
                                        if (item.BillItemTaxes != null)
                                        {
                                            foreach (var itemTax in item.BillItemTaxes)
                                            {
                                                var Tax = await _taxMasterRepository.GetAll().Where(X => X.Id == itemTax.TaxId).FirstOrDefaultAsync();
                                                taxAccountId = Tax.AccountId;
                                                await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, taxAccountId.ToString(), decimal.Parse(itemTax.TaxValue.ToString()), 0, decimal.Parse(itemTax.TaxValue.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()), 0, decimal.Parse(bill.CurrencyValue.Value.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);
                                                totalTax += decimal.Parse(itemTax.TaxValue.ToString());

                                                totalTaxLocal += decimal.Parse(itemTax.TaxValue.ToString()) * decimal.Parse(bill.CurrencyValue.ToString());

                                            }

                                        }
                                        // }
                                        //else
                                        //{
                                        //    await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, bill.TaxAccountId.ToString(), decimal.Parse(bill.TaxValue.ToString()), 0, decimal.Parse(bill.TaxValue.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()), 0, decimal.Parse(bill.CurrencyValue.Value.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);
                                        //    totalTax = decimal.Parse(bill.TaxValue.ToString());
                                        //    totalTaxLocal = decimal.Parse(bill.TaxValue.ToString()) * decimal.Parse(bill.CurrencyValue.ToString());


                                        //}
                                    }
                                    if (inventoryAccountId != null && itemCard.CostPrice > 0 && inventorySystem == (int)InventorySystemEnum.ContinuousInventory)
                                    {
                                        await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, inventoryAccountId.ToString(), decimal.Parse(item.IssuedQuantity.ToString()) * decimal.Parse(itemCard.CostPrice.ToString()), 0, decimal.Parse(item.IssuedQuantity.ToString()) * decimal.Parse(itemCard.CostPrice.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()), 0, decimal.Parse(bill.CurrencyValue.Value.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);
                                        cost += decimal.Parse(item.IssuedQuantity.ToString()) * decimal.Parse(itemCard.CostPrice.ToString());
                                        costLocal += decimal.Parse(item.IssuedQuantity.ToString()) * decimal.Parse(itemCard.CostPrice.ToString()) * decimal.Parse(bill.CurrencyValue.ToString());

                                    }

                                }
                                if (bill.BillAdditionAndDiscounts != null)
                                {
                                    foreach (var item in bill.BillAdditionAndDiscounts)
                                    {
                                        await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, bill.SalesAccountId.ToString(), decimal.Parse(item.AdditionValue.Value.ToString()) - decimal.Parse(item.DiscountValue.Value.ToString()), 0, decimal.Parse(item.AdditionValue.Value.ToString()) - decimal.Parse(item.DiscountValue.Value.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()), 0, decimal.Parse(bill.CurrencyValue.Value.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);

                                    }
                                }

                                if (payWay == (int)PayWaysEnum.Cash && billType[0].IsGenerateVoucherIfPayWayIsCash != true)
                                {
                                    //  cashAccountId = bill.CashAccountId;
                                    //  await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, cashAccountId.ToString(), 0, decimal.Parse(bill.TotalBeforeTax.ToString()) + totalTax, 0, decimal.Parse(bill.TotalBeforeTax.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()) + totalTaxLocal, decimal.Parse(bill.CurrencyValue.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);

                                    var paymentMethod = await _paymentMethodRepos.FirstOrDefaultAsync(billType[0].DefaultPaymentMethodId);
                                    if (paymentMethod != null)
                                    {
                                        await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, paymentMethod.AccountId.ToString(), 0, decimal.Parse(bill.TotalBeforeTax.ToString()) + totalTax, 0, decimal.Parse(bill.TotalBeforeTax.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()) + totalTaxLocal, decimal.Parse(bill.CurrencyValue.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);

                                    }



                                }
                                else
                                {
                                    var customer = await _customerCardRepository.GetAll().Where(X => X.Id == bill.CustomerId).FirstOrDefaultAsync();

                                    customerAccountId = customer.AccountId;
                                    await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, customerAccountId.ToString(), 0, decimal.Parse(bill.TotalBeforeTax.ToString()) + totalTax, 0, decimal.Parse(bill.TotalBeforeTax.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()) + totalTaxLocal, decimal.Parse(bill.CurrencyValue.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);

                                }
                                if (bill.BillAdditionAndDiscounts != null)
                                {
                                    foreach (var item in bill.BillAdditionAndDiscounts)
                                    {
                                        await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, item.AccountId.ToString(), 0, decimal.Parse(item.AdditionValue.Value.ToString()) - decimal.Parse(item.DiscountValue.Value.ToString()), 0, decimal.Parse(item.AdditionValue.Value.ToString()) - decimal.Parse(item.DiscountValue.Value.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()), decimal.Parse(bill.CurrencyValue.Value.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);

                                    }
                                }
                                if (cost > 0 && inventorySystem == (int)InventorySystemEnum.ContinuousInventory)
                                {
                                    await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, salesCostAccountId, 0, cost, 0, costLocal, decimal.Parse(bill.CurrencyValue.Value.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);

                                }

                            }
                            else if (billType[0].Kind == (int)BillKindEnum.SalesReturn)
                            {
                                foreach (var item in bill.BillItems)
                                {

                                    var itemCard = await _itemCardRepos.GetAll().Where(X => X.Id == item.ItemId).FirstOrDefaultAsync();
                                    if (itemCard.SalesReturnsAccountId != null)
                                    {
                                        salesReturnAccountId = itemCard.SalesReturnsAccountId;
                                    }
                                    else
                                    {
                                        salesReturnAccountId = bill.SalesReturnAccountId;
                                    }
                                    if (inventorySystem == (int)InventorySystemEnum.ContinuousInventory)
                                    {
                                        inventoryAccountId = itemCard.InventoryAccountId;
                                        salesCostAccountId = itemCard.SalesCostAccountId;

                                        if (string.IsNullOrEmpty(inventoryAccountId))
                                        {
                                            if (billType[0].BillTypeDefaultValueUsers != null)
                                            {
                                                var userData = billType[0].BillTypeDefaultValueUsers.Where(x => x.UserId == userId).ToList();
                                                if (userData != null && userData.Count > 0)
                                                {
                                                    if (string.IsNullOrEmpty(userData[0].InventoryAccountId))
                                                    {
                                                        inventoryAccountId = billType[0].InventoryAccountId;

                                                    }
                                                    else
                                                    {
                                                        inventoryAccountId = userData[0].InventoryAccountId;

                                                    }

                                                }
                                                else
                                                {
                                                    inventoryAccountId = billType[0].InventoryAccountId;

                                                }
                                            }
                                            else
                                            {
                                                inventoryAccountId = billType[0].InventoryAccountId;


                                            }

                                        }
                                        if (string.IsNullOrEmpty(salesCostAccountId))
                                        {
                                            if (billType[0].BillTypeDefaultValueUsers != null)
                                            {
                                                var userData = billType[0].BillTypeDefaultValueUsers.Where(x => x.UserId == userId).ToList();
                                                if (userData != null && userData.Count > 0)
                                                {
                                                    if (string.IsNullOrEmpty(userData[0].SalesCostAccountId))
                                                    {
                                                        salesCostAccountId = billType[0].SalesCostAccountId;

                                                    }
                                                    else
                                                    {
                                                        salesCostAccountId = userData[0].SalesCostAccountId;

                                                    }

                                                }
                                                else
                                                {
                                                    salesCostAccountId = billType[0].SalesCostAccountId;

                                                }
                                            }
                                            else
                                            {
                                                salesCostAccountId = billType[0].SalesCostAccountId;


                                            }

                                        }
                                    }


                                    await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, salesReturnAccountId.ToString(), 0,
                                        decimal.Parse(item.TotalBeforeTax.Value.ToString()),
                                        0, decimal.Parse(item.TotalBeforeTax.Value.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()), decimal.Parse(bill.CurrencyValue.Value.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);

                                    if (billType[0].CalculatingTax == true)
                                    {
                                        //if (billType[0].CalculatingTaxManual != true)
                                        //{
                                        if (item.BillItemTaxes != null)
                                        {
                                            foreach (var itemTax in item.BillItemTaxes)
                                            {
                                                var tax = await _taxMasterRepository.GetAll().Where(X => X.Id == itemTax.TaxId).FirstOrDefaultAsync();
                                                taxAccountId = tax.AccountId;
                                                await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, taxAccountId.ToString(),
                                                   0, decimal.Parse(itemTax.TaxValue.ToString()),
                                                   0, decimal.Parse(itemTax.TaxValue.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()), decimal.Parse(bill.CurrencyValue.Value.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);
                                                totalTax += decimal.Parse(itemTax.TaxValue.ToString());

                                                totalTaxLocal += decimal.Parse(itemTax.TaxValue.ToString()) * decimal.Parse(bill.CurrencyValue.ToString());

                                            }

                                        }
                                        // }
                                        //else
                                        //{
                                        //    await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, bill.TaxAccountId.ToString(),
                                        //      0, decimal.Parse(bill.TaxValue.ToString()),
                                        //     0, decimal.Parse(bill.TaxValue.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()), decimal.Parse(bill.CurrencyValue.Value.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);
                                        //    totalTax = decimal.Parse(bill.TaxValue.ToString());
                                        //    totalTaxLocal = decimal.Parse(bill.TaxValue.ToString()) * decimal.Parse(bill.CurrencyValue.ToString());


                                        //}
                                    }
                                    if (inventoryAccountId != null && decimal.Parse(itemCard.CostPrice.ToString()) > 0 && inventorySystem == (int)InventorySystemEnum.ContinuousInventory)
                                    {
                                        await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, inventoryAccountId.ToString(),
                                      0, decimal.Parse(item.AddedQuantity.ToString()) * decimal.Parse(itemCard.CostPrice.ToString()),
                                      0, decimal.Parse(item.AddedQuantity.ToString()) * decimal.Parse(itemCard.CostPrice.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()), decimal.Parse(bill.CurrencyValue.Value.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);
                                        cost += decimal.Parse(item.AddedQuantity.ToString()) * decimal.Parse(itemCard.CostPrice.ToString());
                                        costLocal += decimal.Parse(item.AddedQuantity.ToString()) * decimal.Parse(itemCard.CostPrice.ToString()) * decimal.Parse(bill.CurrencyValue.ToString());
                                    }


                                }
                                if (bill.BillAdditionAndDiscounts != null)
                                {
                                    foreach (var item in bill.BillAdditionAndDiscounts)
                                    {
                                        await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, bill.SalesReturnAccountId.ToString(),
                                          0, decimal.Parse(item.AdditionValue.Value.ToString()) - decimal.Parse(item.DiscountValue.Value.ToString()), 0, decimal.Parse(item.AdditionValue.Value.ToString()) - decimal.Parse(item.DiscountValue.Value.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()), decimal.Parse(bill.CurrencyValue.Value.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);

                                    }
                                }

                                if (payWay == (int)PayWaysEnum.Cash && billType[0].IsGenerateVoucherIfPayWayIsCash != true)
                                {
                                    //cashAccountId = bill.CashAccountId;
                                    //  await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, cashAccountId.ToString(), decimal.Parse(bill.TotalBeforeTax.ToString()) + totalTax, 0, decimal.Parse(bill.TotalBeforeTax.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()) + totalTaxLocal, 0, decimal.Parse(bill.CurrencyValue.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);


                                    var paymentMethod = await _paymentMethodRepos.FirstOrDefaultAsync(billType[0].DefaultPaymentMethodId);
                                    if (paymentMethod != null)
                                    {
                                        await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, paymentMethod.AccountId.ToString(), decimal.Parse(bill.TotalBeforeTax.ToString()) + totalTax, 0, decimal.Parse(bill.TotalBeforeTax.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()) + totalTaxLocal, 0, decimal.Parse(bill.CurrencyValue.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);

                                    }






                                }

                                else
                                {
                                    var Customer = await _customerCardRepository.GetAll().Where(X => X.Id == bill.CustomerId).FirstOrDefaultAsync();

                                    customerAccountId = Customer.AccountId;
                                    await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, customerAccountId.ToString(), decimal.Parse(bill.TotalBeforeTax.ToString()) + totalTax, 0, decimal.Parse(bill.TotalBeforeTax.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()) + totalTaxLocal, 0, decimal.Parse(bill.CurrencyValue.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);

                                }
                                if (bill.BillAdditionAndDiscounts != null)
                                {
                                    foreach (var item in bill.BillAdditionAndDiscounts)
                                    {
                                        await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, item.AccountId.ToString(), decimal.Parse(item.AdditionValue.Value.ToString()) - decimal.Parse(item.DiscountValue.Value.ToString()), 0, decimal.Parse(item.AdditionValue.Value.ToString()) - decimal.Parse(item.DiscountValue.Value.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()), 0, decimal.Parse(bill.CurrencyValue.Value.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);

                                    }
                                }
                                if (cost > 0 && inventorySystem == (int)InventorySystemEnum.ContinuousInventory)
                                {
                                    await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, salesCostAccountId, cost, 0, costLocal, 0, decimal.Parse(bill.CurrencyValue.Value.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);

                                }
                            }
                            if (billType[0].Kind == (int)BillKindEnum.Purchases)
                            {
                                foreach (var item in bill.BillItems)
                                {

                                    var itemCard = await _itemCardRepos.GetAll().Where(X => X.Id == item.ItemId).FirstOrDefaultAsync();
                                    if (itemCard.PurchasesAccountId != null)
                                    {
                                        purchasesAccountId = itemCard.PurchasesAccountId;
                                    }
                                    else
                                    {
                                        purchasesAccountId = bill.PurchasesAccountId;
                                    }



                                    await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, purchasesAccountId.ToString(), 0,
                                        decimal.Parse(item.TotalBeforeTax.Value.ToString()),
                                        0, decimal.Parse(item.TotalBeforeTax.Value.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()), decimal.Parse(bill.CurrencyValue.Value.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);

                                    if (billType[0].CalculatingTax == true)
                                    {
                                        //if (billType[0].CalculatingTaxManual != true)
                                        //{
                                        if (item.BillItemTaxes != null)
                                        {
                                            foreach (var itemTax in item.BillItemTaxes)
                                            {
                                                var tax = await _taxMasterRepository.GetAll().Where(X => X.Id == itemTax.TaxId).FirstOrDefaultAsync();
                                                taxAccountId = tax.AccountId;
                                                await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, taxAccountId.ToString(),
                                                   0, decimal.Parse(itemTax.TaxValue.ToString()),
                                                   0, decimal.Parse(itemTax.TaxValue.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()), decimal.Parse(bill.CurrencyValue.Value.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);
                                                totalTax += decimal.Parse(itemTax.TaxValue.ToString());

                                                totalTaxLocal += decimal.Parse(itemTax.TaxValue.ToString()) * decimal.Parse(bill.CurrencyValue.ToString());

                                            }

                                        }
                                        //}
                                        //else
                                        //{
                                        //    await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, bill.TaxAccountId.ToString(),
                                        //      0, decimal.Parse(bill.TaxValue.ToString()),
                                        //     0, decimal.Parse(bill.TaxValue.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()), decimal.Parse(bill.CurrencyValue.Value.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);
                                        //    totalTax = decimal.Parse(bill.TaxValue.ToString());
                                        //    totalTaxLocal = decimal.Parse(bill.TaxValue.ToString()) * decimal.Parse(bill.CurrencyValue.ToString());


                                        //}
                                    }




                                }
                                if (bill.BillAdditionAndDiscounts != null)
                                {
                                    foreach (var item in bill.BillAdditionAndDiscounts)
                                    {
                                        await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, bill.PurchasesAccountId.ToString(),
                                          0, decimal.Parse(item.AdditionValue.Value.ToString()) - decimal.Parse(item.DiscountValue.Value.ToString()), 0, decimal.Parse(item.AdditionValue.Value.ToString()) - decimal.Parse(item.DiscountValue.Value.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()), decimal.Parse(bill.CurrencyValue.Value.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);

                                    }
                                }

                                if (payWay == (int)PayWaysEnum.Cash && billType[0].IsGenerateVoucherIfPayWayIsCash != true)
                                {
                                    //  cashAccountId = bill.CashAccountId;
                                    //  await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, cashAccountId.ToString(), decimal.Parse(bill.TotalBeforeTax.ToString()) + totalTax, 0, decimal.Parse(bill.TotalBeforeTax.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()) + totalTaxLocal, 0, decimal.Parse(bill.CurrencyValue.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);

                                    var paymentMethod = await _paymentMethodRepos.FirstOrDefaultAsync(billType[0].DefaultPaymentMethodId);
                                    if (paymentMethod != null)
                                    {
                                        await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, paymentMethod.AccountId.ToString(), 0, decimal.Parse(bill.TotalBeforeTax.ToString()) + totalTax, 0, decimal.Parse(bill.TotalBeforeTax.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()) + totalTaxLocal, decimal.Parse(bill.CurrencyValue.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);

                                    }


                                }

                                else
                                {
                                    var supplier = await _supplierCardRepository.GetAll().Where(X => X.Id == bill.SupplierId).FirstOrDefaultAsync();

                                    supplierAccountId = supplier.AccountId;
                                    await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, supplierAccountId.ToString(), decimal.Parse(bill.TotalBeforeTax.ToString()) + totalTax, 0, decimal.Parse(bill.TotalBeforeTax.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()) + totalTaxLocal, 0, decimal.Parse(bill.CurrencyValue.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);

                                }
                                if (bill.BillAdditionAndDiscounts != null)
                                {
                                    foreach (var item in bill.BillAdditionAndDiscounts)
                                    {
                                        await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, item.AccountId.ToString(), decimal.Parse(item.AdditionValue.Value.ToString()) - decimal.Parse(item.DiscountValue.Value.ToString()), 0, decimal.Parse(item.AdditionValue.Value.ToString()) - decimal.Parse(item.DiscountValue.Value.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()), 0, decimal.Parse(bill.CurrencyValue.Value.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);

                                    }
                                }
                            }
                            else if (billType[0].Kind == (int)BillKindEnum.PurchasesReturn)
                            {
                                foreach (var item in bill.BillItems)
                                {

                                    var itemCard = await _itemCardRepos.GetAll().Where(X => X.Id == item.ItemId).FirstOrDefaultAsync();
                                    if (itemCard.PurchasesReturnsAccountId != null)
                                    {
                                        purchasesReturnAccountId = itemCard.PurchasesReturnsAccountId;
                                    }
                                    else
                                    {
                                        purchasesReturnAccountId = bill.PurchasesReturnAccountId;
                                    }



                                    await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, purchasesReturnAccountId.ToString()
                                        , decimal.Parse(item.TotalBeforeTax.Value.ToString()), 0, decimal.Parse(item.TotalBeforeTax.Value.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()), 0, decimal.Parse(bill.CurrencyValue.Value.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);

                                    if (billType[0].CalculatingTax == true)
                                    {
                                        //if (billType[0].CalculatingTaxManual != true)
                                        //{
                                        if (item.BillItemTaxes != null)
                                        {
                                            foreach (var itemTax in item.BillItemTaxes)
                                            {
                                                var tax = await _taxMasterRepository.GetAll().Where(X => X.Id == itemTax.TaxId).FirstOrDefaultAsync();
                                                taxAccountId = tax.AccountId;
                                                await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++,
                                                    taxAccountId.ToString(), decimal.Parse(itemTax.TaxValue.ToString()), 0, decimal.Parse(itemTax.TaxValue.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()), 0, decimal.Parse(bill.CurrencyValue.Value.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);
                                                totalTax += decimal.Parse(itemTax.TaxValue.ToString());

                                                totalTaxLocal += decimal.Parse(itemTax.TaxValue.ToString()) * decimal.Parse(bill.CurrencyValue.ToString());

                                            }

                                        }
                                        //}
                                        //else
                                        //{
                                        //    await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, bill.TaxAccountId.ToString(), decimal.Parse(bill.TaxValue.ToString()), 0, decimal.Parse(bill.TaxValue.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()), 0, decimal.Parse(bill.CurrencyValue.Value.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);
                                        //    totalTax = decimal.Parse(bill.TaxValue.ToString());
                                        //    totalTaxLocal = decimal.Parse(bill.TaxValue.ToString()) * decimal.Parse(bill.CurrencyValue.ToString());


                                        //}
                                    }




                                }
                                if (bill.BillAdditionAndDiscounts != null)
                                {
                                    foreach (var item in bill.BillAdditionAndDiscounts)
                                    {
                                        await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, bill.PurchasesReturnAccountId.ToString(), decimal.Parse(item.AdditionValue.Value.ToString()) - decimal.Parse(item.DiscountValue.Value.ToString()), 0, decimal.Parse(item.AdditionValue.Value.ToString()) - decimal.Parse(item.DiscountValue.Value.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()), 0, decimal.Parse(bill.CurrencyValue.Value.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);

                                    }
                                }

                                if (payWay == (int)PayWaysEnum.Cash && billType[0].IsGenerateVoucherIfPayWayIsCash != true)
                                {
                                    //cashAccountId = bill.CashAccountId;
                                    //await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, cashAccountId.ToString(), 0, decimal.Parse(bill.TotalBeforeTax.ToString()) + totalTax, 0, decimal.Parse(bill.TotalBeforeTax.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()) + totalTaxLocal, decimal.Parse(bill.CurrencyValue.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);

                                    var paymentMethod = await _paymentMethodRepos.FirstOrDefaultAsync(billType[0].DefaultPaymentMethodId);
                                    if (paymentMethod != null)
                                    {
                                        await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, paymentMethod.AccountId.ToString(), 0, decimal.Parse(bill.TotalBeforeTax.ToString()) + totalTax, 0, decimal.Parse(bill.TotalBeforeTax.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()) + totalTaxLocal, decimal.Parse(bill.CurrencyValue.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);

                                    }



                                }

                                else
                                {
                                    var supplier = await _supplierCardRepository.GetAll().Where(X => X.Id == bill.SupplierId).FirstOrDefaultAsync();

                                    supplierAccountId = supplier.AccountId;
                                    await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, supplierAccountId.ToString(), 0, decimal.Parse(bill.TotalBeforeTax.ToString()) + totalTax, 0, decimal.Parse(bill.TotalBeforeTax.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()) + totalTaxLocal, decimal.Parse(bill.CurrencyValue.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);

                                }
                                if (bill.BillAdditionAndDiscounts != null)
                                {
                                    foreach (var item in bill.BillAdditionAndDiscounts)
                                    {
                                        await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, item.AccountId.ToString(), 0, decimal.Parse(item.AdditionValue.Value.ToString()) - decimal.Parse(item.DiscountValue.Value.ToString()), 0, decimal.Parse(item.AdditionValue.Value.ToString()) - decimal.Parse(item.DiscountValue.Value.ToString()) * decimal.Parse(bill.CurrencyValue.ToString()), decimal.Parse(bill.CurrencyValue.Value.ToString()), bill.CurrencyId, bill.CostCenterId, bill.ProjectId);

                                    }
                                }
                            }
                        }

                        try
                        {
                            bill.IsGenerateEntry = true;
                            bill.JournalEntryId = JournalEntryId;

                            if (bill.PayWay == (int)PayWaysEnum.Cash && billType[0].IsGenerateVoucherIfPayWayIsCash == true)
                            {
                                bill.Paid = bill.Net;
                                bill.Remaining = 0;

                            }
                            await base.UpdateWithoutCheckCode(bill);
                            result.Success = true;

                        }
                        catch (Exception ex)
                        {

                        }

                    }
                }
            }
            catch (Exception)
            {
                result.Success = false;

                throw;
            }
            return result;
           
        }
        public async Task postToWarehouses(List<long> ids)
        {

            if (ids != null)
            {
                foreach (var id in ids)
                {
                    var bill = await FirstInclude(id);
                    bill.PostToWarehouses = true;
                    await base.UpdateWithoutCheckCode(bill);

                }
            }
        }
        public async Task<List<CalculateCostPriceDto>> CalculateCostPrice(long itemCardId, long? storeId, int costCalculateMethod)
        {
            long companyId = Convert.ToInt64(_auditService.CompanyId);
            StringBuilder query = new StringBuilder();
            query.Append(" SELECT [dbo].[fn_Calculate_Cost_Price] ");
            query.AppendFormat(" ( {0}  ", itemCardId);
            if (storeId > 0)
            {
                query.AppendFormat("  , {0}  ", storeId);

            }
            else
            {
                query.Append(" , null ");
            }
            query.AppendFormat("  , {0}  ", companyId);
            query.AppendFormat("  , {0} ) as CostPrice ", costCalculateMethod);


            var result = await _query.QueryAsync<CalculateCostPriceDto>(query.ToString());
            return (List<CalculateCostPriceDto>)result;
        }
        public async Task<BillDynamicDeterminantList> GetBillDynamicDeterminantList(BillDynamicDeterminantInput input)
        {
            var result = new BillDynamicDeterminantList();

            if (input.BillItemId != null && input.BillItemId > 0)
            {
                var billDynamicDeterminant = await _billDynamicDeterminantRepos.GetAllAsNoTracking()


                    .WhereIf(input.BillItemId != null && input.BillItemId > 0, c => c.BillItemId == input.BillItemId).ToListAsync();

                result.DynamicDeterminantListDto = new List<InsertBillDynamicDeterminantDto>();

                result.DynamicDeterminantListDto = _mpper.Map<List<InsertBillDynamicDeterminantDto>>(billDynamicDeterminant);

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
        public virtual async Task<ResponseResult<List<NotGenerateEntryBillDto>>> ExecuteGetNotGeneratedEntryBills()
        {
            var sp = "SP_Get_Not_Generated_Entry_Bills";

            long companyId = Convert.ToInt64(_auditService.CompanyId);
            long branchId = Convert.ToInt64(_auditService.BranchId);
            var fiscalPeriodId = "7";
            var fiscalPeriodIdResult = await _generalConfiguration.GetAll().Where(x => x.Code == fiscalPeriodId).FirstOrDefaultAsync();

            long fiscalPeriodIdValue = 0;
            if (!string.IsNullOrEmpty(fiscalPeriodIdResult.Value))
            {
                fiscalPeriodIdValue = long.Parse(fiscalPeriodIdResult.Value);
            }

            var result = _billRepos.Excute<NotGenerateEntryBillDto>(sp, new List<SqlParameter>() {
                new SqlParameter(){
                    ParameterName = "@branchId",
                    Value = branchId,


                },
                new SqlParameter(){
                    ParameterName = "@companyId",
                    Value = companyId,

                },
                new SqlParameter(){
                    ParameterName = "@fiscalPeriodId",
                    Value = fiscalPeriodIdValue,

                }
            }, true);

            return result;
        }

        public virtual async Task<ResponseResult<List<NotPostToWarehousesAutomaticallyBillDto>>> ExecuteGetNotPostToWarehousesAutomaticallyBills()
        {
            var sp = "SP_Get_Not_Post_To_Warehouses_Automatically_Bills";

            long companyId = Convert.ToInt64(_auditService.CompanyId);
            long branchId = Convert.ToInt64(_auditService.BranchId);
            var fiscalPeriodId = "7";
            var fiscalPeriodIdResult = await _generalConfiguration.GetAll().Where(x => x.Code == fiscalPeriodId).FirstOrDefaultAsync();

            long fiscalPeriodIdValue = 0;
            if (!string.IsNullOrEmpty(fiscalPeriodIdResult.Value))
            {
                fiscalPeriodIdValue = long.Parse(fiscalPeriodIdResult.Value);
            }

            var result = _billRepos.Excute<NotPostToWarehousesAutomaticallyBillDto>(sp, new List<SqlParameter>() {
                new SqlParameter(){
                    ParameterName = "@branchId",
                    Value = branchId,


                },
                new SqlParameter(){
                    ParameterName = "@companyId",
                    Value = companyId,

                },
                new SqlParameter(){
                    ParameterName = "@fiscalPeriodId",
                    Value = fiscalPeriodIdValue,

                }
            }, true);

            return result;
        }

        public virtual async Task<ResponseResult<List<UnSyncedElectronicBillsDto>>> GetUnsyncedElectronicBills()
        {
            var sp = "SP_Get_UnSynced_Electronic_Bills";

            long companyId = Convert.ToInt64(_auditService.CompanyId);
            long branchId = Convert.ToInt64(_auditService.BranchId);
            var fiscalPeriodId = "7";
            var fiscalPeriodIdResult = await _generalConfiguration.GetAll().Where(x => x.Code == fiscalPeriodId).FirstOrDefaultAsync();

            long fiscalPeriodIdValue = 0;
            if (!string.IsNullOrEmpty(fiscalPeriodIdResult.Value))
            {
                fiscalPeriodIdValue = long.Parse(fiscalPeriodIdResult.Value);
            }

            var result = _billRepos.Excute<UnSyncedElectronicBillsDto>(sp, new List<SqlParameter>() {
                new SqlParameter(){
                    ParameterName = "@branchId",
                    Value = branchId,


                },
                new SqlParameter(){
                    ParameterName = "@companyId",
                    Value = companyId,

                },
                new SqlParameter(){
                    ParameterName = "@fiscalPeriodId",
                    Value = fiscalPeriodIdValue,

                }
            }, true);

            return result;
        }
        public virtual async Task<ResponseResult<List<BillPaymentDto>>> GetBillPayments(int? billKind, long? customerId, long? supplierId, long? voucherTypeId)
        {
            var sp = "SP_Get_Credit_Installment_Bills_Payments";

            long companyId = Convert.ToInt64(_auditService.CompanyId);
            long branchId = Convert.ToInt64(_auditService.BranchId);
            var fiscalPeriodId = "7";
            var fiscalPeriodIdResult = await _generalConfiguration.GetAll().Where(x => x.Code == fiscalPeriodId).FirstOrDefaultAsync();

            long fiscalPeriodValue = 0;
            if (!string.IsNullOrEmpty(fiscalPeriodIdResult.Value))
            {
                fiscalPeriodValue = long.Parse(fiscalPeriodIdResult.Value);
            }

            var result = _billRepos.Excute<BillPaymentDto>(sp, new List<SqlParameter>() {
                new SqlParameter(){
                    ParameterName = "@branchId",
                    Value = branchId,


                },
                new SqlParameter(){
                    ParameterName = "@companyId",
                    Value = companyId,

                },
                new SqlParameter(){
                    ParameterName = "@fiscalPeriodId",
                    Value = fiscalPeriodValue,

                },
                  new SqlParameter(){
                    ParameterName = "@billKind",
                    Value = billKind,

                },
                   new SqlParameter(){
                    ParameterName = "@customerId",
                    Value = customerId,

                }
                   ,
                   new SqlParameter(){
                    ParameterName = "@supplierId",
                    Value = supplierId,

                   }
                   ,
                   new SqlParameter(){
                    ParameterName = "@voucherTypeId",
                    Value = voucherTypeId,

                   }
            }, true);

            return result;
        }
        public virtual async Task<ResponseResult<List<BillPaymentDto>>> GetBillPaid(int? billKind, long? customerId, long? supplierId)
        {
            var sp = "SP_Get_Bills_Paid";

            long companyId = Convert.ToInt64(_auditService.CompanyId);
            long branchId = Convert.ToInt64(_auditService.BranchId);
            var fiscalPeriodId = "7";
            var fiscalPeriodIdResult = await _generalConfiguration.GetAll().Where(x => x.Code == fiscalPeriodId).FirstOrDefaultAsync();

            long fiscalPeriodValue = 0;
            if (!string.IsNullOrEmpty(fiscalPeriodIdResult.Value))
            {
                fiscalPeriodValue = long.Parse(fiscalPeriodIdResult.Value);
            }

            var result = _billRepos.Excute<BillPaymentDto>(sp, new List<SqlParameter>() {
                new SqlParameter(){
                    ParameterName = "@branchId",
                    Value = branchId,


                },
                new SqlParameter(){
                    ParameterName = "@companyId",
                    Value = companyId,

                },
                new SqlParameter(){
                    ParameterName = "@fiscalPeriodId",
                    Value = fiscalPeriodValue,

                },

                  new SqlParameter(){
                    ParameterName = "@billKind",
                    Value = billKind,

                },
                   new SqlParameter(){
                    ParameterName = "@customerId",
                    Value = customerId,

                }
                   ,
                   new SqlParameter(){
                    ParameterName = "@supplierId",
                    Value = supplierId,

                   }
            }, true);

            return result;
        }
        public async Task<bool> CheckQuantity(List<InsertBillDynamicDeterminantDto> billDynamicDeterminants)
        {
            foreach (var item in billDynamicDeterminants)
            {
                var determinantsValue = JsonConvert.SerializeObject(item.DeterminantsValue);
                var query = $@"
                SELECT
                    bd.ItemCardId,
                    SUM(bd.ConvertedAddedQuantity) - SUM(bd.ConvertedIssuedQuantity) AS ConvertedAddedQuantitySum
                   
                FROM
                    [dbo].[BillDynamicDeterminants] bd
               
                WHERE
                    bd.ItemCardId = '{item.ItemCardId}'
                    AND bd.DeterminantValue = '{determinantsValue}'
                GROUP BY
                    bd.ItemCardId
            ";
                var results = await _determinantQuery.QueryAsync<DeterminantQueryOutput>(query);
                if (results.Count() != 0)
                {
                    var res = results.FirstOrDefault();
                    if (item.ConvertedIssuedQuantity > res.ConvertedAddedQuantitySum)
                    {
                        throw new UserFriendlyException("check-quantity-determinant");
                    }


                }


            }
            return true;

        }
        public virtual async Task<ResponseResult<List<BillPaymentDto>>> GetInstallmentPaid(int? billKind, long? customerId, long? supplierId)
        {
            var sp = "SP_Get_Installments_Paid";

            long companyId = Convert.ToInt64(_auditService.CompanyId);
            long branchId = Convert.ToInt64(_auditService.BranchId);
            var fiscalPeriodId = "7";
            var fiscalPeriodIdResult = await _generalConfiguration.GetAll().Where(x => x.Code == fiscalPeriodId).FirstOrDefaultAsync();

            long fiscalPeriodValue = 0;
            if (!string.IsNullOrEmpty(fiscalPeriodIdResult.Value))
            {
                fiscalPeriodValue = long.Parse(fiscalPeriodIdResult.Value);
            }

            var result = _billRepos.Excute<BillPaymentDto>(sp, new List<SqlParameter>() {
                new SqlParameter(){
                    ParameterName = "@branchId",
                    Value = branchId,


                },
                new SqlParameter(){
                    ParameterName = "@companyId",
                    Value = companyId,

                },
                new SqlParameter(){
                    ParameterName = "@fiscalPeriodId",
                    Value = fiscalPeriodValue,

                },

                  new SqlParameter(){
                    ParameterName = "@billKind",
                    Value = billKind,

                },
                   new SqlParameter(){
                    ParameterName = "@customerId",
                    Value = customerId,

                }
                   ,
                   new SqlParameter(){
                    ParameterName = "@supplierId",
                    Value = supplierId,

                   }
            }, true);

            return result;
        }
        public async Task<WarehouseListsDetailDto> GetBillItemByItemId(WhareHouseInput input)
        {
            var itemCard = await _itemCardRepos.GetAllIncluding(c => c.ItemCardUnits).FirstOrDefaultAsync(c => c.Id == input.ItemId);


            var warehouseListsDetailDto = new WarehouseListsDetailDto()
            {
                BarCode = itemCard.Barcode,
                ItemDescription = itemCard.Description,
                ItemGroupId = itemCard.ItemGroupId,
                UnitId = itemCard.MainUnitId,
                MinSellingPrice = itemCard.MinSellingPrice,
                SellingPrice = itemCard.SellingPrice,
                ItemId = itemCard.Id
            };

            var billItems = await _billItemRepos.GetAll().Where(c => c.ItemId == input.ItemId && c.StoreId == input.StoreId).ToListAsync();

            var billItemsIds = billItems.Select(c => c.Id);

            var billsWithMatchingItems = await _billRepos.GetAllIncluding(b => b.BillItems).Where(b => b.BillItems.Any(bi => billItemsIds.Contains(bi.Id)))

                .SelectMany(c => c.BillItems)

                .ToListAsync();
            warehouseListsDetailDto.QuantityComputer = billsWithMatchingItems?.Sum(c => c.ConvertedAddedQuantity - c.ConvertedIssuedQuantity);



            var ItemCardBalances = await _itemCardBalanceRepos.GetAll().Where(x => x.StoreId == input.StoreId && x.ItemCardId == input.ItemId).FirstOrDefaultAsync();

            warehouseListsDetailDto.Price = ItemCardBalances?.CostPrice ?? 1;

            return warehouseListsDetailDto;
        }


        public async Task<List<WarehouseListsDetailDto>> GetBillItemByStoreId(long storeId, DateTime? billDate)
        {

            List<WarehouseListsDetailDto> warehouseListsDetailDtoLst = new List<WarehouseListsDetailDto>();


            var billItems = await _billItemRepos.GetAll().Where(c => c.StoreId == storeId).ToListAsync();

            if (billItems != null && billItems.Count > 0)
            {
                foreach (var item in billItems)
                {
                    var itemCard = await _itemCardRepos.GetAllIncluding(c => c.ItemCardUnits).FirstOrDefaultAsync(c => c.Id == item.ItemId);
                    if (itemCard != null)

                    {

                        var billItemsIds = billItems.Where(c => c.ItemId == itemCard.Id).Select(c => c.Id);
                        var itemCardBalances = await _itemCardBalanceRepos.GetAll().Where(x => x.StoreId == storeId && x.ItemCardId == itemCard.Id).FirstOrDefaultAsync();

                        if (billDate == null)
                        {

                            DateTime currentDate = DateTime.Now.Date;

                            var billsWithMatchingItems = await _billRepos.GetAllIncluding(b => b.BillItems).Where(b => b.BillItems.Any(bi => billItemsIds.Contains(bi.Id)))
                                            .Where(c => c.Date.Date == currentDate)
                                            .SelectMany(c => c.BillItems)

                                            .ToListAsync();

                            warehouseListsDetailDtoLst.Add(new WarehouseListsDetailDto
                            {
                                BarCode = itemCard.Barcode,
                                ItemDescription = itemCard.Description,
                                ItemGroupId = itemCard.ItemGroupId,
                                UnitId = itemCard.MainUnitId,
                                MinSellingPrice = itemCard.MinSellingPrice,
                                SellingPrice = itemCard.SellingPrice,
                                ItemId = itemCard.Id,
                                QuantityComputer = billsWithMatchingItems?.Sum(c => c.ConvertedAddedQuantity - c.ConvertedIssuedQuantity),
                                Price = itemCardBalances?.CostPrice ?? 1


                            });

                        }
                        else
                        {
                            var billsWithMatchingItems = await _billRepos.GetAllIncluding(b => b.BillItems).Where(b => b.BillItems.Any(bi => billItemsIds.Contains(bi.Id)))
                         .Where(c => c.Date <= billDate)
                    .SelectMany(c => c.BillItems)

                    .ToListAsync();




                            warehouseListsDetailDtoLst.Add(new WarehouseListsDetailDto
                            {
                                BarCode = itemCard.Barcode,
                                ItemDescription = itemCard.Description,
                                ItemGroupId = itemCard.ItemGroupId,
                                UnitId = itemCard.MainUnitId,
                                MinSellingPrice = itemCard.MinSellingPrice,
                                SellingPrice = itemCard.SellingPrice,
                                ItemId = itemCard.Id,
                                QuantityComputer = billsWithMatchingItems?.Sum(c => c.ConvertedAddedQuantity - c.ConvertedIssuedQuantity),
                                Price = itemCardBalances?.CostPrice ?? 1


                            });
                        }
                    }

                }

            }
            var groupedResults = warehouseListsDetailDtoLst
    .GroupBy(w => w.ItemId)
    .Select(g => new WarehouseListsDetailDto
    {
        ItemId = (long)(g?.FirstOrDefault().ItemId),
        Quantity = g.Sum(w => w.Quantity ?? 0),
        TotalCostPrice = g?.FirstOrDefault().TotalCostPrice,
        // Add other properties as needed
        ItemDescription = g?.FirstOrDefault().ItemDescription,
        UnitId = g?.FirstOrDefault().UnitId,
        Price = g?.FirstOrDefault().Price,
        StoreId = g?.FirstOrDefault().StoreId,
        ProjectId = g?.FirstOrDefault().ProjectId,
        SellingPrice = g?.FirstOrDefault().SellingPrice,
        MinSellingPrice = g?.FirstOrDefault().MinSellingPrice,
        BarCode = g?.FirstOrDefault().BarCode,
        QuantityComputer = g?.FirstOrDefault().QuantityComputer,
        PriceComputer = g?.FirstOrDefault().PriceComputer,
        ItemGroupId = g?.FirstOrDefault().ItemGroupId,
        Notes = null,
        Total = 0,
        WarehouseListId = g?.FirstOrDefault().WarehouseListId
    })
    .ToList();
            return groupedResults;

        }


        public void UpdateCostPrice(long id, double costPrice)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();

            StringBuilder updateItem = new StringBuilder();
            updateItem.AppendFormat(" Update ItemCards set CostPrice = {0} ", costPrice);
            updateItem.AppendFormat(" Where Id = {0} ", id);
            using (SqlCommand command = new SqlCommand(updateItem.ToString(), conn))
            {

                int rowsAffected = command.ExecuteNonQuery();

            }
            conn.Close();


        }

        public async Task<List<WarehouseListsDetailDto>> GetItemsByItemCodes(WhareHouseListInputCode input)
        {
            var warehouseListsDetailDtos = new List<WarehouseListsDetailDto>();

            foreach (var itemCode in input.itemCodes)
            {
                var itemCard = await _itemCardRepos.GetAllIncluding(c => c.ItemCardUnits)
                    .FirstOrDefaultAsync(c => c.Code == itemCode);

                if (itemCard != null)
                {
                    var warehouseListsDetailDto = new WarehouseListsDetailDto()
                    {
                        BarCode = itemCard.Barcode,
                        ItemDescription = itemCard.Description,
                        ItemGroupId = itemCard.ItemGroupId,
                        UnitId = itemCard.MainUnitId,
                        MinSellingPrice = itemCard.MinSellingPrice,
                        SellingPrice = itemCard.SellingPrice,
                        ItemId = itemCard.Id,
                        ItemCode = itemCard.Code
                    };

                    var billItems = await _billItemRepos.GetAll()
                        .Where(c => c.ItemId == itemCard.Id && c.StoreId == input.storeId)
                        .ToListAsync();

                    var billItemIds = billItems.Select(c => c.Id);

                    var billsWithMatchingItems = await _billRepos.GetAllIncluding(b => b.BillItems)
                        .Where(b => b.BillItems.Any(bi => billItemIds.Contains(bi.Id)))
                        .SelectMany(c => c.BillItems)
                        .ToListAsync();

                    warehouseListsDetailDto.QuantityComputer = billsWithMatchingItems?.Sum(c => c.ConvertedAddedQuantity - c.ConvertedIssuedQuantity);

                    var itemCardBalances = await _itemCardBalanceRepos.GetAll()
                        .Where(x => x.StoreId == input.storeId && x.ItemCardId == itemCard.Id)
                        .FirstOrDefaultAsync();

                    warehouseListsDetailDto.Price = itemCardBalances?.CostPrice ?? 1;

                    warehouseListsDetailDtos.Add(warehouseListsDetailDto);
                }
            }

            return warehouseListsDetailDtos;
        }
      
        
        public async Task<BillDynamicDeterminantList> GetDynamicDeterminantList(ItemCard.Dto.InventoryDynamicDeterminantInput input)
        {
            var resultOld = new BillDynamicDeterminantList();

            if (input.ItemCardId != null && input.ItemCardId > 0)
            {
                var itemCardDeterminant = await _itemCardDeterminantRepos.GetAllAsNoTracking().Include(c => c.DeterminantsMaster)
                    .ThenInclude(c => c.DeterminantsDetails)
                   .WhereIf(input.ItemCardId != null && input.ItemCardId > 0, c => c.ItemCardId == input.ItemCardId).ToListAsync();

                resultOld.ItemCardDeterminantListDto = new List<ItemCardDeterminantDto>();

                resultOld.ItemCardDeterminantListDto = _mpper.Map<List<ItemCardDeterminantDto>>(itemCardDeterminant);

                var result = new BillDynamicDeterminantList();
                var itemCardDeterminants = await _billDynamicDeterminantRepos.GetAllIncluding()
                        .Where(c => c.ItemCardId == input.ItemCardId)
                       
                        .ToListAsync();


                var res = _mpper.Map<List<InsertBillDynamicDeterminantDto>>(itemCardDeterminants);

                resultOld.DynamicDeterminantListDto = new List<InsertBillDynamicDeterminantDto>();
                resultOld.DynamicDeterminantListDto = res;

                return resultOld;
            }
            return resultOld;
        }

        public virtual async Task<ResponseResult<List<EInvoiceDto>>> GetEInvoice(long billId)
        {
            var sp = "SP_EInvoices";


            var result = _billRepos.Excute<EInvoiceDto>(sp, new List<SqlParameter>() {
                new SqlParameter(){
                    ParameterName = "@billId",
                    Value = billId


                }
              
            }, true);

            return result;
        }
        public virtual async Task<ResponseResult<List<EInvoiceLineDto>>> GetEInvoiceLines(long billId)
        {
            var VW = "select * from VW_EInvoice_Lines where BillId = "+ billId;


            var result = _billRepos.Excute<EInvoiceLineDto>(VW, null, false);

            return result;
        }
        public async Task<int> UploadEInvoices(List<object> ids)
        {
            foreach (var id in ids)
            {

                var bill = await GetEInvoice(Convert.ToInt64(id));

                Document obj = new Document();
                List<_documents> List = new List<_documents>();

                Issuer issuer = new Issuer();

                Address Address = new Address();

                Receiver Receiver = new Receiver();

                Payment Payment = new Payment();


                // issuer.name = bill[0].IssuerName;
                // issuer.id = bill[0].IssuerId;

            }
            return 0;
        }


    }
}
