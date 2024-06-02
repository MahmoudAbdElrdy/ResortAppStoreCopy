using AutoMapper;
using ResortAppStore.Repositories;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Subscription.Dto;
using ResortAppStore.Services.ERPBackEnd.Domain.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ResortAppStore.Services.ERPBackEnd.Application.Configuration.Subscription.Repository
{
   
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly IGRepository<Subscriptions> _context;

        private readonly IMapper _mapper;
        public SubscriptionRepository(IGRepository<Subscriptions> context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<SubscriptionDto> GetLastSubscription(GetLastSubscription request)
        {
            var subscriptions = (await _context.GetAllListAsync(x => !x.IsDeleted)).LastOrDefault();

            var res = _mapper.Map<SubscriptionDto>(subscriptions);

            return res;

        }
    }
}
