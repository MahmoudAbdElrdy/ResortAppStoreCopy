using AutoMapper;
using Common.Extensions;
using Common.Interfaces;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.IncomingCheques.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Shared.Enums;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using IncomingChequeMaster = ResortAppStore.Services.ERPBackEnd.Domain.Accounting.IncomingChequeMaster;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.JournalsEntries.Repository
{
    public class IncomingChequeRepository : GMappRepository<IncomingChequeMaster, IncomingChequeMasterDto, long>, IIncomingChequeRepository
    {
        private readonly IGRepository<IncomingChequeMaster> _incomingChequeMaster;
        private IGRepository<IncomingChequeDetail> _incomingChequeDetail { get; set; }
        private IGRepository<IncomingChequeStatusDetail> _incomingChequeStatusDetailRepo { get; set; }

        private IAuditService _auditService;
        private IMapper _mpper;
        private IJournalEntriesDetailsRepository _journalEntriesDetailRepos;
        private IGRepository<JournalEntriesMaster> _journalEntriesRepository { get; set; }
        private IGRepository<JournalEntriesDetail> _journalEntriesDetailRepository { get; set; }
        private readonly IGRepository<GeneralConfiguration> _generalConfiguration;

        public IncomingChequeRepository(
            IGRepository<IncomingChequeMaster> mainRepos,
            IAuditService auditService,
            IMapper mapper, DeleteService deleteService,
            IJournalEntriesDetailsRepository JournalEntriesDetailRepos,
            IGRepository<GeneralConfiguration> generalConfiguration,
            IGRepository<IncomingChequeStatusDetail> IncomingChequeStatusDetail,

            IGRepository<IncomingChequeDetail> incomingChequeDetail,
            IGRepository<JournalEntriesMaster> journalEntriesRepository,
            IGRepository<JournalEntriesDetail> journalEntriesDetailRepository

            )
            : base(mainRepos, mapper, deleteService)
        {
            _incomingChequeMaster = mainRepos;
            _auditService = auditService;
            _mpper = mapper;
            _journalEntriesDetailRepos = JournalEntriesDetailRepos;
            _generalConfiguration = generalConfiguration;
            _journalEntriesRepository = journalEntriesRepository;
            _incomingChequeDetail = incomingChequeDetail;
            _incomingChequeStatusDetailRepo = IncomingChequeStatusDetail;
            _journalEntriesDetailRepository = journalEntriesDetailRepository;
        }

        public async Task<IncomingChequeMasterDto> CreateIncomingCheque(IncomingChequeMasterDto input)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                var result = await base.Create(input);
                await AddRelationsOfIncomingCheques(result.Id.Value, input, 0);
                scope.Complete();
            }
            return input;
        }


        public async Task AddRelationsOfIncomingCheques(long IncomingChequeId, IncomingChequeMasterDto input, int Status)
        {

            IncomingChequeStatusDetail incomingChequeStatusDetail = new IncomingChequeStatusDetail();
            incomingChequeStatusDetail.IncomingChequeId = IncomingChequeId;
            incomingChequeStatusDetail.Status = Status;
            incomingChequeStatusDetail.Date = DateTime.Now;

            _incomingChequeStatusDetailRepo.Insert(incomingChequeStatusDetail);
            var journalId = await _generalConfiguration.GetAll().Where(x => x.Code == "1007").FirstOrDefaultAsync();
            var financialEntryCycle = await _generalConfiguration.GetAll().Where(x => x.Code == "4").FirstOrDefaultAsync();


            var Code = "";

            List<JournalEntriesMaster> journalEntriesList = _journalEntriesRepository.GetAll().Where(x => x.IsDeleted != true).ToList();
            if (journalEntriesList.Count > 0)
            {
                Code = journalEntriesList.Select(x => x.Code).LastOrDefault().ToString();
                Code = (long.Parse(Code) + 1).ToString();

            }
            else
            {
                Code = "1";

            }
            JournalEntriesMaster journalEntriesMaster = new JournalEntriesMaster();
            journalEntriesMaster.Code = Code.ToString();
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
                        journalEntriesMaster.PostType = (int)PostTypeEnum.NotPost;
                    }
                    else if (financialEntryCycle.Value == "3")
                    {
                        journalEntriesMaster.PostType = (int)PostTypeEnum.Post;

                    }

                }
            }
            journalEntriesMaster.CompanyId = input.CompanyId;
            journalEntriesMaster.BranchId = input.BranchId;
            journalEntriesMaster.Date = input.Date;
            journalEntriesMaster.FiscalPeriodId = input.FiscalPeriodId;

            journalEntriesMaster.ParentType = (int)EntryTypesEnum.RegisterIncomingCheque;
            journalEntriesMaster.ParentTypeId = IncomingChequeId;


            await _journalEntriesRepository.InsertAsync(journalEntriesMaster);
            _journalEntriesRepository.SaveChanges();
            if (journalEntriesMaster.Id > 0)
            {
                //مع تسجيل الشيك بنعمل قيد من حساب اوراق القبض الي حساب العميل
                var account = await _generalConfiguration.GetAll().Where(x => x.Code == "6").FirstOrDefaultAsync();

                await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, 1, account.Value.ToString(), 0, decimal.Parse(input.Amount.Value.ToString()), 0, decimal.Parse(input.AmountLocal.Value.ToString()), decimal.Parse(input.CurrencyFactor.Value.ToString()), input.CurrencyId, input.CostCenterId, input.ProjectId);

                byte i = 2;
                foreach (var item in input.IncomingChequeDetail)
                {
                    await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i, item.AccountId.ToString(), decimal.Parse(item.Amount.ToString()), 0, decimal.Parse(item.CurrencyLocal.ToString()), 0, decimal.Parse(item.TransactionFactor.ToString()), item.CurrencyId,item.CostCenterId, item.ProjectId);
                    i++;
                }
            }

        }
        public async Task CancelEntryActions(long Id, int action)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    var journalEntriesItem = new JournalEntriesMaster();


                    if (action==4)
                    {
                         journalEntriesItem = await _journalEntriesRepository.GetAllIncluding(c => c.JournalEntriesDetail).AsNoTracking()
                                           .FirstOrDefaultAsync(c => c.ParentTypeId == Id && c.IsDeleted != true
                                           && c.ParentType == (int)EntryTypesEnum.CollectIncomingCheque);
                    }
                    else if (action ==5)
                    {
                         journalEntriesItem = await _journalEntriesRepository.GetAllIncluding(c => c.JournalEntriesDetail).AsNoTracking()
                                           .FirstOrDefaultAsync(c => c.ParentTypeId == Id && c.IsDeleted != true
                                           && c.ParentType == (int)EntryTypesEnum.RejectIncomingCheque);
                    }
               

                    if (journalEntriesItem != null)
                    {
                        foreach (var item in journalEntriesItem.JournalEntriesDetail.ToList())
                        {

                            await _journalEntriesDetailRepository.SoftDeleteWithoutSaveAsync(item.Id);
                        }

                        await _journalEntriesRepository.SoftDeleteWithoutSaveAsync(journalEntriesItem.Id);
                    }


                    var incomingChequeResult = await _incomingChequeMaster.GetAllIncluding(c => c.IncomingChequeDetail).Include(c=>c.IncomingChequeStatusDetail).Where(x => x.Id == Id).FirstOrDefaultAsync();

                    incomingChequeResult.Status = action;
                    await _incomingChequeMaster.UpdateAsync(incomingChequeResult);
                    await _incomingChequeMaster.SaveChangesAsync();

                    IncomingChequeStatusDetail incomingChequeStatusDetail = new IncomingChequeStatusDetail();
                    incomingChequeStatusDetail.IncomingChequeId = Id;
                    incomingChequeStatusDetail.Status = action;
                    incomingChequeStatusDetail.Date = DateTime.Now;


                    await _incomingChequeStatusDetailRepo.InsertAsync(incomingChequeStatusDetail);
                    await _incomingChequeStatusDetailRepo.SaveChangesAsync();
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
                    var incomingCheque = await _incomingChequeMaster.GetAllIncluding(c => c.IncomingChequeDetail).Where(x => x.Id == Id).FirstOrDefaultAsync();

                    long companyId = Convert.ToInt64(_auditService.CompanyId);
                    long branchId = Convert.ToInt64(_auditService.BranchId);


                    var fiscalPeriodResult = await _generalConfiguration.GetAll().Where(x => x.Code == "7").FirstOrDefaultAsync();
                    var accountReceivablesResult = await _generalConfiguration.GetAll().Where(x => x.Code == "6").FirstOrDefaultAsync();
                    var journalId = await _generalConfiguration.GetAll().Where(x => x.Code == "1007").FirstOrDefaultAsync();
                    var financialEntryCycle = await _generalConfiguration.GetAll().Where(x => x.Code == "4").FirstOrDefaultAsync();

                    var Code = "";

                    List<JournalEntriesMaster> JournalEntriesList = _journalEntriesRepository.GetAll().Where(x => x.IsDeleted != true).ToList();
                    if (JournalEntriesList.Count > 0)
                    {
                        Code = JournalEntriesList.Select(x => x.Code).LastOrDefault().ToString();
                        Code = (long.Parse(Code) + 1).ToString();

                    }
                    else
                    {
                        Code = "1";

                    }

                    JournalEntriesMaster journalEntriesMaster = new JournalEntriesMaster();
                    journalEntriesMaster.Code = Code.ToString();
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
                    if (financialEntryCycle != null)
                    {
                        if (!string.IsNullOrEmpty(financialEntryCycle.Value))
                        {
                            journalEntriesMaster.FiscalPeriodId = Int64.Parse(financialEntryCycle.Value);
                        }
                    }
                    if (action == 2)
                    {
                        journalEntriesMaster.ParentType = (int)EntryTypesEnum.CollectIncomingCheque;
                    }
                    else if (action == 3)
                    {
                        journalEntriesMaster.ParentType = (int)EntryTypesEnum.RejectIncomingCheque;

                    }
                    journalEntriesMaster.ParentTypeId = Id;

                    journalEntriesMaster.Date = DateTime.Now;



                    await _journalEntriesRepository.InsertAsync(journalEntriesMaster);
                    await _journalEntriesRepository.SaveChangesAsync();
                    if (journalEntriesMaster.Id > 0)
                    {
                        if (action == 2)
                        {
                            //لما بحصل الشيك بعمل قيد من حساب البنك الى حساب اوراق القبض
                            var incomingChequeDetails = _incomingChequeDetail.GetAll().Where(x => x.IncomingChequeId == Id).ToList();
                            
                            await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, 1, incomingCheque.BankAccountId.ToString(), 0, decimal.Parse(incomingCheque.Amount.ToString()), 0, decimal.Parse(incomingCheque.AmountLocal.ToString()), decimal.Parse(incomingCheque.CurrencyFactor.ToString()), incomingCheque.CurrencyId, incomingCheque.CostCenterId, incomingCheque.ProjectId);

                            await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, 2, accountReceivablesResult.Value.ToString(), decimal.Parse(incomingCheque.Amount.ToString()), 0, decimal.Parse(incomingCheque.AmountLocal.ToString()), 0, decimal.Parse(incomingCheque.CurrencyFactor.Value.ToString()), incomingCheque.CurrencyId, incomingCheque.CostCenterId, incomingCheque.ProjectId);


                        }
                        else if (action == 3)
                        {
                            //من حساب العميل الى حساب اوراق القبض
                            byte i = 1;
                            foreach (var item in incomingCheque.IncomingChequeDetail)
                            {
                                await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i, item.AccountId.ToString(), 0, decimal.Parse(item.Amount.ToString()), 0, decimal.Parse(item.CurrencyLocal.ToString()), decimal.Parse(item.TransactionFactor.ToString()), item.CurrencyId, item.CostCenterId, item.ProjectId);
                                i++;
                            }
                            await _journalEntriesDetailRepos.createEntryDetails(journalEntriesMaster.Id, i, accountReceivablesResult.Value.ToString(), decimal.Parse(incomingCheque.Amount.ToString()), 0, decimal.Parse(incomingCheque.AmountLocal.ToString()), 0, decimal.Parse(incomingCheque.CurrencyFactor.Value.ToString()), incomingCheque.CurrencyId, incomingCheque.CostCenterId , incomingCheque.ProjectId);

                        }
                        incomingCheque.Status = action;

                        await _incomingChequeMaster.UpdateAsync(incomingCheque);
                        await _incomingChequeMaster.SaveChangesAsync();

                        IncomingChequeStatusDetail incomingChequeStatusDetail = new IncomingChequeStatusDetail();
                        incomingChequeStatusDetail.IncomingChequeId = incomingCheque.Id;
                        incomingChequeStatusDetail.Status = action;
                        incomingChequeStatusDetail.Date = DateTime.Now;


                        await _incomingChequeStatusDetailRepo.InsertAsync(incomingChequeStatusDetail);
                        await _incomingChequeStatusDetailRepo.SaveChangesAsync();
                    }



                }
                catch (Exception ex)
                {

                }
                scope.Complete();
            }

        }


        public async Task<IncomingChequeMasterDto> UpdateIncomingCheque(IncomingChequeMasterDto input)
        {
            using (TransactionScope scope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                input.CompanyId = Convert.ToInt64(_auditService.CompanyId);
                input.BranchId = Convert.ToInt64(_auditService.BranchId);
                var journalEntriesItem = await _journalEntriesRepository.GetAllIncluding(c => c.JournalEntriesDetail).AsNoTracking().FirstOrDefaultAsync(c =>  c.ParentTypeId == input.Id && c.IsDeleted != true && (c.ParentType == (int)EntryTypesEnum.RegisterIncomingCheque || c.ParentType == (int)EntryTypesEnum.CollectIncomingCheque || c.ParentType == (int)EntryTypesEnum.RejectIncomingCheque));

                if (journalEntriesItem != null)
                {
                    foreach (var item in journalEntriesItem.JournalEntriesDetail.ToList())
                    {

                        await _journalEntriesDetailRepository.SoftDeleteWithoutSaveAsync(item.Id);
                    }

                    await _journalEntriesRepository.SoftDeleteWithoutSaveAsync(journalEntriesItem.Id);
                }
                var incomingChequeResult = await FirstInclude(input.Id.Value);



                if (incomingChequeResult.IncomingChequeDetail != null)
                {
                    foreach (var item in incomingChequeResult.IncomingChequeDetail.ToList())
                    {
                        var entity = _mpper.Map<IncomingChequeDetail>(item);
                        await _incomingChequeDetail.SoftDeleteAsync(entity);
                    }

                }
                if (incomingChequeResult.IncomingChequeStatusDetail != null)
                {
                    foreach (var item in incomingChequeResult.IncomingChequeStatusDetail.ToList())
                    {
                        var entity = _mpper.Map<IncomingChequeStatusDetail>(item);
                        await _incomingChequeStatusDetailRepo.SoftDeleteAsync(entity);
                    }

                }

                input.CreatedBy = incomingChequeResult.CreatedBy;
                input.CreatedAt = incomingChequeResult.CreatedAt;

                var result = await base.Update(input);
                await AddRelationsOfIncomingCheques(result.Id.Value, input, 1);
                scope.Complete();
            }
            return input;

        }
        public async Task<IncomingChequeMasterDto> FirstInclude(long id)
        {
            var item = await _incomingChequeMaster.GetAllIncluding(c => c.IncomingChequeDetail, c => c.IncomingChequeStatusDetail).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id && c.IsDeleted != true);

            var result = _mpper.Map<IncomingChequeMasterDto>(item);
            return result;
        }
        public async Task<int> DeleteAsync(long id)
        {

            List<JournalEntriesMaster> journalEntriesList = _journalEntriesRepository.GetAll().Include(c => c.JournalEntriesDetail).Where(c =>  c.ParentTypeId == id && c.IsDeleted != true && (c.ParentType == (int)EntryTypesEnum.RegisterIncomingCheque || c.ParentType == (int)EntryTypesEnum.CollectIncomingCheque || c.ParentType == (int)EntryTypesEnum.RejectIncomingCheque) ).ToList();

            if (journalEntriesList != null)
            {
                foreach (var JournalEntriesItem in journalEntriesList)
                {
                    foreach (var JournalEntriesDetailItem in JournalEntriesItem.JournalEntriesDetail.ToList())
                    {

                        await _journalEntriesDetailRepository.SoftDeleteWithoutSaveAsync(JournalEntriesDetailItem.Id);
                    }

                    await _journalEntriesRepository.SoftDeleteWithoutSaveAsync(JournalEntriesItem.Id);
                }

            }
            var incomingChequeResult = await FirstInclude(id);

            if (incomingChequeResult.IncomingChequeDetail != null)
            {
                foreach (var item in incomingChequeResult.IncomingChequeDetail.ToList())
                {

                    await _incomingChequeDetail.SoftDeleteWithoutSaveAsync(item.Id);
                }

            }
            if (incomingChequeResult.IncomingChequeStatusDetail != null)
            {
                foreach (var item in incomingChequeResult.IncomingChequeStatusDetail.ToList())
                {

                    await _incomingChequeStatusDetailRepo.SoftDeleteWithoutSaveAsync(item.Id);
                }

            }
            return await base.ScriptCheckDeleteWithDetails(id, new List<string> { "IncomingChequeDetails" }, "IncomingChequeMasters", "Id");

        }
        public async Task<int> DeleteListAsync(List<object> ids)
        {
            foreach (var id in ids)
            {
                List<JournalEntriesMaster> journalEntriesList = _journalEntriesRepository.GetAll().Include(c => c.JournalEntriesDetail).Where(c => c.ParentTypeId == Convert.ToInt64(id) && c.IsDeleted != true && (c.ParentType == (int)EntryTypesEnum.RegisterIncomingCheque || c.ParentType == (int)EntryTypesEnum.CollectIncomingCheque || c.ParentType == (int)EntryTypesEnum.RejectIncomingCheque)).ToList();

                if (journalEntriesList != null)
                {
                    foreach (var JournalEntriesItem in journalEntriesList)
                    {
                        foreach (var JournalEntriesDetailItem in JournalEntriesItem.JournalEntriesDetail.ToList())
                        {

                            await _journalEntriesDetailRepository.SoftDeleteWithoutSaveAsync(JournalEntriesDetailItem.Id);
                        }

                        await _journalEntriesRepository.SoftDeleteWithoutSaveAsync(JournalEntriesItem.Id);
                    }

                }
                var incomingChequeResult = await FirstInclude(Convert.ToInt64(id));

                if (incomingChequeResult.IncomingChequeDetail != null)
                {
                    foreach (var item in incomingChequeResult.IncomingChequeDetail.ToList())
                    {

                        await _incomingChequeDetail.SoftDeleteWithoutSaveAsync(item.Id);
                    }

                }
                if (incomingChequeResult.IncomingChequeStatusDetail != null)
                {
                    foreach (var item in incomingChequeResult.IncomingChequeStatusDetail.ToList())
                    {

                        await _incomingChequeStatusDetailRepo.SoftDeleteWithoutSaveAsync(item.Id);
                    }

                }
            }
            return await base.ScriptCheckDeleteWithDetails(ids, new List<string> { "IncomingChequeDetails" }, "IncomingChequeMasters", "Id");
        }
        public override string LastCode()
        {
            return base.LastCode();
        }
    }
}
