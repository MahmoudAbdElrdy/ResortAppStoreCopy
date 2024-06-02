using AutoMapper;
using Common.Infrastructures;
using Common.Interfaces;
using Common.Repositories;
using Common.Services.Service;
using Configuration.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using Microsoft.Identity.Client;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.JournalsEntries.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.JournalsEntries.Repository
{
    public class JournalEntriesRepository : GMappRepository<JournalEntriesMaster, JournalEntriesMasterDto, long>, IJournalEntriesRepository
    {
        private IGRepository<JournalEntriesMaster> _journalEntriesMaster { get; set; }
      
        private IGRepository<GeneralConfiguration> _generalConfiguration { get; set; }
        private IGRepository<JournalEntriesDetail> _journalEntriesDetail { get; set; }
        private IAuditService _auditService;
        private IMapper _mpper;
        private readonly IDapperRepository<JournalEntriesDto> _query;

        public JournalEntriesRepository(
            IGRepository<JournalEntriesMaster> mainRepos,
            IAuditService auditService,
            IMapper mapper, DeleteService deleteService,
            IGRepository<JournalEntriesDetail> journalEntriesDetail,
          
            IGRepository<GeneralConfiguration> generalConfiguration,
            IDapperRepository<JournalEntriesDto> query

            )
            : base(mainRepos, mapper, deleteService)
        {
            _journalEntriesMaster = mainRepos;
            _auditService = auditService;
            _mpper = mapper;
            _journalEntriesDetail = journalEntriesDetail;
         
            _generalConfiguration = generalConfiguration;
            _query = query;
        }

        public async Task<PaginatedList<JournalEntriesMasterDto>> GetAllList(Paging paging)
        {
            var res =await base.GetAllIncluding(paging, c => c.Journal);
           
            //var check = await _generalConfiguration.FirstOrDefaultAsync(c => c.Code == "7");
          
            //if (check != null)
            //{
            //    var value = check.Value;
              
            //    var fiscalPeriodItem = await _FiscalPeriod.FirstOrDefaultAsync(c => c.Id == Int64.Parse(value));
             
            //    if (fiscalPeriodItem != null)
            //    {
            //        res.Items = res.Items.Where(c => c.Date.Value >= fiscalPeriodItem.FromDate && c.Date.Value <= fiscalPeriodItem.ToDate).ToList();
                   
            //    }

            //}
           
            return  res;
        }
        public async Task<JournalEntriesMasterDto> FirstInclude(long id)
        {
            var item = await _journalEntriesMaster.GetAllIncluding(c => c.JournalEntriesDetail, c => c.Journal).FirstOrDefaultAsync(c => c.Id == id);
            var result = _mpper.Map<JournalEntriesMasterDto>(item);
            return result;
        }
        public async Task<int> DeleteAsync(long id)
        {
            var details = await _journalEntriesMaster.GetAllIncluding(c => c.JournalEntriesDetail).FirstOrDefaultAsync(c => c.Id == id);

            if (details.JournalEntriesDetail.Count > 0)
            {
                foreach (var item in details.JournalEntriesDetail)
                {

                    await _journalEntriesDetail.SoftDeleteWithoutSaveAsync(item.Id);
                }
            }

            return await base.ScriptCheckDeleteWithDetails(id, new List<string> { "JournalEntriesDetails" }, "JournalEntriesMasters", "Id");
        }
        public async Task<int> DeleteListAsync(List<object> ids)
        {
            var details = _journalEntriesMaster.GetAllIncluding(c => c.JournalEntriesDetail).Where(c => ids.Contains(c.Id)).ToList();

            foreach (var item in details)
            {

                if (item.JournalEntriesDetail.Count > 0)
                {
                    foreach (var itemEntity in item.JournalEntriesDetail)
                    {

                        await _journalEntriesDetail.SoftDeleteWithoutSaveAsync(itemEntity.Id);
                    }
                }
            }

            return await base.ScriptCheckDeleteWithDetails(ids, new List<string> { "JournalEntriesDetails" }, "JournalEntriesMasters", "Id");
        }
        public override string LastCode()
        {
            return base.LastCode();
        }

        public async Task<int> UpdateListAsync(List<long> ids)
        {
            var details = _journalEntriesMaster.GetAllIncluding().Where(c => ids.Contains(c.Id)).ToList();

            foreach (var item in details)
            {
                if (item.PostType == null || item.PostType == 2)
                {
                    item.PostType = 1;
                }
                else
                {
                    item.PostType = 2;
                }

                await _journalEntriesMaster.UpdateAsync(item);
            }

            var save = await _journalEntriesMaster.SaveChangesAsync();
            return save;
        }
        public async Task<List<JournalEntriesDto>> GetJournalEntries()
        {
            long companyId = Convert.ToInt64(_auditService.CompanyId);
            long branchId = Convert.ToInt64(_auditService.BranchId);
            var fiscalPeriodResult = await _generalConfiguration.GetAll().Where(x => x.Code == "7").FirstOrDefaultAsync();
           

            StringBuilder query = new StringBuilder();
            query.Append(" select *  from dbo.[fn_journal_entries] ");
            query.AppendFormat(" ( {0} ,  ", companyId);
            query.AppendFormat("  {0} ,  ", branchId);
            if(string.IsNullOrEmpty(fiscalPeriodResult.Value))
            {
                query.Append(" 0 , ");

            }
            else
            {
                query.AppendFormat("  {0} ,  ", fiscalPeriodResult.Value);

            }
            query.Append(" null ) ");

            var result = await _query.QueryAsync<JournalEntriesDto>(query.ToString());
            return (List<JournalEntriesDto>)result;


        }
        public async Task<List<JournalEntriesDto>> GetJournalEntryById(long Id)
        {
            StringBuilder query = new StringBuilder();
            query.Append(" select * from dbo.[fn_journal_entries] ");
            query.Append(" ( null, ");
            query.Append(" null , ");
            query.Append("  null, ");
            query.AppendFormat("  {0} ) ", Id);


            var result = await _query.QueryAsync<JournalEntriesDto>(query.ToString());
            return (List<JournalEntriesDto>)result;


        }
    }
}
