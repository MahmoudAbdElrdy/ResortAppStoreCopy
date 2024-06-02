using AutoMapper;
using Common.Extensions;
using Common.Interfaces;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Accounting.Vouchers.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.POS.CashTransfers.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Accounting;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.CashTransfers.Repository
{
    public class CashTransferRepository : GMappRepository<CashTransfer, CashTransferDto, long>, ICashTransferRepository
    {
        private readonly IGRepository<CashTransfer> _cashTransferRepos;
        private IMapper _mpper;
        private IAuditService _auditService;
        private readonly IGRepository<GeneralConfiguration> _generalConfiguration;
        private readonly IDapperRepository<CalculatePOSBillCashPaymentTotalDto> _query;

        public CashTransferRepository(IGRepository<CashTransfer> mainRepos, IMapper mapper, DeleteService deleteService,
             IAuditService auditService, IGRepository<GeneralConfiguration> generalConfiguration,
             IDapperRepository<CalculatePOSBillCashPaymentTotalDto> query


            ) : base(mainRepos, mapper, deleteService)
        {
            _cashTransferRepos = mainRepos;
            _mpper = mapper;
            _auditService = auditService;
            _generalConfiguration = generalConfiguration;
            _query = query;


        }
        public async Task<List<CalculatePOSBillCashPaymentTotalDto>> CalculatePOSBillCashPaymentTotal(string? fromUserId, long fromPointOfSaleId, long? fromShiftId)
        {
            long companyId = Convert.ToInt64(_auditService.CompanyId);
            long branchId = Convert.ToInt64(_auditService.BranchId);

            var fiscalPeriodId = "7";
            var fiscalPeriodIdResult = await _generalConfiguration.GetAll().Where(x => x.Code == fiscalPeriodId).FirstOrDefaultAsync();

            long fiscalPeriodValue = 0;
            if (!string.IsNullOrEmpty(fiscalPeriodIdResult.Value))
            {
                fiscalPeriodValue = long.Parse(fiscalPeriodIdResult.Value);
            }

            // Build the query string
            StringBuilder query = new StringBuilder();
            query.Append("SELECT [dbo].[fn_Calculate_POS_Bill_Cash_Payment_Total] ");

            query.AppendFormat("('{0}', ", !string.IsNullOrEmpty(fromUserId) ? fromUserId : "null");
            query.AppendFormat("{0}, ", fromPointOfSaleId > 0 ? fromPointOfSaleId.ToString() : "null");
            query.AppendFormat("{0}, ", fromShiftId.HasValue && fromShiftId.Value > 0 ? fromShiftId.ToString() : "null");
            query.AppendFormat("{0}, ", companyId);
            query.AppendFormat("{0}, ", branchId);
            query.AppendFormat("{0}) AS Total", fiscalPeriodValue);

            // Execute the query and handle potential null results
            var result = await _query.QueryAsync<CalculatePOSBillCashPaymentTotalDto>(query.ToString());

            // Ensure result is not null before returning
            return result?.ToList() ?? new List<CalculatePOSBillCashPaymentTotalDto>();
        }
        public async Task<CashTransferDto> CreateCashTransfer(CashTransferDto input)
        {
            List<CalculatePOSBillCashPaymentTotalDto> calculate = await CalculatePOSBillCashPaymentTotal(input.FromUserId, input.FromPointOfSaleId, input.FromShiftDetailId);
            if (input.Amount > calculate[0].Total)
            {
                throw new UserFriendlyException("Amount is greater than Bill Cash Payment Total for this user");

            }
            var entity = _mpper.Map<CashTransfer>(input);
            entity.CreatedBy = _auditService.UserId;
            entity.CreatedAt = DateTime.Now;
            var result = await base.CreateTEntity(entity);
            return result;


        }
        public async Task<CashTransferDto> UpdateCashTransfer(CashTransferDto input)
        {
            List<CalculatePOSBillCashPaymentTotalDto> calculate = await CalculatePOSBillCashPaymentTotal(input.FromUserId, input.FromPointOfSaleId, input.FromShiftDetailId);
            if (input.Amount > calculate[0].Total)
            {
                throw new UserFriendlyException("Amount is greater than Bill Cash Payment Total for this user");

            }
            var cashTransferResult = await FirstInclude(input.Id);

            input.CreatedBy = cashTransferResult.CreatedBy;
            input.CreatedAt = cashTransferResult.CreatedAt;

            input.UpdateBy = _auditService.UserId;
            input.UpdatedAt = DateTime.Now;

            var result = await base.Update(input);
            return result;



        }
        public async Task<CashTransferDto> FirstInclude(long id)
        {
            var item = await _cashTransferRepos.GetAll().AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);

            var result = _mpper.Map<CashTransferDto>(item);
            return result;
        }



    }
}
