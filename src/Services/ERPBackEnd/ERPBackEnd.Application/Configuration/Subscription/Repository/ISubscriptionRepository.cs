using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Subscription.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.Subscription.Repository
{
    public interface ISubscriptionRepository
    {
        Task<SubscriptionDto> GetLastSubscription(GetLastSubscription request);
    }
}
