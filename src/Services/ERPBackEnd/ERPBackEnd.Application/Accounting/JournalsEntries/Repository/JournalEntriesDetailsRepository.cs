using AutoMapper;
using Common.Interfaces;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.JournalsEntries.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using System;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.JournalsEntries.Repository
{
    public class JournalEntriesDetailsRepository : GMappRepository<JournalEntriesDetail, JournalEntriesDetailDto, long>, IJournalEntriesDetailsRepository
    {
        private IGRepository<JournalEntriesDetail> _journalEntriesDetail { get; set; }
        private IAuditService _auditService;
        private IMapper _mpper;
       

        public JournalEntriesDetailsRepository(
            IGRepository<JournalEntriesDetail> mainRepos,
            IAuditService auditService,
            IMapper mapper, DeleteService deleteService
            )
            : base(mainRepos, mapper, deleteService)
        {
            _journalEntriesDetail = mainRepos;
            _auditService = auditService;
            _mpper = mapper;
        }


        public async Task createEntryDetails(long masterid, byte orderid, string accountid, decimal? credit, decimal? debit, decimal? creditLocal, decimal? debitLocal, decimal? transactionFactor, long? currencyId,long? CostCenterId,long ? ProjectId)
        {
            try
            {
                JournalEntriesDetail _entryDetails = new JournalEntriesDetail();
                _entryDetails.JournalEntriesMasterId = masterid;
                _entryDetails.EntryRowNumber = orderid;
                _entryDetails.AccountId = accountid;
                _entryDetails.JEDetailCredit = credit;
                _entryDetails.JEDetailDebit = debit;
                _entryDetails.JEDetailCreditLocal = creditLocal;
                _entryDetails.JEDetailDebitLocal = debitLocal;
                _entryDetails.TransactionFactor = transactionFactor;
                _entryDetails.CurrencyId = currencyId;
                _entryDetails.CostCenterId = CostCenterId;
                _entryDetails.ProjectId = ProjectId;


                _entryDetails.JournalEntriesMaster = null;
                await _journalEntriesDetail.InsertAsync(_entryDetails);
                await _journalEntriesDetail.SaveChangesAsync();

            }
            catch (Exception ex)
            {
             }

        }
        
    
    }
}
