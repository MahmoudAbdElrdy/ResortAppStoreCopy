using AutoMapper;
using Common.Constants;
using Common.Interfaces;
using Common.Repositories;
using Common.Services.Service;
using Configuration.Entities;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.JournalsEntries.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Shared.Enums;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Bills.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.Data.SqlClient;
using Common.Extensions;


namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Repository
{
    public class VoucherRepository : GMappRepository<Voucher, VoucherDto, long>, IVoucherRepository
    {
        private readonly IGRepository<Voucher> _voucherRepos;
        private IGRepository<VoucherDetail> _voucherDetailRepos { get; set; }
        private IGRepository<BillPay> _billPayRepos { get; set; }
        private readonly IDapperRepository<NotGenerateEntryVoucherDto> _query;
        private readonly IGRepository<VoucherType> _voucherTypeRepos;
        private IJournalEntriesDetailsRepository _journalEntriesDetailRepos;
        private IGRepository<JournalEntriesMaster> _journalEntriesRepository { get; set; }
        private readonly IGRepository<GeneralConfiguration> _generalConfiguration;
        private IGRepository<JournalEntriesDetail> _journalEntriesDetailRepository { get; set; }
        private IMapper _mpper;
        private IAuditService _auditService;
        private readonly IGRepository<Bill> _billRepos;
        private IBillRepository _billRepository;
        private IGRepository<BillInstallmentDetail> _billInstallmentDetailRepos { get; set; }



        public VoucherRepository(IGRepository<Voucher> mainRepos, IMapper mapper, DeleteService deleteService
            , IGRepository<VoucherType> voucherTypeRepos
            , IJournalEntriesDetailsRepository journalEntriesDetailRepos,
             IGRepository<Role> roleRepository,
        IGRepository<JournalEntriesMaster> journalEntriesRepository,
            IGRepository<GeneralConfiguration> generalConfiguration,
            IGRepository<JournalEntriesDetail> journalEntriesDetailRepository,
            IGRepository<VoucherDetail> voucherDetailRepos,
             IGRepository<BillPay> billPayRepos,
            IDapperRepository<NotGenerateEntryVoucherDto> queryDb,
            IGRepository<Bill> billRepos,
            IBillRepository billRepository,
             IGRepository<BillInstallmentDetail> billInstallmentDetailRepos,

        IAuditService auditService

            ) : base(mainRepos, mapper, deleteService)
        {
            _voucherRepos = mainRepos;
           
            _voucherTypeRepos = voucherTypeRepos;
            _journalEntriesDetailRepos = journalEntriesDetailRepos;
            _journalEntriesRepository = journalEntriesRepository;
            _generalConfiguration = generalConfiguration;
            _journalEntriesDetailRepository = journalEntriesDetailRepository;
            _mpper = mapper;
            _voucherDetailRepos = voucherDetailRepos;
            _billPayRepos = billPayRepos;
            _query = queryDb;
            _auditService = auditService;
            _billRepos = billRepos;
            _billRepository = billRepository;
            _billInstallmentDetailRepos = billInstallmentDetailRepos;

        }
        public async Task<VoucherDto> FirstInclude(long id)
        {
            var item = await _voucherRepos.GetAllIncluding(c => c.VoucherDetail).Include(c=>c.BillPay).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted !=true);

            var result = _mpper.Map<VoucherDto>(item);
            return result;
        }
        public  async Task<int> DeleteAsync(long id)
        {
            var journalEntriesItem = await _journalEntriesRepository.GetAllIncluding(c => c.JournalEntriesDetail).AsNoTracking().FirstOrDefaultAsync(c => c.ParentType == (int)EntryTypesEnum.Voucher & c.ParentTypeId == id && c.IsDeleted != true);

            if (journalEntriesItem != null)
            {
                foreach (var item in journalEntriesItem.JournalEntriesDetail.ToList())
                {

                    await _journalEntriesDetailRepository.SoftDeleteWithoutSaveAsync(item.Id);
                }
                await _journalEntriesRepository.SoftDeleteWithoutSaveAsync(journalEntriesItem.Id);
            }
            var voucherResult = await FirstInclude(id);

            if (voucherResult?.VoucherDetail != null)
            {
                foreach (var item in voucherResult?.VoucherDetail)
                {
                    await _voucherDetailRepos.SoftDeleteWithoutSaveAsync(item.Id);
                }

            }
            if (voucherResult?.BillPay != null)
            {
                foreach (var item in voucherResult?.BillPay)
                {
                    await _billPayRepos.SoftDeleteWithoutSaveAsync(item.Id);

                    var billResult = await _billRepository.FirstInclude(item.BillId);

                    double? Paid = billResult.Paid;
                    if (Paid == null)
                    {
                        Paid = 0;
                    }
                    double remaining = billResult.Remaining.Value;

                    billResult.Paid = Paid - (item.Amount.Value * (1 / billResult.CurrencyValue.Value));
                    billResult.Remaining = remaining + (item.Amount.Value * (1 / billResult.CurrencyValue.Value));
                    var _result = _mpper.Map<Bill>(billResult);
                    _billRepos.Update(_result);
                    _billRepos.SaveChanges();
                    if (item.PaidInstallment > 0)
                    {
                        var billInstallmentDetail = await _billInstallmentDetailRepos.FirstOrDefaultAsync(x => x.Id == item.BillInstallmentId);
                        if (billInstallmentDetail != null)
                        {
                            if (item.PaidInstallment > 0)
                            {
                                billInstallmentDetail.Paid -= item.PaidInstallment;

                            }
                            if (item.RemainingInstallment > 0)
                            {
                                billInstallmentDetail.Remaining -= item.RemainingInstallment;
                            }
                            if (billInstallmentDetail.Remaining <= 0)
                            {
                                billInstallmentDetail.State = 1;
                            }
                            else
                            {
                                billInstallmentDetail.State = 0;

                            }
                            _billInstallmentDetailRepos.Update(billInstallmentDetail);
                        }
                    }
                }

            }
          
            return await base.ScriptCheckDeleteWithDetails(id, new List<string> { "BillPays", "VoucherDetails" }, "Vouchers", "Id");

           
        }
        public async Task<int> DeleteListAsync(List<object> ids)
        {
            foreach (var id in ids)
            {
                var journalEntriesItem = await _journalEntriesRepository.GetAllIncluding(c => c.JournalEntriesDetail).AsNoTracking().FirstOrDefaultAsync(c => c.ParentType == (int)EntryTypesEnum.Voucher & c.ParentTypeId == Convert.ToInt64(id) && c.IsDeleted != true);

                if (journalEntriesItem != null)
                {
                    foreach (var item in journalEntriesItem.JournalEntriesDetail.ToList())
                    {

                        await _journalEntriesDetailRepository.SoftDeleteWithoutSaveAsync(item.Id);
                    }
                    await _journalEntriesRepository.SoftDeleteWithoutSaveAsync(journalEntriesItem.Id);
                }
                var voucherResult = await FirstInclude(Convert.ToInt64(id));

                if (voucherResult?.VoucherDetail != null)
                {
                    foreach (var item in voucherResult?.VoucherDetail)
                    {
                        await _voucherDetailRepos.SoftDeleteWithoutSaveAsync(item.Id);
                    }

                }
                if (voucherResult?.BillPay != null)
                {
                    foreach (var item in voucherResult?.BillPay)
                    {
                        await _billPayRepos.SoftDeleteWithoutSaveAsync(item.Id);

                        var billResult = await _billRepository.FirstInclude(item.BillId);

                        double? Paid = billResult.Paid;
                        if(Paid == null)
                        {
                            Paid = 0;
                        }
                        double remaining = billResult.Remaining.Value;

                        billResult.Paid = Paid - (item.Amount.Value * (1 / billResult.CurrencyValue.Value));
                        billResult.Remaining = remaining + (item.Amount.Value * (1 / billResult.CurrencyValue.Value));
                        var _result = _mpper.Map<Bill>(billResult);
                        _billRepos.Update(_result);
                        _billRepos.SaveChanges();

                        if (item.PaidInstallment > 0)
                        {
                            var billInstallmentDetail = await _billInstallmentDetailRepos.FirstOrDefaultAsync(x => x.Id == item.BillInstallmentId);
                            if (billInstallmentDetail != null)
                            {
                                if (item.PaidInstallment > 0)
                                {
                                    billInstallmentDetail.Paid -= item.PaidInstallment;

                                }
                                if (item.RemainingInstallment > 0)
                                {
                                    billInstallmentDetail.Remaining -= item.RemainingInstallment;
                                }
                                if (billInstallmentDetail.Remaining <= 0)
                                {
                                    billInstallmentDetail.State = 1;
                                }
                                else
                                {
                                    billInstallmentDetail.State = 0;

                                }
                                _billInstallmentDetailRepos.Update(billInstallmentDetail);
                            }
                        }
                    }

                }
               

            }

            return await base.ScriptCheckDeleteWithDetails(ids, new List<string> { "BillPays", "BillInstallmentPays", "VoucherDetails" }, "Vouchers", "Id");
        }
        public async Task<VoucherDto> CreateVoucher(VoucherDto input)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                if(string.IsNullOrEmpty(input.Code))
                {
                    input.Code = await getLastVoucherCodeByTypeId(input.VoucherTypeId);
                }
                var entity = _mpper.Map<Voucher>(input);
                entity.CreatedBy = _auditService.UserId;
                entity.CreatedAt = DateTime.Now;
                var result = await base.CreateTEntity(entity);
               
                List<VoucherType> voucherType = new List<VoucherType>();

                List<long> lst = new List<long>();
                voucherType = _voucherTypeRepos.GetAll().Where(x => x.Id == result.VoucherTypeId).ToList();

                if (voucherType[0].VoucherKindId == (int)VoucherTypesEnum.SimpleDeposit || voucherType[0].VoucherKindId == (int)VoucherTypesEnum.SimpleWithdrawal)
                {
                    if (result.BillPay.Count() > 0)
                    {
                        foreach (var item in result.BillPay)
                        {
                            var billResult = await _billRepository.FirstInclude(item.BillId);

                            double? Paid = billResult.Paid;
                            if (Paid == null)
                            {
                                Paid = 0;
                            }
                            double remaining = billResult.Remaining.Value;
                            if (item.Amount > 0)
                            {
                                billResult.Paid = Paid + (item.Amount.Value * (1 / billResult.CurrencyValue.Value));
                                billResult.Remaining = remaining - (item.Amount.Value * (1 / billResult.CurrencyValue.Value));
                            }
                            var _result = _mpper.Map<Bill>(billResult);
                            _billRepos.Update(_result);
                            _billRepos.SaveChanges();

                            if(item.PaidInstallment>0)
                            {
                                var billInstallmentDetail = await _billInstallmentDetailRepos.FirstOrDefaultAsync(x => x.Id == item.BillInstallmentId);
                                if (billInstallmentDetail != null)
                                {
                                    if (item.PaidInstallment > 0)
                                    {
                                        billInstallmentDetail.Paid = item.PaidInstallment;

                                    }
                                    billInstallmentDetail.Remaining = item.RemainingInstallment;

                                    if (item.RemainingInstallment > 0)
                                    {
                                        billInstallmentDetail.State = 0;
                                    }
                                    else
                                    {
                                        billInstallmentDetail.State = 1;
                                    }
                                    _billInstallmentDetailRepos.Update(billInstallmentDetail);
                                    _billInstallmentDetailRepos.SaveChanges();

                                }
                            }

                        }
                    }
                }

                if (voucherType[0].CreateFinancialEntryId == (int)CreateFinancialEntryEnum.CreateTheEntryAutomatically)
                {
                    lst.Add(result.Id);
                    await generateEntry(lst);
                }
                scope.Complete();
                return result;

            }
        }
        public async Task<VoucherDto> UpdateVoucher(VoucherDto input)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var journalEntriesItem = await _journalEntriesRepository.GetAllIncluding(c => c.JournalEntriesDetail).AsNoTracking().FirstOrDefaultAsync(c => c.ParentType == (int)EntryTypesEnum.Voucher & c.ParentTypeId == input.Id && c.IsDeleted != true);

                if (journalEntriesItem != null)
                {
                    foreach (var item in journalEntriesItem.JournalEntriesDetail.ToList())
                    {

                        await _journalEntriesDetailRepository.SoftDeleteWithoutSaveAsync(item.Id);
                    }
                    await _journalEntriesRepository.SoftDeleteWithoutSaveAsync(journalEntriesItem.Id);
                }
                var voucherResult = await FirstInclude(input.Id);

                if (voucherResult?.VoucherDetail != null)
                {
                    foreach (var item in voucherResult?.VoucherDetail)
                    {
                        var entity = _mpper.Map<VoucherDetail>(item);
                        await _voucherDetailRepos.SoftDeleteAsync(entity);
                    }

                }
                if (input.VoucherDetail != null)
                {
                    foreach (var item in input.VoucherDetail)
                    {
                        item.Id = 0;

                    }
                }
                if (voucherResult?.BillPay != null)
                {
                    foreach (var item in voucherResult?.BillPay)
                    {
                        await _billPayRepos.SoftDeleteWithoutSaveAsync(item.Id);
                    }

                }

              
                if (input.BillPay != null)
                {
                    foreach (var item in input.BillPay)
                    {
                        item.Id = 0;

                    }
                }

                input.CreatedBy = voucherResult.CreatedBy;
                input.CreatedAt = voucherResult.CreatedAt;

                input.UpdateBy = _auditService.UserId;
                input.UpdatedAt = DateTime.Now;

                var result = await base.UpdateWithoutCheckCode(input);
                List<VoucherType> voucherType = new List<VoucherType>();

                List<long> lst = new List<long>();
                voucherType = _voucherTypeRepos.GetAll().Where(x => x.Id == result.VoucherTypeId).ToList();

               

                if (voucherType[0].CreateFinancialEntryId == (int)CreateFinancialEntryEnum.CreateTheEntryAutomatically)
                {
                    lst.Add(result.Id);
                    await generateEntry(lst);
                }
                scope.Complete();
                return result;

            }

        }
        public async Task generateEntry(List<long> ids)
        {
            var fiscalPeriodResult = await _generalConfiguration.GetAll().Where(x => x.Code == "7").FirstOrDefaultAsync();
            long? fiscalPeriodId = 0;

            if (fiscalPeriodResult != null)
            {
                if (!string.IsNullOrEmpty(fiscalPeriodResult.Value))
                {
                    fiscalPeriodId = long.Parse(fiscalPeriodResult.Value);
                }
            }

            if(fiscalPeriodId > 0)
            {
                using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    List<VoucherType> voucherType = new List<VoucherType>();
                    if (ids != null)
                    {
                        foreach (var id in ids)
                        {
                            var voucher = await FirstInclude(id);
                            voucherType = _voucherTypeRepos.GetAll().Where(x => x.Id == voucher.VoucherTypeId).ToList();

                            if (voucherType != null)
                            {

                                var code = "";


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
                                journalEntriesMaster.CompanyId = voucher.CompanyId;
                                journalEntriesMaster.BranchId = voucher.BranchId;
                                journalEntriesMaster.Date = voucher.VoucherDate;
                                journalEntriesMaster.ParentType = (int)EntryTypesEnum.Voucher;
                                journalEntriesMaster.ParentTypeId = voucher.Id;
                                journalEntriesMaster.FiscalPeriodId = fiscalPeriodId;

                                if (voucherType[0].PostingToAccountsAutomatically == true)
                                {
                                    journalEntriesMaster.PostType = (int)PostTypeEnum.Post;
                                }
                                else
                                {
                                    journalEntriesMaster.PostType = (int)PostTypeEnum.NotPost;

                                }
                                if(voucherType[0].JournalId > 0)
                                {
                                    journalEntriesMaster.JournalId = voucherType[0].JournalId;

                                }
                                else
                                {

                                    throw new UserFriendlyException("Journal  is required");
                                }


                                await _journalEntriesRepository.InsertAsync(journalEntriesMaster);
                                _journalEntriesRepository.SaveChanges();

                                byte i = 1;
                                if (voucherType[0].VoucherKindId == (int)VoucherTypesEnum.Deposit || voucherType[0].VoucherKindId == (int)VoucherTypesEnum.SimpleDeposit)
                                {
                                    //من حساب النقدية الى حساب الجهة
                                    await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, voucher.CashAccountId.ToString(), 0, decimal.Parse(voucher.VoucherTotal.ToString()), 0, decimal.Parse(voucher.VoucherTotalLocal.ToString()), 1, voucher.CurrencyId, voucher.CostCenterId,voucher.ProjectId);

                                    foreach (var item in voucher.VoucherDetail)
                                    {
                                        await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, item.BeneficiaryAccountId.ToString(), decimal.Parse(item.Credit.Value.ToString()), 0, decimal.Parse(item.CreditLocal.Value.ToString()), 0, decimal.Parse(item.CurrencyConversionFactor.Value.ToString()), item.CurrencyId, item.CostCenterId,item.ProjectId);

                                    }


                                }
                                else if (voucherType[0].VoucherKindId == (int)VoucherTypesEnum.Withdrawal || voucherType[0].VoucherKindId == (int)VoucherTypesEnum.SimpleWithdrawal)
                                {
                                    //من حساب الجهة الى حساب النقدية
                                    foreach (var item in voucher.VoucherDetail)
                                    {
                                        await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, item.BeneficiaryAccountId.ToString(), 0, decimal.Parse(item.Debit.ToString()), 0, decimal.Parse(item.DebitLocal.ToString()), decimal.Parse(item.CurrencyConversionFactor.ToString()), item.CurrencyId, item.CostCenterId,item.ProjectId);

                                    }
                                    await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i++, voucher.CashAccountId.ToString(), decimal.Parse(voucher.VoucherTotal.ToString()), 0, decimal.Parse(voucher.VoucherTotalLocal.ToString()), 0, 1, voucher.CurrencyId, voucher.CostCenterId,voucher.ProjectId);

                                }

                                voucher.IsGenerateEntry = true;
                                await base.UpdateWithoutCheckCode(voucher);
                            }

                        }
                    }
                    scope.Complete();
                }
            }
           
            
        }
        #region Get Last Code  
        public async Task<string> getLastVoucherCodeByTypeId(long typeId)
        {
            
            var code = "";
            var CodingPolicy = 0;
            var voucherTypeResult = await _voucherTypeRepos.GetAll().Where(x => x.Id == typeId).FirstOrDefaultAsync();

            if (voucherTypeResult != null)
            {
                CodingPolicy = voucherTypeResult.SerialTypeId;
            }

            List<Voucher> vouchersList = new List<Voucher>();

            if (CodingPolicy == (int)CodingPolicyEnum.Automatic)
            {
                vouchersList = _voucherRepos.GetAll().Where(x => x.VoucherTypeId == typeId && x.IsDeleted != true).ToList();
            }
            else if (CodingPolicy == (int)CodingPolicyEnum.AutomaticDependingOnTheUser)
            {
                var userId = _auditService.UserId;
                vouchersList = _voucherRepos.GetAll().Where(x => x.VoucherTypeId == typeId && x.IsDeleted != true && x.CreatedBy == userId).ToList();

            }

            if (vouchersList.Count > 0 && vouchersList != null)
            {
                code = vouchersList.Select(x => x.Code).LastOrDefault().ToString();

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
        public async Task<List<NotGenerateEntryVoucherDto>> GetNotGenerateEntryVouchers()
        {
            long companyId = Convert.ToInt64(_auditService.CompanyId);
            long branchId = Convert.ToInt64(_auditService.BranchId);
            var fiscalPeriodResult = await _generalConfiguration.GetAll().Where(x => x.Code == "7").FirstOrDefaultAsync();

            long fiscalPeriodIdValue = 0;
            if (!string.IsNullOrEmpty(fiscalPeriodResult.Value))
            {
                fiscalPeriodIdValue = long.Parse(fiscalPeriodResult.Value);
            }
            


            StringBuilder query = new StringBuilder();
            query.Append(" select v.Id,t.CompanyId, t.BranchId,  v.VoucherTypeId, t.nameAr,t.nameEn, t.VoucherKindId, v.Code,");
            query.Append(" case when t.VoucherKindId = 1 then 'Deposit' when t.VoucherKindId = 2 then 'Withdrawal' end as VoucherKindEn , ");
            query.Append(" case when t.VoucherKindId = 1 then N'قبض' when t.VoucherKindId = 2 then N'صرف' end as VoucherKindAr , ");
            query.Append(" v.VoucherDate, v.VoucherTotalLocal from Vouchers as v join VoucherTypes as t on v.VoucherTypeId = t.Id ");
            query.AppendFormat(" where  t.CreateFinancialEntryId = {0}", (int)CreateFinancialEntryEnum.TheEntryIsNotCreatedAutomatically);
            query.Append("  and v.IsDeleted !=1 ");
            query.AppendFormat("  and v.CompanyId = {0} ", companyId);
            query.AppendFormat("  and v.BranchId = {0} ", branchId);
            query.AppendFormat("  and v.FiscalPeriodId = {0} ", fiscalPeriodIdValue);
            query.Append("  and (v.IsGenerateEntry is null or v.IsGenerateEntry = 0) ");


            var result = await _query.QueryAsync<NotGenerateEntryVoucherDto>(query.ToString());
            return (List<NotGenerateEntryVoucherDto>)result;


        }

        public virtual async Task<ResponseResult<List<BillPaymentDto>>> GetVouchersBillPays(long VoucherId)
        {
            var sp = "SP_Get_Vouchers_Bill_Pays";


            var result = _voucherRepos.Excute<BillPaymentDto>(sp, new List<SqlParameter>() {
                new SqlParameter(){
                    ParameterName = "@voucherId",
                    Value = VoucherId,


                },

            }, true);

            return result;
        }



    }
}
