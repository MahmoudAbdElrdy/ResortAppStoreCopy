using Common.Constants;
using ResortAppStore.Services.ERPBackEnd.Application.Warehouses.Bills.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.POS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.Repository
{
    public interface IPOSBillsCollectionRepository
    {
        Task<ResponseResult> ExecuteGetPOSBillsCollection(POSBillsCollection _POSBillsCollection);
    }
}
