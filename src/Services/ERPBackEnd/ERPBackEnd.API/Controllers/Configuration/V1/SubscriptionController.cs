using Common.BaseController;
using ERPBackEnd.API.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Subscription.Dto;
using ResortAppStore.Services.ERPBackEnd.Application.Configuration.Subscription.Repository;

namespace ResortAppStore.Services.ERPBackEnd.API.Controllers.Configuration.V1
{
    [Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(AuthorizeByPermissionsAttribute))]
    public class SubscriptionController : BaseController
    {
        private ISubscriptionRepository _subscriptionRepository;
        public SubscriptionController(ISubscriptionRepository subscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
        }

        [HttpGet("GetLastSubscription")]
        public async Task<ActionResult<SubscriptionDto>> GetLastSubscription() 
        {
            return Ok(await _subscriptionRepository.GetLastSubscription(new GetLastSubscription()));
        }
    }
}
