
using ResortAppStore.Services.Administration.Application.Subscription.CashPayment.Dto;
using ResortAppStore.Services.Administration.Domain.Entities.Subscription;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Subscription
{
    public interface ICashPaymentInfoRepository
    {
      
        Task<CashPaymentInfoDto> EditCashPaymentInfo(CashPaymentInfoDto request);
        Task<CashPaymentInfoDto> GetCashPaymentInfo();
    }
}

