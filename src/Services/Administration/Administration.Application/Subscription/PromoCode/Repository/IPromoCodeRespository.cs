using ResortAppStore.Services.Administration.Application.Features.Customers.Dto;
using ResortAppStore.Services.Administration.Application.Subscription.PromoCode.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.Administration.Application.Subscription.PromoCode.Repository
{
    public interface IPromoCodeRespository
    {
        Task<PromoCodeDto> CreatePromoCodeCommand(PromoCodeDto request);

        Task<PromoCodeDto> EditPromoCodeCommand(PromoCodeDto request);

        Task<int> DeletePromoCodeCommand(long id);

        Task<PromoCodeDto> GetPromoCodeCommandbyId(long id);

        Task<PromoCodeDto> GetPromoCodeCommandbyCode(string id);

        Task<List<PromoCodeDto>> GetAllPromoCodesCommand();


    }
}
