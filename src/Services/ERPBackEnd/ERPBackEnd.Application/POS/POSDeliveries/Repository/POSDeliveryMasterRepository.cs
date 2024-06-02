using AutoMapper;
using Common.Constants;
using Common.Interfaces;
using Common.Repositories;
using Common.Services.Service;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.POS.Bills.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.POS.CashTransfers.Repository;
using ResortAppStore.Services.ERPBackEnd.Application.POS.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.POSDeliveries.Repository
{
    public class POSDeliveryMasterRepository : GMappRepository<POSDeliveryMaster, POSDeliveryMasterDto, long>, IPOSDeliveryMasterRepository
    {
        private readonly IGRepository<POSDeliveryMaster> _posDeliveryMasterRepos;
        private IGRepository<POSDeliveryDetail> _posDeliveryDetailsRepos { get; set; }
        private readonly IGRepository<POSBill> _posBillRepos;
        private readonly IPOSBillRepository _posBillRepository;
        private readonly IGRepository<CashTransfer> _cashTransferRepos;
        private readonly ICashTransferRepository _cashTransferRepository;
        private IMapper _mpper;
        private readonly IGRepository<GeneralConfiguration> _generalConfiguration;
        private IAuditService _auditService;


        public POSDeliveryMasterRepository(IGRepository<POSDeliveryMaster> mainRepos, IMapper mapper,
            DeleteService deleteService
            , IGRepository<POSDeliveryDetail> posDeliveryDetailsRepos,
            IGRepository<POSBill> posBillRepos,
            IGRepository<CashTransfer> cashTransferRepos,
            IPOSBillRepository posBillRepository,
            ICashTransferRepository cashTransferRepository,
        IGRepository<GeneralConfiguration> generalConfiguration,
             IAuditService auditService
            ) : base(mainRepos, mapper, deleteService)
        {
            _posDeliveryMasterRepos = mainRepos;
            _mpper = mapper;
            _posDeliveryDetailsRepos = posDeliveryDetailsRepos;
            _posBillRepos = posBillRepos;
            _cashTransferRepos = cashTransferRepos;
            _generalConfiguration = generalConfiguration;
            _auditService = auditService;
            _posBillRepository = posBillRepository;
            _cashTransferRepository = cashTransferRepository;



        }
        public async Task<POSDeliveryMasterDto> FirstInclude(long id)
        {
            var item = await _posDeliveryMasterRepos.GetAllIncluding(c => c.POSDeliveryDetails).AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);


            var result = _mpper.Map<POSDeliveryMasterDto>(item);
            return result;
        }

        public async Task<int> DeleteAsync(long id)
        {

            var posDeliveryResult = await FirstInclude(id);

            if (posDeliveryResult?.POSDeliveryDetails != null)
            {
                foreach (var item in posDeliveryResult?.POSDeliveryDetails)
                {
                    await _posDeliveryDetailsRepos.SoftDeleteWithoutSaveAsync(item.Id);
                }

            }

            return await base.ScriptCheckDeleteWithDetails(id, new List<string> { "POSDeliveryDetails" }, "POSDeliveryMasters", "Id");


        }

        public async Task<int> DeleteListAsync(List<object> ids)
        {
            foreach (var id in ids)
            {

                var posDeliveryResult = await FirstInclude(Convert.ToInt64(id));

                if (posDeliveryResult?.POSDeliveryDetails != null)
                {
                    foreach (var item in posDeliveryResult?.POSDeliveryDetails)
                    {
                        await _posDeliveryDetailsRepos.SoftDeleteWithoutSaveAsync(item.Id);
                    }

                }


            }

            return await base.ScriptCheckDeleteWithDetails(ids, new List<string> { "POSDeliveryDetails" }, "POSDeliveryMasters", "Id");
        }
        public async Task<POSDeliveryMasterDto> CreatePOSDelivery(POSDeliveryMasterDto input)
        {

            var result = await base.Create(input);

            if (input.POSDeliveryDetails != null)
            {
                foreach (var item in input.POSDeliveryDetails)
                {
                    if (item.BillIds != null && item.BillIds != "")
                    {
                        string[] billIdsArray = item.BillIds.Split(',');
                        foreach (string billId in billIdsArray)
                        {

                            var billResult = await _posBillRepository.FirstInclude(long.Parse(billId));


                            if (billResult != null)
                            {
                                billResult.IsLocked = true;
                                billResult.POSDeliveryMasterId = result.Id;
                                var entity = _mpper.Map<POSBill>(billResult);

                                await _posBillRepos.UpdateAsync(entity);

                            }

                        }

                    }

                    if (item.CashTransferIds != null && item.CashTransferIds != "")
                    {
                        string[] CashTransferIdsArray = item.CashTransferIds.Split(',');
                        foreach (string CashTransferId in CashTransferIdsArray)
                        {

                            var cashTransferResult = await _cashTransferRepos.FirstOrDefaultAsync(long.Parse(CashTransferId));
                            if (cashTransferResult != null)
                            {
                                cashTransferResult.IsLocked = true;
                                cashTransferResult.POSDeliveryMasterId = result.Id;

                                await _cashTransferRepos.UpdateAsync(cashTransferResult);
                            }

                        }

                    }


                }


            }
            return result;
        }

        public async Task<POSDeliveryMasterDto> UpdatePOSDelivery(POSDeliveryMasterDto input)
        {
            var posDeliveryResult = await FirstInclude(input.Id);


            if (posDeliveryResult?.POSDeliveryDetails != null)
            {
                foreach (var item in posDeliveryResult?.POSDeliveryDetails)
                {
                    var entity = _mpper.Map<POSDeliveryDetail>(item);
                    await _posDeliveryDetailsRepos.SoftDeleteAsync(entity);
                }

            }


            var result = await base.Update(input);
            return input;

        }

        public virtual async Task<ResponseResult<List<CalculatePOSDeliveryDto>>> CalculatePOSDelivery(string DateFrom, string DateTo)
        {
            var sp = "SP_POS_Delivery";

            long companyId = Convert.ToInt64(_auditService.CompanyId);
            long branchId = Convert.ToInt64(_auditService.BranchId);

            var userId = _auditService.UserId;

            var fiscalPeriodId = "7";
            long? fiscalPeriodIdValue = null;
            var fiscalPeriodIdResult = await _generalConfiguration.GetAll().Where(x => x.Code == fiscalPeriodId).FirstOrDefaultAsync();
            if (!string.IsNullOrEmpty(fiscalPeriodIdResult.Value))
            {
                fiscalPeriodIdValue = long.Parse(fiscalPeriodIdResult.Value);
            }


            var result = _posDeliveryMasterRepos.Excute<CalculatePOSDeliveryDto>(sp, new List<SqlParameter>() {
                 new SqlParameter(){
                    ParameterName = "@dateFrom",
                    Value = DateFrom,


                },
                  new SqlParameter(){
                    ParameterName = "@dateTo ",
                    Value = DateTo,


                },

                new SqlParameter(){
                    ParameterName = "@userId",
                    Value = userId,


                },
                new SqlParameter(){
                    ParameterName = "@companyId",
                    Value = companyId,

                },
                 new SqlParameter(){
                    ParameterName = "@branchId",
                    Value = branchId,

                },
                new SqlParameter(){
                    ParameterName = "@fiscalPeriodId" ,
                    Value = fiscalPeriodIdValue,

                },

            }, true);

            return result;
        }




    }
}
