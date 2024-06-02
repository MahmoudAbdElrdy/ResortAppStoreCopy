using AutoMapper;
using Common.Extensions;
using Common.Interfaces;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.FiscalPeriods.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.JournalsEntries.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.Shared.Enums;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Accounting.FiscalPeriods.Repository
{
    public class FiscalPeriodRepository : GMappRepository<FiscalPeriod, FiscalPeriodDto, long>, IFiscalPeriodRepository
    {
        private readonly IGRepository<FiscalPeriod> _fiscalPeriodRepos;
        private IGRepository<JournalEntriesMaster> _journalEntriesRepository { get; set; }
        private IGRepository<JournalEntriesDetail> _journalEntriesDetailRepository { get; set; }
        private IJournalEntriesDetailsRepository _journalEntriesDetailRepos;
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        private IMapper _mapper;
        private readonly IGRepository<GeneralConfiguration> _generalConfiguration;
        private IAuditService _auditService;
        private readonly IDapperRepository<RevenuesExpensesTotalDto> _query;


        public FiscalPeriodRepository(IGRepository<FiscalPeriod> mainRepos, IConfiguration configuration, IMapper mapper, DeleteService deleteService,
        IGRepository<JournalEntriesDetail> journalEntriesDetailRepository, IJournalEntriesDetailsRepository journalEntriesDetailRepos,
        IGRepository<GeneralConfiguration> generalConfiguration,
        IAuditService auditService,
        IDapperRepository<RevenuesExpensesTotalDto> query,
        IGRepository<JournalEntriesMaster> journalEntriesRepository) : base(mainRepos, mapper, deleteService)
        {
            _fiscalPeriodRepos = mainRepos;
            _journalEntriesRepository = journalEntriesRepository;
            _journalEntriesDetailRepository = journalEntriesDetailRepository;
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
            _journalEntriesDetailRepos = journalEntriesDetailRepos;
            _mapper = mapper;
            _generalConfiguration = generalConfiguration;
            _auditService = auditService;
            _query = query;
        }
        public async Task<ActionResult<FiscalPeriodDto>> Edit([FromBody] FiscalPeriodDto command)
        {
            if (command.FiscalPeriodStatus == (int)FiscalPeriodStatusEnum.Opened)
            {
                var journalEntriesItem = await _journalEntriesRepository.GetAllIncluding(c => c.JournalEntriesDetail).AsNoTracking().FirstOrDefaultAsync(c => c.FiscalPeriodId == command.Id && c.IsDeleted != true && c.IsCloseFiscalPeriod == true);

                if (journalEntriesItem != null)
                {
                    foreach (var item in journalEntriesItem.JournalEntriesDetail.ToList())
                    {

                        await _journalEntriesDetailRepository.SoftDeleteWithoutSaveAsync(item.Id);
                    }

                    await _journalEntriesRepository.SoftDeleteWithoutSaveAsync(journalEntriesItem.Id);
                }


            }

            var entityDb = await _fiscalPeriodRepos.FirstOrDefaultAsync(c => c.Id == command.Id);
            if (entityDb == null)
            {
                throw new UserFriendlyException("Not Found");
            }
            var existCode = await _fiscalPeriodRepos.GetAllAsNoTracking().AnyAsync(x => x.Id != entityDb.Id && x.Code == command.Code && !x.IsDeleted);

            if (existCode)
                throw new UserFriendlyException("SameCodeExistsBefore");

            entityDb = _mapper.Map(command, entityDb);


            await _fiscalPeriodRepos.UpdateAsync(entityDb);
            await _fiscalPeriodRepos.SaveChangesAsync();

            return _mapper.Map<FiscalPeriodDto>(entityDb);
        }
        public async Task close(long fiscalPeriodId, string fromDateFisCalPeriod, string toDateFisCalPeriod, string closeAccountId)
        {
            try
            {

                long CompanyId = Convert.ToInt64(_auditService.CompanyId);
                long BranchId = Convert.ToInt64(_auditService.BranchId);

                var RevenuesTotalQuery = "select [dbo].[fn_Get_Revenues_Total]('" + fromDateFisCalPeriod + "','" + toDateFisCalPeriod + "')";
                var ExpensesTotalQuery = "select [dbo].[fn_Get_Expenses_Total]('" + fromDateFisCalPeriod + "','" + toDateFisCalPeriod + "')";
                //var RevenuesTotalResponse =  GetRevenuesTotal(fromDateFisCalPeriod, toDateFisCalPeriod);
                //object RevenuesTotalResult = RevenuesTotalResponse.Result[0].RevenuesTotal;


                //StringBuilder ExpensesTotalQuery = new StringBuilder();
                //ExpensesTotalQuery.Append(" select [dbo].[fn_Get_Expenses_Total]( ");
                //ExpensesTotalQuery.AppendFormat(" '{0}', ", fromDateFisCalPeriod);
                //ExpensesTotalQuery.AppendFormat(" '{0}' ) ", toDateFisCalPeriod);

                //var ExpensesTotalResult = await _query.QueryAsync<RevenuesExpensesTotalDto>(ExpensesTotalQuery.ToString());


                SqlConnection conn = new SqlConnection(_connectionString);
                conn.Open();

                SqlCommand RevenuesTotalCmd = new SqlCommand(RevenuesTotalQuery, conn);
                SqlDataAdapter RevenuesTotalCmdDa = new SqlDataAdapter(RevenuesTotalCmd);
                DataTable RevenuesTotalCmdDt = new DataTable();
                RevenuesTotalCmdDa.Fill(RevenuesTotalCmdDt);
                string RevenuesTotalResult = RevenuesTotalCmdDt.Rows[0][0].ToString();


                SqlCommand ExpensesTotalCmd = new SqlCommand(ExpensesTotalQuery, conn);
                SqlDataAdapter ExpensesTotalCmdDa = new SqlDataAdapter(ExpensesTotalCmd);
                DataTable ExpensesTotalCmdDt = new DataTable();
                ExpensesTotalCmdDa.Fill(ExpensesTotalCmdDt);
                string ExpensesTotalResult = ExpensesTotalCmdDt.Rows[0][0].ToString();

                conn.Close();


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

                var journalId = await _generalConfiguration.GetAll().Where(x => x.Code == "1007").FirstOrDefaultAsync();


                JournalEntriesMaster JournalEntriesMaster = new JournalEntriesMaster();
                JournalEntriesMaster.CompanyId = CompanyId;
                JournalEntriesMaster.BranchId = BranchId;
                JournalEntriesMaster.Code = code.ToString();

                if (fiscalPeriodId > 0)
                {
                    JournalEntriesMaster.FiscalPeriodId = fiscalPeriodId;
                }
                if (journalId != null)
                {
                    if (!string.IsNullOrEmpty(journalId.Value))
                    {
                        JournalEntriesMaster.JournalId = long.Parse(journalId.Value);
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
                string dateString = toDateFisCalPeriod;
                IFormatProvider culture = new CultureInfo("en-US", true);
                DateTime dateVal = DateTime.ParseExact(dateString, "yyyy-MM-dd", culture);
                JournalEntriesMaster.Date = dateVal;
                JournalEntriesMaster.IsCloseFiscalPeriod = true;
                await _journalEntriesRepository.InsertAsync(JournalEntriesMaster);
                _journalEntriesRepository.SaveChanges();

                await _journalEntriesDetailRepos.createEntryDetails(JournalEntriesMaster.Id, 1, closeAccountId, 0, 0, decimal.Parse(ExpensesTotalResult), decimal.Parse(RevenuesTotalResult), 1, 0, 0,0);
                var FiscalPeriodResult = await FirstInclude(fiscalPeriodId);
                FiscalPeriodResult.FiscalPeriodStatus = (int)FiscalPeriodStatusEnum.Closed;
                var result = await base.Update(FiscalPeriodResult);


            }
            catch (Exception ex)
            {

            }
        }

        public async Task<List<RevenuesExpensesTotalDto>> GetRevenuesTotal(string fromDateFisCalPeriod, string toDateFisCalPeriod)
        {
            StringBuilder revenuesTotalQuery = new StringBuilder();
            revenuesTotalQuery.Append(" select [dbo].[fn_Get_Revenues_Total]( ");
            revenuesTotalQuery.AppendFormat(" '{0}', ", fromDateFisCalPeriod);
            revenuesTotalQuery.AppendFormat(" '{0}' ) ", toDateFisCalPeriod);

            var RevenuesTotalResult = await _query.QueryAsync<RevenuesExpensesTotalDto>(revenuesTotalQuery.ToString());
            return (List<RevenuesExpensesTotalDto>)RevenuesTotalResult;
        }

        public async Task open(long fiscalPeriodId)
        {
            try
            {
                var journalEntriesItem = await _journalEntriesRepository.GetAllIncluding(c => c.JournalEntriesDetail).AsNoTracking().FirstOrDefaultAsync(c => c.FiscalPeriodId == fiscalPeriodId && c.IsDeleted != true && c.IsCloseFiscalPeriod == true);

                if (journalEntriesItem != null)
                {
                    foreach (var item in journalEntriesItem.JournalEntriesDetail.ToList())
                    {

                        await _journalEntriesDetailRepository.SoftDeleteWithoutSaveAsync(item.Id);
                    }

                    await _journalEntriesRepository.SoftDeleteWithoutSaveAsync(journalEntriesItem.Id);
                }

                var fiscalPeriodResult = await FirstInclude(fiscalPeriodId);
                fiscalPeriodResult.FiscalPeriodStatus = (int)FiscalPeriodStatusEnum.Opened;
                var result = await base.Update(fiscalPeriodResult);
            }
            catch(Exception ex)
            {

            }
         

        }



        public async Task<FiscalPeriodDto> FirstInclude(long id)
        {
            var item = await _fiscalPeriodRepos.GetAll().AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

            var result = _mapper.Map<FiscalPeriodDto>(item);
            return result;
        }

    }
}
