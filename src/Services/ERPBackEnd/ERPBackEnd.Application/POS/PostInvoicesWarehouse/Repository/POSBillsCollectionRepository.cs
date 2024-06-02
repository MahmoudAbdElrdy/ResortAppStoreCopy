using Common.Constants;
using Common.Interfaces;
using Microsoft.Data.SqlClient;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Shared.Enums;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Bills.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Bills.Repository;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;
using ResortAppStore.Services.ERPBackEnd.Domain.Warehouses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.Repository
{
    public class POSBillsCollectionRepository : IPOSBillsCollectionRepository
    {
        private readonly IAuditService _auditService;
        private readonly IBillRepository _billRepository;
        private readonly IGRepository<Bill> _billRepos;
        private readonly IGRepository<POSBillsUser> _POSBillsUserRepos;
        private readonly int  billsSales =(int) BillKindEnum.Sales ;
        private readonly int billsSalesReturns = (int)BillKindEnum.SalesReturn;

        public POSBillsCollectionRepository(
            IGRepository<Bill> billRepos,
            IAuditService auditService,
            IGRepository<POSBillsUser> POSBillsUserRepos,
            IBillRepository billRepository)
        {
            _billRepos = billRepos;
            _auditService = auditService;
            _billRepository = billRepository;
            _POSBillsUserRepos = POSBillsUserRepos;
        }

        public virtual async Task<ResponseResult> ExecuteGetPOSBillsCollection(POSBillsCollection POSBillsCollection)
        {
             string storedProcedure = "";
            if (POSBillsCollection.BillKindId == billsSales)
            {
                  storedProcedure = "SP_Get_POS_Bills_Collection_Sales";
            }
            else if(POSBillsCollection.BillKindId == billsSalesReturns)
            {
                storedProcedure = "SP_Get_POS_Bills_Collection_Sales_Returns";
            }

       

            var parameters = new List<SqlParameter>
            {
                new SqlParameter("@companyId", Convert.ToInt64(_auditService.CompanyId)),
                new SqlParameter("@branchId", Convert.ToInt64(_auditService.BranchId)),
                new SqlParameter("@userId", _auditService.UserId),
                new SqlParameter("@fromDate", POSBillsCollection.FromDate),
                new SqlParameter("@toDate", POSBillsCollection.ToDate),
                new SqlParameter("@posBillKindId", POSBillsCollection.BillKindId),
                new SqlParameter("@warehouseBillKindId", POSBillsCollection.WarehouseBillTypeId),
                new SqlParameter("@posBillTypeIds", POSBillsCollection.BillTypeIds),
                new SqlParameter("@isSelectShift", POSBillsCollection.IsSelectShift),
                new SqlParameter("@isSelectUser", POSBillsCollection.IsSelectUser),
                new SqlParameter("@isSelectPointOfSale", POSBillsCollection.IsSelectPointOfSale),
                new SqlParameter("@isSelectCostOfCenter", POSBillsCollection.IsSelectCostCenter),
                new SqlParameter("@isDeleteOriginalBill", POSBillsCollection.IsDeleteOriginalInvoice)
            };

            var result = _billRepos.Excute(storedProcedure, parameters, true);

            if (result.Success)
            {
                await GeneratePostPOSBillsEntries();
            }

            return result;
        }

        public ResponseResult ExecuteDeleteAllFromPOSBillsUsers()
        {
            const string storedProcedure = "DeleteAllFromPOSBillsUsers";
            return _billRepos.Excute(storedProcedure, new List<SqlParameter>(), true);
        }

        private async Task GeneratePostPOSBillsEntries()
        {
            const string storedProcedure = "GetAllPOSBillsUsers";
            var billUsers = _POSBillsUserRepos.Excute<POSBillsUser>(storedProcedure, new List<SqlParameter>(), true);

            var billIds= billUsers.Data.Select(user => user.BillId).ToList();

            // Assuming `generateEntry` is a method that takes a list of bill IDs
            ResponseResult result =   await _billRepository.generateEntry(billIds);
            if (result.Success)
            {
                ExecuteDeleteAllFromPOSBillsUsers();
            }
           


        }
    }
}
                                                                                                                                                