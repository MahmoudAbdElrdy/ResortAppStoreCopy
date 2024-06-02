using Common.Constants;
using ResortAppStore.Services.ERPBackEnd.Application.POS.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.POS.POSDeliveries.Repository
{
    public interface IPOSDeliveryMasterRepository
    {
        Task<int> DeleteAsync(long id);
        Task<int> DeleteListAsync(List<object> ids);
        Task<POSDeliveryMasterDto> CreatePOSDelivery(POSDeliveryMasterDto input);
        Task<POSDeliveryMasterDto> UpdatePOSDelivery(POSDeliveryMasterDto input);
        Task<POSDeliveryMasterDto> FirstInclude(long id);
        Task<ResponseResult<List<CalculatePOSDeliveryDto>>> CalculatePOSDelivery(string DateFrom, string DateTo);


    }
}

