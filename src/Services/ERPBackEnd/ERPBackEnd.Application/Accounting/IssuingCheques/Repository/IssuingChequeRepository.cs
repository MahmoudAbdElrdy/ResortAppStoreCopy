using AutoMapper;
using Common.Extensions;
using Common.Interfaces;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.IssuingCheques.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.IssuingCheques.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.Shared.Enums;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.JournalsEntries.Repository
{
    public class IssuingChequeRepository : GMappRepository<IssuingChequeMaster, IssuingChequeMasterDto, long>, IIssuingChequeRepository
    {
        private readonly IGRepository<IssuingChequeMaster> _issuingChequeMaster;
        private IGRepository<IssuingChequeDetail> _issuingChequeDetail { get; set; }
        private IGRepository<IssuingChequeStatusDetails> _issuingChequeStatusDetailRepo { get; set; }

        private IAuditService _auditService;
        private IMapper _mpper;
        private IJournalEntriesDetailsRepository _journalEntriesDetailRepos;
        private IGRepository<JournalEntriesMaster> _journalEntriesRepository { get; set; }

        private readonly IGRepository<GeneralConfiguration> _generalConfiguration;
        private IGRepository<JournalEntriesDetail> _journalEntriesDetailRepository { get; set; }


        public IssuingChequeRepository(
            IGRepository<IssuingChequeMaster> mainRepos,
            IAuditService auditService,
            IMapper mapper, DeleteService deleteService,
            IJournalEntriesDetailsRepository JournalEntriesDetailRepos,
            IGRepository<GeneralConfiguration> generalConfiguration,
          IGRepository<IssuingChequeStatusDetails> IssuingChequeStatusDetail,
             IGRepository<IssuingChequeDetail> IssuingChequeDetail,
            IGRepository<JournalEntriesMaster> journalEntriesRepository,
            IGRepository<JournalEntriesDetail> journalEntriesDetailRepository

            )
            : base(mainRepos, mapper, deleteService)
        {
            _issuingChequeMaster = mainRepos;
            _auditService = auditService;
            _mpper = mapper;
            _journalEntriesDetailRepos = JournalEntriesDetailRepos;
            _generalConfiguration = generalConfiguration;
            _journalEntriesRepository = journalEntriesRepository;
            _issuingChequeDetail = IssuingChequeDetail;
            _issuingChequeStatusDetailRepo = IssuingChequeStatusDetail;
            _journalEntriesDetailRepository = journalEntriesDetailRepository;
        }

        public async Task<IssuingChequeMasterDto> CreateIssuingCheque(IssuingChequeMasterDto input)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = await base.Create(input);
                await AddRelationsOfIssuingCheques(result.Id.Value, input, 0);
                scope.Complete();
            }
            return input;
        }


        public async Task AddRelationsOfIssuingCheques(long issuingChequeId, IssuingChequeMasterDto input, int status)
        {
            IssuingChequeStatusDetails _IssuingChequeStatusDetail = new IssuingChequeStatusDetails();
            _IssuingChequeStatusDetail.IssuingChequeId = issuingChequeId;
            _IssuingChequeStatusDetail.Status = status;
            _IssuingChequeStatusDetail.Date = DateTime.Now;

            _issuingChequeStatusDetailRepo.Insert(_IssuingChequeStatusDetail);
            var JournalId = await _generalConfiguration.GetAll().Where(x => x.Code == "1007").FirstOrDefaultAsync();
            var FinancialEntryCycle = await _generalConfiguration.GetAll().Where(x => x.Code == "4").FirstOrDefaultAsync();

            var code = "";

            List<JournalEntriesMaster> JournalEntriesList = _journalEntriesRepository.GetAll().Where(x => x.IsDeleted != true).ToList();
            if (JournalEntriesList.Count > 0)
            {
                code = JournalEntriesList.Select(x => x.Code).LastOrDefault().ToString();
                code = (long.Parse(code) + 1).ToString();

            }
            else
            {
                code = "1";

            }
            JournalEntriesMaster journalEntriesMaster = new JournalEntriesMaster();
            journalEntriesMaster.Code = code.ToString();
            if (JournalId != null)
            {

                if (!string.IsNullOrEmpty(JournalId.Value))
                {
                    journalEntriesMaster.JournalId = long.Parse(JournalId.Value);
                }
                else
                {

                    throw new UserFriendlyException("Journal  is required");
                }
            }
            else
            {

                throw new UserFriendlyException("Journal  is required");
            }
            if (FinancialEntryCycle != null)
            {
                if (!string.IsNullOrEmpty(FinancialEntryCycle.Value))
                {
                    if (FinancialEntryCycle.Value == "1" || FinancialEntryCycle.Value == "2")
                    {
                        journalEntriesMaster.PostType = 2;
                    }
                    else if (FinancialEntryCycle.Value == "3")
                    {
                        journalEntriesMaster.PostType = 1;

                    }

                }
            }

            journalEntriesMaster.CompanyId = input.CompanyId;
            journalEntriesMaster.BranchId = input.BranchId;
            journalEntriesMaster.Date = input.Date;
            journalEntriesMaster.ParentType = (int)EntryTypesEnum.RegisterIssuingCheque;
            journalEntriesMaster.ParentTypeId = issuingChequeId;
            journalEntriesMaster.FiscalPeriodId = input.FiscalPeriodId;

            await _journalEntriesRepository.InsertAsync(journalEntriesMaster);
            _journalEntriesRepository.SaveChanges();
            if (journalEntriesMaster.Id > 0)
            {
                //مع تسجيل الشيك بنعمل قيد من حساب المورد الي حساب اوراق الدفع
                var account = await _generalConfiguration.GetAll().Where(x => x.Code == "8").FirstOrDefaultAsync();
                byte i = 1;
                foreach (var item in input.IssuingChequeDetail)
                {
                    await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i, item.AccountId.ToString(), 0, decimal.Parse(item.Amount.ToString()), 0, decimal.Parse(item.CurrencyLocal.ToString()), decimal.Parse(item.TransactionFactor.ToString()), item.CurrencyId,item.CostCenterId ,item.ProjectId);
                    i++;
                }
                await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i, account.Value.ToString(), decimal.Parse(input.Amount.Value.ToString()), 0, decimal.Parse(input.AmountLocal.Value.ToString()), 0, decimal.Parse(input.CurrencyFactor.Value.ToString()), input.CurrencyId,input.CostCenterId, input.ProjectId);


            }

        }
        public async Task CancelEntryActions(long Id, int action)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var journalEntriesItem = new JournalEntriesMaster();


                    if (action == 4)
                    {
                        journalEntriesItem = await _journalEntriesRepository.GetAllIncluding(c => c.JournalEntriesDetail).AsNoTracking()
                                          .FirstOrDefaultAsync(c => c.ParentTypeId == Id && c.IsDeleted != true
                                          && c.ParentType == (int)EntryTypesEnum.CollectIssuingCheque);
                    }
                    else if (action == 5)
                    {
                        journalEntriesItem = await _journalEntriesRepository.GetAllIncluding(c => c.JournalEntriesDetail).AsNoTracking()
                                          .FirstOrDefaultAsync(c => c.ParentTypeId == Id && c.IsDeleted != true
                                          && c.ParentType == (int)EntryTypesEnum.RejectIssuingCheque);
                    }


                    if (journalEntriesItem != null)
                    {
                        foreach (var item in journalEntriesItem.JournalEntriesDetail.ToList())
                        {

                            await _journalEntriesDetailRepository.SoftDeleteWithoutSaveAsync(item.Id);
                        }

                        await _journalEntriesRepository.SoftDeleteWithoutSaveAsync(journalEntriesItem.Id);
                    }


                    var issuingChequeResult = await _issuingChequeMaster.GetAllIncluding(c => c.IssuingChequeDetail).Include(c => c.IssuingChequeStatusDetails).Where(x => x.Id == Id).FirstOrDefaultAsync();

                    issuingChequeResult.Status = action;
                    await _issuingChequeMaster.UpdateAsync(issuingChequeResult);
                    await _issuingChequeMaster.SaveChangesAsync();

                    IssuingChequeStatusDetails issuingChequeStatusDetail = new IssuingChequeStatusDetails();
                    issuingChequeStatusDetail.IssuingChequeId = Id;
                    issuingChequeStatusDetail.Status = action;
                    issuingChequeStatusDetail.Date = DateTime.Now;


                    await _issuingChequeStatusDetailRepo.InsertAsync(issuingChequeStatusDetail);
                    await _issuingChequeStatusDetailRepo.SaveChangesAsync();
                    scope.Complete();
                }
                catch (Exception ex)
                {

                }
            }
        }
        public async Task GenerateEntryActions(long Id, int action)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var issuingCheque = await _issuingChequeMaster.GetAllIncluding(c => c.IssuingChequeDetail).Where(x => x.Id == Id).FirstOrDefaultAsync();

                    long companyId = Convert.ToInt64(_auditService.CompanyId);
                    long branchId = Convert.ToInt64(_auditService.BranchId);


                    var fiscalPeriodResult = await _generalConfiguration.GetAll().Where(x => x.Code == "7").FirstOrDefaultAsync();
                    var accountPaymentsResult = await _generalConfiguration.GetAll().Where(x => x.Code == "8").FirstOrDefaultAsync();
                    var journalId = await _generalConfiguration.GetAll().Where(x => x.Code == "1007").FirstOrDefaultAsync();
                    var financialEntryCycle = await _generalConfiguration.GetAll().Where(x => x.Code == "4").FirstOrDefaultAsync();

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
                    if (journalId != null)
                    {
                        if (!string.IsNullOrEmpty(journalId.Value))
                        {
                            journalEntriesMaster.JournalId = long.Parse(journalId.Value);
                        }
                        else
                        {

                            throw new UserFriendlyException("Journal  is required");
                        }
                    }
                    else
                    {

                        throw new UserFriendlyException("Journal  is required");
                    }
                    if (financialEntryCycle != null)
                    {
                        if (!string.IsNullOrEmpty(financialEntryCycle.Value))
                        {
                            if (financialEntryCycle.Value == "1" || financialEntryCycle.Value == "2")
                            {
                                journalEntriesMaster.PostType = 2;
                            }
                            else if (financialEntryCycle.Value == "3")
                            {
                                journalEntriesMaster.PostType = 1;

                            }

                        }
                    }
                    journalEntriesMaster.CompanyId = companyId;
                    journalEntriesMaster.BranchId = branchId;
                    if (fiscalPeriodResult != null)
                    {
                        if (!string.IsNullOrEmpty(fiscalPeriodResult.Value))
                        {
                            journalEntriesMaster.FiscalPeriodId = Int64.Parse(fiscalPeriodResult.Value);
                        }
                    }
                    if (action == 2)
                    {
                        journalEntriesMaster.ParentType = (int)EntryTypesEnum.CollectIssuingCheque;
                    }
                    else if (action == 3)
                    {
                        journalEntriesMaster.ParentType = (int)EntryTypesEnum.RejectIssuingCheque;

                    }
                    journalEntriesMaster.ParentTypeId = Id;

                    journalEntriesMaster.Date = DateTime.Now;

                    await _journalEntriesRepository.InsertAsync(journalEntriesMaster);
                    await _journalEntriesRepository.SaveChangesAsync();
                    if (journalEntriesMaster.Id > 0)
                    {
                        if (action == 2)
                        {
                            //لما بحصل الشيك من حساب اوراق الدفع الي حساب البنك
                            await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, 1, accountPaymentsResult.Value.ToString(), 0, decimal.Parse(issuingCheque.Amount.ToString()), 0, decimal.Parse(issuingCheque.AmountLocal.ToString()), decimal.Parse(issuingCheque.CurrencyFactor.Value.ToString()), issuingCheque.CurrencyId, issuingCheque.CostCenterId,issuingCheque.ProjectId);
                            await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, 2, issuingCheque.BankAccountId.ToString(), decimal.Parse(issuingCheque.Amount.ToString()), 0, decimal.Parse(issuingCheque.AmountLocal.ToString()), 0, decimal.Parse(issuingCheque.CurrencyFactor.ToString()), issuingCheque.CurrencyId, issuingCheque.CostCenterId, issuingCheque.ProjectId);

                        }
                        else if (action == 3)
                        {
                            //رفض الشيك من حساب اوراق الدفع الى حساب المورد
                            await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, 1, accountPaymentsResult.Value.ToString(), 0, decimal.Parse(issuingCheque.Amount.Value.ToString()), 0, decimal.Parse(issuingCheque.AmountLocal.Value.ToString()), decimal.Parse(issuingCheque.CurrencyFactor.Value.ToString()), issuingCheque.CurrencyId,issuingCheque.CostCenterId, issuingCheque.ProjectId);

                            byte i = 2;
                            foreach (var item in issuingCheque.IssuingChequeDetail)
                            {
                                await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i, item.AccountId.ToString(), decimal.Parse(item.Amount.ToString()), 0, decimal.Parse(item.CurrencyLocal.ToString()), 0, decimal.Parse(item.TransactionFactor.ToString()), item.CurrencyId, item.CostCenterId, item.ProjectId);
                                i++;
                            }

                        }
                        issuingCheque.Status = action;

                        await _issuingChequeMaster.UpdateAsync(issuingCheque);
                        await _issuingChequeMaster.SaveChangesAsync();

                        IssuingChequeStatusDetails issuingChequeStatusDetail = new IssuingChequeStatusDetails();
                        issuingChequeStatusDetail.IssuingChequeId = issuingCheque.Id;
                        issuingChequeStatusDetail.Status = action;
                        issuingChequeStatusDetail.Date = DateTime.Now;


                        await _issuingChequeStatusDetailRepo.InsertAsync(issuingChequeStatusDetail);
                        await _issuingChequeStatusDetailRepo.SaveChangesAsync();
                    }



                }
                catch (Exception ex)
                {

                }
                scope.Complete();
            }

        }


        public async Task<IssuingChequeMasterDto> UpdateIssuingCheque(IssuingChequeMasterDto input)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                input.CompanyId = Convert.ToInt64(_auditService.CompanyId);
                input.BranchId = Convert.ToInt64(_auditService.BranchId);

                var journalEntriesItem = await _journalEntriesRepository.GetAllIncluding(c => c.JournalEntriesDetail).AsNoTracking()
                    .FirstOrDefaultAsync(c => c.ParentTypeId == input.Id && c.IsDeleted != true
                    && (c.ParentType == (int)EntryTypesEnum.RegisterIssuingCheque || c.ParentType == (int)EntryTypesEnum.CollectIssuingCheque || c.ParentType == (int)EntryTypesEnum.RejectIssuingCheque));

                if (journalEntriesItem != null)
                {
                    foreach (var item in journalEntriesItem.JournalEntriesDetail.ToList())
                    {

                        await _journalEntriesDetailRepository.SoftDeleteWithoutSaveAsync(item.Id);
                    }
                    await _journalEntriesRepository.SoftDeleteWithoutSaveAsync(journalEntriesItem.Id);
                }

                var issuingChequeResult = await FirstInclude(input.Id.Value);


                if (issuingChequeResult?.IssuingChequeDetail != null)
                {
                    foreach (var item in issuingChequeResult?.IssuingChequeDetail)
                    {
                        var entity = _mpper.Map<IssuingChequeDetail>(item);
                        await _issuingChequeDetail.SoftDeleteAsync(entity);
                    } 

                }

                input.CreatedBy = issuingChequeResult.CreatedBy;
                input.CreatedAt = issuingChequeResult.CreatedAt;
                var result = await base.Update(input);
                await AddRelationsOfIssuingCheques(result.Id.Value, input, 1);
                scope.Complete();
            }
            return input;

        }
        public async Task<IssuingChequeMasterDto> FirstInclude(long id)
        {
            var item = await _issuingChequeMaster.GetAllIncluding(c => c.IssuingChequeDetail, c => c.IssuingChequeStatusDetails).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted != true);

            var result = _mpper.Map<IssuingChequeMasterDto>(item);
            return result;
        }
        public async Task<int> DeleteAsync(long id)
        {
            List<JournalEntriesMaster> journalEntriesList = _journalEntriesRepository.GetAll().Include(c => c.JournalEntriesDetail).Where(c => c.ParentTypeId == id && c.IsDeleted != true
            && (c.ParentType == (int)EntryTypesEnum.RegisterIssuingCheque || c.ParentType == (int)EntryTypesEnum.CollectIssuingCheque || c.ParentType == (int)EntryTypesEnum.RejectIssuingCheque)).ToList();
            if (journalEntriesList != null)
            {
                foreach (var journalEntriesItem in journalEntriesList)
                {
                    foreach (var journalEntriesDetailItem in journalEntriesItem.JournalEntriesDetail.ToList())
                    {

                        await _journalEntriesDetailRepository.SoftDeleteWithoutSaveAsync(journalEntriesDetailItem.Id);
                    }

                    await _journalEntriesRepository.SoftDeleteWithoutSaveAsync(journalEntriesItem.Id);
                }

            }
            var issuingChequeResult = await FirstInclude(id);

            if (issuingChequeResult?.IssuingChequeDetail != null)
            {
                foreach (var item in issuingChequeResult?.IssuingChequeDetail)
                {
                    await _issuingChequeDetail.SoftDeleteWithoutSaveAsync(item.Id);
                }

            }
            if (issuingChequeResult?.IssuingChequeStatusDetails != null)
            {
                foreach (var item in issuingChequeResult?.IssuingChequeStatusDetails)
                {
                    await _issuingChequeStatusDetailRepo.SoftDeleteWithoutSaveAsync(item.Id);
                }
            }
            return await base.ScriptCheckDeleteWithDetails(id, new List<string> { "IssuingChequeDetails" }, "IssuingChequeMasters", "Id");


        }
        public async Task<int> DeleteListAsync(List<object> ids)
        {
            foreach (var id in ids)
            {
                List<JournalEntriesMaster> journalEntriesList = _journalEntriesRepository.GetAll().Include(c => c.JournalEntriesDetail).
                    Where(c => c.ParentTypeId == Convert.ToInt64(id) && c.IsDeleted != true && (c.ParentType == (int)EntryTypesEnum.RegisterIssuingCheque || c.ParentType == (int)EntryTypesEnum.CollectIssuingCheque || c.ParentType == (int)EntryTypesEnum.RejectIssuingCheque)).ToList();
                if (journalEntriesList != null)
                {
                    foreach (var journalEntriesItem in journalEntriesList)
                    {
                        foreach (var journalEntriesDetailItem in journalEntriesItem.JournalEntriesDetail.ToList())
                        {

                            await _journalEntriesDetailRepository.SoftDeleteWithoutSaveAsync(journalEntriesDetailItem.Id);
                        }

                        await _journalEntriesRepository.SoftDeleteWithoutSaveAsync(journalEntriesItem.Id);
                    }

                }
                var issuingChequeResult = await FirstInclude(Convert.ToInt64(id));

                if (issuingChequeResult?.IssuingChequeDetail != null)
                {
                    foreach (var item in issuingChequeResult?.IssuingChequeDetail)
                    {
                        await _issuingChequeDetail.SoftDeleteWithoutSaveAsync(item.Id);
                    }

                }
                if (issuingChequeResult?.IssuingChequeStatusDetails != null)
                {
                    foreach (var item in issuingChequeResult?.IssuingChequeStatusDetails)
                    {
                        await _issuingChequeStatusDetailRepo.SoftDeleteWithoutSaveAsync(item.Id);
                    }
                }
            }

            return await base.ScriptCheckDeleteWithDetails(ids, new List<string> { "IssuingChequeDetails" }, "IssuingChequeMasters", "Id");
        }
        public override string LastCode()
        {
            return base.LastCode();
        }
    }
}
